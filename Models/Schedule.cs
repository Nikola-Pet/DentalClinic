using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dental.Models
{
    public class Schedule
    {
        [Key]
        public int ScheduleId { get; set; }
        public int PatientId { get; set; }
        public int DentistId { get; set; }
        public DateTime? DateTime { get; set; }

    }
}
