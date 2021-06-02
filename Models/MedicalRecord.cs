using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dental.Models
{
    public class MedicalRecord
    {
        [Key]
        public int MedicalRecordNumber { get; set; }
        public int PatientId { get; set; }
    }
}
