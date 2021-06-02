using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dental.Data
{
    public interface IPatientRepo
    {
        string GetPatienttFullNamebyId(int id);
    }
}
