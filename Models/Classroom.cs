namespace ClassroomAllocationApp.Models
{
    public class Classroom
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; }
        public int Capacity { get; set; }

        // One classroom can have many allocations
        public ICollection<Allocation> Allocations { get; set; }
    }
}
