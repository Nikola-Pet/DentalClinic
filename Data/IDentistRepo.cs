using Dental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dental.Data
{
    public interface IDentistRepo
    {
        string GetDentistFullNamebyId(int id);
        List<Dentist> AllDentists();
        Dentist GetDentistbyFullName(string fn, string ln);
    }
}
