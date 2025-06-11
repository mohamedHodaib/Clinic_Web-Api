using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.DataAccess.DTOs
{
    public class PaymentDTO
    {
        public int PaymentID { get; set; }
        public DateOnly PaymentDate { get; set; }
        public string PaymentMethod { get; set; }
        public decimal AmountPaid { get; set; }
        public string AdditionalNotes { get; set; }
        public int AppointmentID { get; set; }

        public PaymentDTO(int paymentID, DateOnly paymentDate,
            string paymentMethod, decimal amountPaid, string additionalNotes, int appointmentID)
        {
            PaymentID = paymentID;
            PaymentDate = paymentDate;
            PaymentMethod = paymentMethod;
            AmountPaid = amountPaid;
            AdditionalNotes = additionalNotes;
            AppointmentID = appointmentID;
        }


        public bool IsValid() =>
           PaymentDate >= DateOnly.FromDateTime(DateTime.Now.Date)
           && AmountPaid > 0
           && AppointmentID > 0;
    }
}
