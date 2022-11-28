using System.ComponentModel.DataAnnotations;

namespace MyWorkingEnvironment.Models
{
    public class MeetingRoomModel
    {
        public Guid IdMeetingRoom { get; set; }
        [StringLength(50, ErrorMessage = "Maximum 50 characters")]
        public string Name { get; set; } = null!;
        [StringLength(50, ErrorMessage = "Maximum 50 characters")]
        public string? Floor { get; set; }
        [Range(1, 100)]
        public int? Capacity { get; set; }
    }
}
