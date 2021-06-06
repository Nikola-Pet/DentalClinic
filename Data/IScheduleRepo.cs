using Dental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dental.Data
{
    public interface IScheduleRepo
    {
        Task<Schedule> GetSchedulebyId(int? id);
    }
}
