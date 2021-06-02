using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dental.Models
{
    public class Intervention
    {
        [Key]
        public int InterventionId { get; set; }
        public int MedicalRecordNumber { get; set; }
        public int DentistId { get; set; }
        public DateTime DateTime { get; set; }
        public string Description { get; set; }
    }
}
