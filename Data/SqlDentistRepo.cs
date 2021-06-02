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
    }
}
