using Dental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dental.Data

{
    public interface IAuthenticate
    {
        Task AddPatientAsync(Patient patient);
        string GenerateToken(string id, string eMail, string rola);
        public string ValidateRoleJwtToken(string token);
        string ValidateEmailJwtToken(string token);
        string ValidateIdJwtToken(string token);
        Task<Patient> GetPatientAsync(string eMail, string password);
        Task<Dentist> GetDentistAsync(string eMail, string password);
       
    }
}
