using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.DataAccess.DTOs
{
    public class AppointmentDTO
    {
        public int AppointmentID { get; set; }

        public int DoctorID { get; set; }

        public int PatientID { get; set; }

        public DateTime AppointmentDateTime { get; set; }

        public byte AppointmentStatus { get; set; }

        
        public AppointmentDTO(int appointmentid, int doctorId, 
            int patientId, DateTime appointmentdateTime, byte appointmentStatus)
        {
            AppointmentID = appointmentid;
            DoctorID = doctorId;
            PatientID = patientId;
            AppointmentDateTime = appointmentdateTime;
            AppointmentStatus = appointmentStatus;
        } 

        public bool IsValid()
        {
            return AppointmentDateTime.Hour >= DateTime.Now.Hour
                 && AppointmentDateTime > DateTime.Now
                 && AppointmentStatus < 7
                 && AppointmentStatus > 1
                 && PatientID > 0
                 && DoctorID > 0
                 && AppointmentID > 0;
        }

    }
}
