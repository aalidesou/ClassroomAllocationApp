namespace ClassroomAllocationApp.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //One Department has many Teachers
        public List<Teacher> Teachers { get; set; } = new();

        // One Department has many Courses
        public List<Course> Courses { get; set; } = new();
    }
}
