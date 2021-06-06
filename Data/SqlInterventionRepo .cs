using Dental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dental.Data
{
    public class SqlInerventionRepo : IInterventionRepo
    {
        private readonly DentalClinicContext _context;

        public SqlInerventionRepo(DentalClinicContext context)
        {
            _context = context;
        }

        public IEnumerable<Intervention> GetInterventionbyDentistId(int id)
        {
            return _context.Interventions.Where(x => x.DentistId == id);
        }

        public IEnumerable<Intervention> GetInterventionbyMedicalRecordId(int mrId)
        {
            return _context.Interventions.Where(x => x.MedicalRecordNumber == mrId);
        }

        public void AddInterverntion(Intervention intervention)
        {
            _context.Add(intervention);
        }

        public async Task SaveChanges()
        {
             await _context.SaveChangesAsync();
        }
    }
}
