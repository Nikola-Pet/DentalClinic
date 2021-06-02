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


    }
}
