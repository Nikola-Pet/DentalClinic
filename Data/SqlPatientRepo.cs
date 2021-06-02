﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dental.Data
{
    public class SqlPatientRepo : IPatientRepo
    {
        private readonly DentalClinicContext _context;

        public SqlPatientRepo(DentalClinicContext context)
        {
            _context = context;
        }
        public string GetPatienttFullNamebyId(int id)
        {
            var patient = _context.Patients.FirstOrDefault(x => x.PatientId == id);
            string fullName = patient.FirstName + " " + patient.LastName;
            return fullName;
        }
    }
}
