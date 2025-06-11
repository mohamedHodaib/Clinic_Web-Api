using Clinic.DataAccess.DTOs;
using Clinic.DataAccess;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Business
{
    public class Payment

    {
        public enum enMode { AddNew, Update };
        public enMode Mode = enMode.AddNew;

        private static string _ExceptionMessage = string.Empty;

        public PaymentDTO paymentDTO => new PaymentDTO(PaymentID,PaymentDate,PaymentMethod,
            AmountPaid,AdditionalNotes,AppointmentID);

        public int PaymentID { get; set; }
        public DateOnly PaymentDate { get; set; }
        public string PaymentMethod { get; set; }
        public decimal AmountPaid { get; set; }
        public string AdditionalNotes { get; set; }
        public int AppointmentID { get; set; }

        public Payment(PaymentDTO paymentDTO, enMode mode = enMode.AddNew)
        {
            PaymentID = paymentDTO.PaymentID;
            PaymentDate = paymentDTO.PaymentDate;
            PaymentMethod = paymentDTO.PaymentMethod;
            AmountPaid = paymentDTO.AmountPaid;
            AdditionalNotes = paymentDTO.AdditionalNotes;
            AppointmentID = paymentDTO.AppointmentID;

            Mode = mode;
        }


        public static async Task<Payment> GetPaymentByID(int id)
        {
            try
            {
                PaymentDTO paymentDTO =  await PaymentDataAccess.GetPaymentByID(id);

                if (paymentDTO != null)
                    return new Payment(paymentDTO, enMode.Update);
                else
                    return null;
            }
            catch (SqlException ex)
            {
                _ExceptionMessage = ex.Message;
                Global.Log(_ExceptionMessage, EventLogEntryType.Error);
                throw;
            }
            catch (Exception ex)
            {

                _ExceptionMessage = ex.Message;
                Global.Log(_ExceptionMessage, EventLogEntryType.Error);
                throw;
            }
        }

        public static async Task<List<PaymentHistoryDTO>> GetPaymentHistoryByPatientID(int patientID)
        {
            try
            {
              return  await PaymentDataAccess.GetPaymentHistoryByPatientID(patientID);
            }
            catch (SqlException ex)
            {
                _ExceptionMessage = ex.Message;
                Global.Log(_ExceptionMessage, EventLogEntryType.Error);
                throw;
            }
            catch (Exception ex)
            {

                _ExceptionMessage = ex.Message;
                Global.Log(_ExceptionMessage, EventLogEntryType.Error);
                throw;
            }
        }

        public static async Task<PaymentHistoryDTO> GetPaymentHistoryByAppointmentID(int appointmentID)
        {
            try
            {
               return await PaymentDataAccess.GetPaymentHistoryByAppointmentID(appointmentID);
            }
            catch (SqlException ex)
            {
                _ExceptionMessage = ex.Message;
                Global.Log(_ExceptionMessage, EventLogEntryType.Error);
                throw;
            }
            catch (Exception ex)
            {

                _ExceptionMessage = ex.Message;
                Global.Log(_ExceptionMessage, EventLogEntryType.Error);
                throw;
            }
        }

        public static async Task<List<PaymentHistoryDTO>> GetPaymentHistoryByWithinDateRange(DateOnly startDate,DateOnly endDate)
        {
            try
            {
              return  await PaymentDataAccess.GetPaymentHistoryWithinDateRange(startDate,endDate);
            }
            catch (SqlException ex)
            {
                _ExceptionMessage = ex.Message;
                Global.Log(_ExceptionMessage, EventLogEntryType.Error);
                throw;
            }
            catch (Exception ex)
            {

                _ExceptionMessage = ex.Message;
                Global.Log(_ExceptionMessage, EventLogEntryType.Error);
                throw;
            }
        }

        public static async Task<bool> IsAppointmentPaidFor(int appointmentID)
        {
            try
            {
                return await PaymentDataAccess.IsAppointmentPaidFor(appointmentID);
            }
            catch (SqlException ex)
            {
                _ExceptionMessage = ex.Message;
                Global.Log(_ExceptionMessage, EventLogEntryType.Error);
                throw;
            }
            catch (Exception ex)
            {

                _ExceptionMessage = ex.Message;
                Global.Log(_ExceptionMessage, EventLogEntryType.Error);
                throw;
            }

        }

        private async Task<bool> _AddNewPaymentRecord()
        {
            try
            {
                PaymentID = await PaymentDataAccess.AddNewPaymentRecord(paymentDTO);
                return PaymentID != 0;
            }
            catch (SqlException ex)
            {
                _ExceptionMessage = ex.Message;
                Global.Log(_ExceptionMessage, EventLogEntryType.Error);
                throw;
            }
            catch (Exception ex)
            {

                _ExceptionMessage = ex.Message;
                Global.Log(_ExceptionMessage, EventLogEntryType.Error);
                throw;
            }
        }




        public async Task<bool> Save()
        {
            try
            {
                switch (Mode)
                {
                    case enMode.AddNew:
                        if (await _AddNewPaymentRecord())
                        {

                            Mode = enMode.Update;
                            return true;
                        }
                        else
                        {
                            return false;
                        }

                    case enMode.Update:

                        return true;//this feature not exist

                }

                return false;
            }
            
            catch (SqlException ex)
            {
                _ExceptionMessage = ex.Message;
                Global.Log(_ExceptionMessage, EventLogEntryType.Error);
                throw;
            }
            catch (Exception ex)
            {

                _ExceptionMessage = ex.Message;
                Global.Log(_ExceptionMessage, EventLogEntryType.Error);
                throw;
            }
        }
    }
}
