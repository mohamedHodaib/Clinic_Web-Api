using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.DataAccess.DTOs
{
    public class MedicalRecordInfoDTO
    {
        public int MedicalRecordID { get; }
        public string PatientName { get; }
        public string DoctorName { get; }
        public string VisitDescription { get; }
        public string Diagnosis { get; }
        public string AdditionalNotes { get; }
        public DateTime MedicalRecordDateTime { get; }

        public MedicalRecordInfoDTO(
            int medicalRecordID,
            string patientName,
            string doctorName,
            string visitDescription,
            string diagnosis,
            string additionalNotes,
            DateTime medicalRecordDateTime)
        {
            MedicalRecordID = medicalRecordID;
            PatientName = patientName ?? string.Empty;
            DoctorName = doctorName ?? string.Empty;
            VisitDescription = visitDescription ?? string.Empty;
            Diagnosis = diagnosis ?? string.Empty;
            AdditionalNotes = additionalNotes ?? string.Empty;
            MedicalRecordDateTime = medicalRecordDateTime;
        }

        public bool IsValid() =>
            string.IsNullOrEmpty(PatientName)
            && string.IsNullOrEmpty(DoctorName)
            && string.IsNullOrEmpty(VisitDescription)
            && string.IsNullOrEmpty(Diagnosis)
            && string.IsNullOrEmpty(AdditionalNotes)
            && MedicalRecordDateTime != default;
    }

}
