using Dental.Models;
using System;
using System.Collections;
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
        public  IEnumerable GetSchedulebyDentistId(int id)
        {

            return  _context.Schedules.Where(x => x.DentistId == id);
        }

        public IEnumerable GetSchedulebyPatientId(int id)
        {
            return _context.Schedules.Where(x => x.PatientId == id);   
        }

        public void CreateSchedule(Schedule schedule)
        {
            _context.Add(schedule);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public void UpdateSchedule(Schedule schedule)
        {
            _context.Update(schedule);
        }

        public void DeleteSchedule(Schedule schedule)
        {
            _context.Schedules.Remove(schedule);
        }

        public bool ScheduleExists(int id)
        {
            return _context.Schedules.Any(e => e.ScheduleId == id);
        }
    }
}
