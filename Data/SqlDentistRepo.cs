using Dental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dental.Data
{
    public class SqlDentistRepo : IDentistRepo
    {
        private readonly DentalClinicContext _context;

        public SqlDentistRepo(DentalClinicContext context)
        {
            _context = context;
        }

        public string GetDentistFullNamebyId(int id)
        {
            var dentist = _context.Dentists.FirstOrDefault(x => x.DentistId == id);
            string fullName = dentist.FirstName + " " + dentist.LastName;
            return fullName;
        }

        public List<Dentist> AllDentists()
        {
            return (from x in _context.Dentists select new Dentist { FirstName = x.FirstName, LastName = x.LastName }).ToList();
        }

        public Dentist GetDentistbyFullName(string fn, string ln)
        {
            return _context.Dentists.FirstOrDefault(x => x.FirstName == fn && x.LastName == ln);
        }
    }
}
