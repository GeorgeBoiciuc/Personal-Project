using System;
using System.Collections.Generic;

namespace MyWorkingEnvironment.Models.DBObjects
{
    public partial class Reservation
    {
        public Reservation()
        {
            MeetingRoomReservations = new HashSet<MeetingRoomReservation>();
        }

        public Guid IdReservation { get; set; }
        public Guid? IdEmployee { get; set; }
        public DateTime Date { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public virtual Employee? IdEmployeeNavigation { get; set; }
        public virtual ICollection<MeetingRoomReservation> MeetingRoomReservations { get; set; }
    }
}
