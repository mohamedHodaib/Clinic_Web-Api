using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.DataAccess.DTOs
{
    public class PaymentHistoryDTO
    {
        public int PaymentID { get; set; }
        public DateOnly PaymentDate { get; set; }
        public string PaymentMethod { get; set; }
        public decimal AmountPaid { get; set; }
        public string AdditionalNotes { get; set; }
        public string PatientName { get; set; }
        public string DoctorName { get; set; }
        public byte AppointmentStatus { get; set; }

        public PaymentHistoryDTO(
            int paymentID,
            DateOnly paymentDate,
            string paymentMethod,
            decimal amountPaid,
            string additionalNotes,
            string patientName,
            string doctorName,
            byte appointmentStatus)
        {
            PaymentID = paymentID;
            PaymentDate = paymentDate;
            PaymentMethod = paymentMethod;
            AmountPaid = amountPaid;
            AdditionalNotes = additionalNotes;
            PatientName = patientName;
            DoctorName = doctorName;
            AppointmentStatus = appointmentStatus;
        }

       

    }
}
