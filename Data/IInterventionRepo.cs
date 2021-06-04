using Dental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dental.Data
{
    public interface IInterventionRepo
    {
        IEnumerable<Intervention> GetInterventionbyDentistId(int id);
        IEnumerable<Intervention> GetInterventionbyMedicalRecordId(int mrId);
    }
}
