using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dental.Data
{
    public class SqlScheduleRepo : IScheduleRepo
    {
        private readonly DentalClinicContext _context;

        public SqlScheduleRepo(DentalClinicContext context)
        {
            _context = context;
        }
    }
}
