using Dental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dental.Data
{
    public class SqlMedicalRecordRepo : IMedicalRecordRepo
    {
        private readonly DentalClinicContext _context;
        public SqlMedicalRecordRepo(DentalClinicContext context)
        {
            _context = context;
        }

        public async Task AddMedicalRecordAsync(int patientId)
        {
           

            MedicalRecord medicalRecord = new MedicalRecord();
            medicalRecord.PatientId = patientId;
            _context.Add(medicalRecord);
            await _context.SaveChangesAsync();
        }

        public int GetMedicalRecordNumberbyId (int id)
        {
            var mr = _context.MedicalRecords.FirstOrDefault(x => x.MedicalRecordNumber == id);
            int mrNumber = mr.MedicalRecordNumber;
            return mrNumber;
        }

        public MedicalRecord GetMedicalRecordbyPatientId(int id)
        {
            var mr = _context.MedicalRecords.FirstOrDefault(x => x.PatientId == id);
            return mr;
        }
    }
}
