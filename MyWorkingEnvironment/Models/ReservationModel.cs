using System.ComponentModel.DataAnnotations;

namespace MyWorkingEnvironment.Models
{
    public class ReservationModel
    {
        public Guid IdReservation { get; set; }
        public Guid? IdEmployee { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}")]
        public DateTime Date { get; set; }
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}")]
        public DateTime Start { get; set; }
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}")]
        public DateTime End { get; set; }
    }
}
