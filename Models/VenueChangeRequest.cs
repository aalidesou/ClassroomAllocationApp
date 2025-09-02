using ClassroomAllocationApp.Models;

public class VenueChangeRequest
{
    public int Id { get; set; }

    public int TeacherId { get; set; }
    public Teacher Teacher { get; set; }

    public int AllocationId { get; set; } // Which class they want to change
    public Allocation Allocation { get; set; }

    public int? NewClassroomId { get; set; }
    public Classroom NewClassroom { get; set; }

    public DateTime RequestedDate { get; set; } // Single-day change
    public string Status { get; set; } = "Pending"; // Pending / Approved / Rejected
}
