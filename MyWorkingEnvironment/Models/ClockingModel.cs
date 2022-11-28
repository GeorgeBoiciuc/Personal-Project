using System.ComponentModel.DataAnnotations;

namespace MyWorkingEnvironment.Models
{
    public class ClockingModel
    {
        public Guid IdClocking { get; set; }
        public Guid? IdEmployee { get; set; }
        public string Type { get; set; } = null!;
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}")]
        public DateTime Date { get; set; }
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}")]
        public DateTime In { get; set; }
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}")]
        public DateTime Out { get; set; }
    }

    public enum ClockingType
    {
        Workday,
        Vacation,
        NationalHoliday,
        Sickleave
    }
}
