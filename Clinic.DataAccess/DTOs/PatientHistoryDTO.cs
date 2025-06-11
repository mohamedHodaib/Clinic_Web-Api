using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.DataAccess.DTOs
{
    public class PatientHistoryDTO
    {
        public int PatientID { get; set; }
        public int AppointmentsCount { get; set; }
        public int CompletedAppointments { get; set; }
        public int ConfirmedAppointments { get; set; }
        public int PendingAppointments { get; set; }
        public int CanceledAppointments { get; set; }
        public int NoShowAppointments { get; set; }
        public int MedicalRecordsCount { get; set; }
        public int PrescriptionCount { get; set; }


        public PatientHistoryDTO(int patientID,int appointmentsCount, int completedAppointments, int confirmedAppointments, int pendingAppointments,
            int canceledAppointments, int noShowAppointments, int medicalRecordsCount, int prescriptionCount)
        {
            PatientID = patientID;
            AppointmentsCount = appointmentsCount;
            CompletedAppointments = completedAppointments;
            ConfirmedAppointments = confirmedAppointments;
            PendingAppointments = pendingAppointments;
            CanceledAppointments = canceledAppointments;
            NoShowAppointments = noShowAppointments;
            MedicalRecordsCount = medicalRecordsCount;
            PrescriptionCount = prescriptionCount;

        }
    }
}
