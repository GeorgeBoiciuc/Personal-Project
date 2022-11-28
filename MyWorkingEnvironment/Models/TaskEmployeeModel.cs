using System.ComponentModel.DataAnnotations;

namespace MyWorkingEnvironment.Models
{
    public class TaskEmployeeModel
    {
        public Guid IdTaskEmployee { get; set; }
        public Guid? IdEmployee { get; set; }
        [StringLength(50, ErrorMessage = "Maximum 50 characters")]
        public string Title { get; set; } = null!;
        public string Priority { get; set; } = null!;
        [StringLength(250, ErrorMessage = "Maximum 250 characters")]
        public string? Description { get; set; }
    }

    public enum PriorityType
    {
        High,
        Medium,
        Low
    }
}
