namespace MyWorkingEnvironment.Models
{
    public class MeetingRoomReservationModel
    {
        public Guid IdMeetingRoomReservation { get; set; }
        public Guid? IdReservation { get; set; }
        public Guid? IdMeetingRoom { get; set; }
    }
}
