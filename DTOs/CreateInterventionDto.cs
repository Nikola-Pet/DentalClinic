using Dental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dental.DTOs
{
    public class CreateInterventionDto
    {
        public int MedicalRecordNumber { get; set; }
        public DateTime DateTime { get; set; }
        public string Description { get; set; }

    }
}
