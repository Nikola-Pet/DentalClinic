using Dental.Models;
using Microsoft.EntityFrameworkCore;
using System;
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

        public Task<Patient> GetPatientbyId (int id)
        {
            return _context.Patients.FirstOrDefaultAsync(m => m.PatientId == id);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public void UpdatePatient(Patient patient)
        {
             _context.Update(patient);
        }
        public void DeletePatient(Patient patient)
        {
            _context.Remove(patient);
        }
        public bool PatientExists(int id)
        {
            return _context.Patients.Any(e => e.PatientId == id);
        }

    }
}
