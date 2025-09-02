namespace ClassroomAllocationApp.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Each Teacher belongs to a Department
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        // One Teacher can teach many Courses
        public List<Course> Courses { get; set; } = new();
    }
}
