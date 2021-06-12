using Dental.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dental.Data
{
    public interface IScheduleRepo
    {
        Task<Schedule> GetSchedulebyId(int? id);
        IEnumerable GetSchedulebyDentistId(int id);
        IEnumerable GetSchedulebyPatientId(int id);
        Task SaveChanges();
        void UpdateSchedule(Schedule schedule);
        void CreateSchedule(Schedule schedule);
        void DeleteSchedule(Schedule schedule);
        bool ScheduleExists(int id);
    }
}
