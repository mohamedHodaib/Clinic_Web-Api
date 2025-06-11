using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.DataAccess.DTOs
{
    public class AppointmentReportDTO
    {
        public int AppointmentID { get; set; }

        public string DoctorName { get; set; }

        public string PatientName { get; set; }

        public TimeOnly AppointmentTime { get; set; }

        public byte AppointmentStatus { get; set; }

        public AppointmentReportDTO(int appointmentid, string doctorName, string patientName, 
            TimeOnly appointmentTime, byte appointmentStatus)
        {
            AppointmentID = appointmentid;
            DoctorName = doctorName;
            PatientName = patientName;
            AppointmentTime = appointmentTime;
            AppointmentStatus = appointmentStatus;
        }


        public bool IsValid()
        {
            return AppointmentID > 0 &&
                   DoctorName != null &&
                   PatientName != null &&
                   AppointmentTime != default ;
        }
    }
}
