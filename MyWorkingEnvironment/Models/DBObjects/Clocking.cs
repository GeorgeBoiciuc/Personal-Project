using System;
using System.Collections.Generic;

namespace MyWorkingEnvironment.Models.DBObjects
{
    public partial class Clocking
    {
        public Guid IdClocking { get; set; }
        public Guid? IdEmployee { get; set; }
        public string Type { get; set; } = null!;
        public DateTime Date { get; set; }
        public DateTime In { get; set; }
        public DateTime Out { get; set; }

        public virtual Employee? IdEmployeeNavigation { get; set; }
    }
}
