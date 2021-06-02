using Dental.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dental.Data
{
    public class DentalClinicContext : DbContext
    {
        

        public DentalClinicContext(DbContextOptions<DentalClinicContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<Dentist> Dentists { get; set; }
        public virtual DbSet<Intervention> Interventions { get; set; }
        public virtual DbSet<MedicalRecord> MedicalRecords { get; set; }
        public virtual DbSet<Schedule> Schedules { get; set; }



    }
}
