using MyWorkingEnvironment.Models;
using MyWorkingEnvironment.Repository;
using System.ComponentModel.DataAnnotations;

namespace MyWorkingEnvironment.ViewModels
{
    public class ClockingsEmployeeViewModel
    {
        public Guid IdClocking { get; set; }
        public Guid? IdEmployee { get; set; }
        [StringLength(100, ErrorMessage = "Maximum 50 characters")]
        public string Name { get; set; } = null!;
        [StringLength(50, ErrorMessage = "Maximum 50 characters")]
        public string? Title { get; set; }
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
        public ClockingsEmployeeViewModel(ClockingModel model, EmployeeRepository employeegRepository)
        {
            if (model != null)
            {
                var employee = employeegRepository.GetEmployeeById((Guid)model.IdEmployee);
                IdClocking = model.IdClocking;
                IdEmployee = model.IdEmployee;
                Name = employee.FirstName + " " + employee.LastName;
                Title = employee.Title;
                Type = model.Type;
                Date = model.Date;
                In = model.In;
                Out = model.Out;
            }
        }
    }
}
