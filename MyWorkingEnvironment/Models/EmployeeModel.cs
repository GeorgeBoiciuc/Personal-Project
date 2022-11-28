using System.ComponentModel.DataAnnotations;

namespace MyWorkingEnvironment.Models
{
    public class EmployeeModel
    {
        public Guid IdEmployee { get; set; }
        [StringLength(50, ErrorMessage = "Maximum 50 characters")]
        public string FirstName { get; set; } = null!;
        [StringLength(50, ErrorMessage = "Maximum 50 characters")]
        public string LastName { get; set; } = null!;
        [StringLength(50, ErrorMessage = "Maximum 50 characters")]
        public string? Title { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}")]
        public DateTime? JoinDate { get; set; }
        [Range(1, 100)]
        public int? VacationDays { get; set; }
    }

    public enum JobTitle
    {
        Developer,
        Tester,
        Recruiter,
        Manager
    }
}
