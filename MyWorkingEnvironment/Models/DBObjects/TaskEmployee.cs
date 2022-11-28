using System;
using System.Collections.Generic;

namespace MyWorkingEnvironment.Models.DBObjects
{
    public partial class TaskEmployee
    {
        public Guid IdTaskEmployee { get; set; }
        public Guid? IdEmployee { get; set; }
        public string Title { get; set; } = null!;
        public string Priority { get; set; } = null!;
        public string? Description { get; set; }

        public virtual Employee? IdEmployeeNavigation { get; set; }
    }
}
