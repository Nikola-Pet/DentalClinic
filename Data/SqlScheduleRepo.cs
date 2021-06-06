using Dental.Models;
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

        public async Task<Schedule> GetSchedulebyId (int? id)
        {
            
            return await _context.Schedules.FindAsync(id);
        }
    }
}
