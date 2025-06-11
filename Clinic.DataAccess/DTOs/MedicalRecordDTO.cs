using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.DataAccess.DTOs
{
    public class MedicalRecordDTO
    {
        public int MedicalRecordID { get; set; }
        public string VisitDescription { get; set; }
        public string Diagnosis { get; set; }
        public string AdditionalNotes { get; set; }
        public int AppointmentID { get; set; }


        public MedicalRecordDTO(int medicalRecordID, string visitDescription,
            string diagnosis, string additionalNotes, int appointmentID)
        {
            MedicalRecordID = medicalRecordID;
            VisitDescription = visitDescription;
            Diagnosis = diagnosis;
            AdditionalNotes = additionalNotes;
            AppointmentID = appointmentID;
        }

        public bool IsValid() => AppointmentID > 0;
    }
}
