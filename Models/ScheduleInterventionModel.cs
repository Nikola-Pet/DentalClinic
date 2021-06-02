using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dental.Models
{
    public class ScheduleInterventionModel
    {
        public Intervention Intervention { get; set; }
        public Schedule schedule { get; set; }
    }
}
