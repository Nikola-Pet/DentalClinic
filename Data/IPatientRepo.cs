using Dental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dental.Data
{
    public interface IPatientRepo
    {
        string GetPatienttFullNamebyId(int id);
        Task<Patient> GetPatientbyId(int id);
        Task UpdatePatient(Patient patient);
        Task SaveChanges();
    }
}
