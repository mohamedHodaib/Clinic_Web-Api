using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.DataAccess.DTOs
{
    namespace Clinic.DataAccess.DTOs
    {
        public class PrescriptionInfoDTO
        {
            public int PrescriptionID { get; set; }
            public string Diagnosis { get; set; }
            public string MedicationName { get; set; }
            public string Dosage { get; set; }
            public string Frequency { get; set; }
            public DateOnly StartDate { get; set; }
            public DateOnly EndDate { get; set; }
            public string SpecialInstructions { get; set; }
            public string PatientName { get; set; }
            public string DoctorName { get; set; }

           
            public PrescriptionInfoDTO(
                int prescriptionID,
                string diagnosis,
                string medicationName,
                string dosage,
                string frequency,
                DateOnly startDate,
                DateOnly endDate,
                string specialInstructions,
                string patientName,
                string doctorName)
            {
                PrescriptionID = prescriptionID;
                Diagnosis = diagnosis;
                MedicationName = medicationName;
                Dosage = dosage;
                Frequency = frequency;
                StartDate = startDate;
                EndDate = endDate;
                SpecialInstructions = specialInstructions;
                PatientName = patientName;
                DoctorName = doctorName;
            }
        }
    }

}
