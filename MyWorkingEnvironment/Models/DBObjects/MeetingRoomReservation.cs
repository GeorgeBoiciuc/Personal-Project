using System;
using System.Collections.Generic;

namespace MyWorkingEnvironment.Models.DBObjects
{
    public partial class MeetingRoomReservation
    {
        public Guid IdMeetingRoomReservation { get; set; }
        public Guid? IdReservation { get; set; }
        public Guid? IdMeetingRoom { get; set; }

        public virtual MeetingRoom? IdMeetingRoomNavigation { get; set; }
        public virtual Reservation? IdReservationNavigation { get; set; }
    }
}
