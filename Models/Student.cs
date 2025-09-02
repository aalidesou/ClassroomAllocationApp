namespace ClassroomAllocationApp.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //Many Students can enroll in many Courses (many-to-many)
        public List<Course> Courses { get; set; } = new();
    }
}
