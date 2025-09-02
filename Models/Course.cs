namespace ClassroomAllocationApp.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string CourseCode { get; set; }
        public string SubjectName { get; set; }

        // One Course belongs to one Teacher
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        // One Course belongs to one Department
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        //  Many Students can enroll in many Courses (many-to-many)
        public List<Student> Students { get; set; } = new();

        // One course can have many allocations
        public ICollection<Allocation> Allocations { get; set; }
    }
}
