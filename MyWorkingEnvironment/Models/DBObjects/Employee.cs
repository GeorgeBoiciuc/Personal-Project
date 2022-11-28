using System;
using System.Collections.Generic;

namespace MyWorkingEnvironment.Models.DBObjects
{
    public partial class Employee
    {
        public Employee()
        {
            Clockings = new HashSet<Clocking>();
            Reservations = new HashSet<Reservation>();
            TaskEmployees = new HashSet<TaskEmployee>();
        }

        public Guid IdEmployee { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Title { get; set; }
        public string Email { get; set; } = null!;
        public DateTime? JoinDate { get; set; }
        public int? VacationDays { get; set; }

        public virtual ICollection<Clocking> Clockings { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
        public virtual ICollection<TaskEmployee> TaskEmployees { get; set; }
    }
}
