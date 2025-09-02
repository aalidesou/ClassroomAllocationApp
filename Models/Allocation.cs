using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassroomAllocationApp.Models
{
    public class Allocation
    {
        public int Id { get; set; }

        // Foreign Key to Course
        public int CourseId { get; set; }
        public Course Course { get; set; }

        // Foreign Key to Classroom
        public int ClassroomId { get; set; }
        public Classroom Classroom { get; set; }

        public DateTime StartTime { get; set; }

        [NotMapped]
        public DateTime EndTime => StartTime.AddHours(1);

        public DayOfWeek DayOfWeek { get; set; }
    }
}
