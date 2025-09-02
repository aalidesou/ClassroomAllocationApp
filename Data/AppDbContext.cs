using Microsoft.EntityFrameworkCore;
using ClassroomAllocationApp.Models;

namespace ClassroomAllocationApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Classroom> Classrooms { get; set; }
        public DbSet<Allocation> Allocations { get; set; }
        public DbSet<VenueChangeRequest> VenueChangeRequests { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 🔹 Many-to-Many: Student <-> Course
            modelBuilder.Entity<Student>()
                .HasMany(s => s.Courses)
                .WithMany(c => c.Students)
                .UsingEntity(j => j.ToTable("StudentCourses"));

            // 🔹 One-to-Many: Teacher -> Courses
            modelBuilder.Entity<Teacher>()
                .HasMany(t => t.Courses)
                .WithOne(c => c.Teacher)
                .HasForeignKey(c => c.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);

            // 🔹 One-to-Many: Department -> Teachers
            modelBuilder.Entity<Department>()
                .HasMany(d => d.Teachers)
                .WithOne(t => t.Department)
                .HasForeignKey(t => t.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            // 🔹 One-to-Many: Department -> Courses
            modelBuilder.Entity<Department>()
                .HasMany(d => d.Courses)
                .WithOne(c => c.Department)
                .HasForeignKey(c => c.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            // 🔹 Allocation: Course -> Classroom
            modelBuilder.Entity<Allocation>()
                .HasOne(a => a.Course)
                .WithMany()
                .HasForeignKey(a => a.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Allocation>()
                .HasOne(a => a.Classroom)
                .WithMany()
                .HasForeignKey(a => a.ClassroomId)
                .OnDelete(DeleteBehavior.Cascade);

            // 🔹 Ensure StartTime is stored properly
            modelBuilder.Entity<Allocation>()
                .Property(a => a.StartTime)
                .HasColumnType("datetime");

            // Optional: You could enforce uniqueness (no double-booking classrooms)
            modelBuilder.Entity<Allocation>()
                .HasIndex(a => new { a.ClassroomId, a.StartTime })
                .IsUnique();

            modelBuilder.Entity<Allocation>()
        .HasOne(a => a.Course)
        .WithMany(c => c.Allocations)
        .HasForeignKey(a => a.CourseId);

            modelBuilder.Entity<Allocation>()
                .HasOne(a => a.Classroom)
                .WithMany(c => c.Allocations)
                .HasForeignKey(a => a.ClassroomId);

            // Example seed data (kept minimal)
            modelBuilder.Entity<Department>().HasData(
                new Department { Id = 1, Name = "Information Technology" },
                new Department { Id = 2, Name = "Management" },
                new Department { Id = 3, Name = "Mass Media" }
            );

            modelBuilder.Entity<Teacher>().HasData(
                new Teacher { Id = 1, Name = "Dr. Smith", DepartmentId = 3 },
                new Teacher { Id = 2, Name = "Prof. Johnson", DepartmentId = 2 },
                new Teacher { Id = 3, Name = "Mr. Roy", DepartmentId = 1 }
            );

            modelBuilder.Entity<Classroom>().HasData(
                new Classroom { Id = 1, RoomNumber = "LR01", Capacity = 50 },
                new Classroom { Id = 2, RoomNumber = "LR11", Capacity = 30 },
                new Classroom { Id = 3, RoomNumber = "LR20C", Capacity = 20 }
            );

            modelBuilder.Entity<Course>().HasData(
                new Course { Id = 1, CourseCode = "USITY5002", DepartmentId=1, SubjectName = "Database Systems", TeacherId = 3 },
                new Course { Id = 2, CourseCode = "UCMGS2003", DepartmentId = 2, SubjectName = "Organisation of Commerce", TeacherId = 2 },
                new Course { Id = 3, CourseCode = "UAMCJ3001", DepartmentId = 3, SubjectName = "Introduction to Mass Media", TeacherId = 1}
            );

            modelBuilder.Entity<Student>().HasData(
                new Student { Id = 1, Name = "Aaliya Desousa" },
                new Student { Id = 2, Name = "Diana Tanttra" },
                new Student { Id = 3, Name = "Jahnvi Kumar" }
            );
        }
    }
}
