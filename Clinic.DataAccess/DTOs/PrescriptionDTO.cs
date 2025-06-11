using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.DataAccess.DTOs
{
    public class PrescriptionDTO
    {
        public int PrescriptionID { get; set; }
        public int MedicalRecordID { get; set; }
        public string MedicationName { get; set; }
        public string Dosage { get; set; }
        public string Frequency { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public string SpecialInstructions { get; set; }


        public PrescriptionDTO(int prescriptionID, int medicalRecordID, string medicationName, string dosage,
            string frequency, DateOnly startDate, DateOnly endDate, string specialInstructions)
        {
            PrescriptionID = prescriptionID;
            MedicalRecordID = medicalRecordID;
            MedicationName = medicationName;
            Dosage = dosage;
            Frequency = frequency;
            StartDate = startDate;
            EndDate = endDate;
            SpecialInstructions = specialInstructions;
        }

        public bool IsValid() =>
            MedicalRecordID > 0
            && !string.IsNullOrEmpty(MedicationName)
            && !string.IsNullOrEmpty(Dosage)
            && !string.IsNullOrEmpty(Frequency)
            && StartDate >= DateOnly.FromDateTime(DateTime.Now.Date)
            && EndDate > DateOnly.FromDateTime(DateTime.Now.Date)
            && StartDate != EndDate;    


    }
}
