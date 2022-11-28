using MyWorkingEnvironment.Models;
using MyWorkingEnvironment.Repository;
using System.ComponentModel.DataAnnotations;

namespace MyWorkingEnvironment.ViewModels
{
    public class MeetingRoomReservationViewModel
    {
        public Guid IdMeetingRoomReservation { get; set; }
        public Guid IdReservation { get; set; }
        public Guid? IdEmployee { get; set; }
        public Guid IdMeetingRoom { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}")]
        public DateTime Date { get; set; }
        [DisplayFormat(DataFormatString = "{0:HH:mm}")]
        public DateTime Start { get; set; }
        [DisplayFormat(DataFormatString = "{0:HH:mm}")]
        public DateTime End { get; set; }
        public string Name { get; set; } = null!;

        public MeetingRoomReservationViewModel(MeetingRoomReservationModel model, ReservationRepository reservationRepository, MeetingRoomRepository meetingRoomRepository)
        {
            if (model != null)
            {
                var reservation = reservationRepository.GetReservationById((Guid)model.IdReservation);
                this.IdReservation = reservation.IdReservation;
                this.IdEmployee = reservation.IdEmployee;
                this.Date = reservation.Date;
                this.Start = reservation.Start;
                this.End = reservation.End;

                var meetingRoom = meetingRoomRepository.GetMeetingRoomById((Guid)model.IdMeetingRoom);
                this.IdMeetingRoom = meetingRoom.IdMeetingRoom;
                this.Name = meetingRoom.Name;
            }
        }
    }
}
