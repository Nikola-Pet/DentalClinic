using Dental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dental.Data
{
    public interface IMedicalRecordRepo
    {
        Task AddMedicalRecordAsync(int patientId);
        int GetMedicalRecordNumberbyId(int id);
        MedicalRecord GetMedicalRecordbyPatientId(int id);
    }
}
