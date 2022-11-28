using System;
using System.Collections.Generic;

namespace MyWorkingEnvironment.Models.DBObjects
{
    public partial class MeetingRoom
    {
        public MeetingRoom()
        {
            MeetingRoomReservations = new HashSet<MeetingRoomReservation>();
        }

        public Guid IdMeetingRoom { get; set; }
        public string Name { get; set; } = null!;
        public string? Floor { get; set; }
        public int? Capacity { get; set; }

        public virtual ICollection<MeetingRoomReservation> MeetingRoomReservations { get; set; }
    }
}
