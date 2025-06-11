using Clinic.DataAccess;
using Clinic.DataAccess.DTOs;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Clinic.Business
{
    public class Appointment
    {
        public enum enMode { AddNew ,Update};
        public enMode Mode = enMode.AddNew;

        private static string _ExceptionMessage = string.Empty;

        public enum AppointmentStatus : byte
        {
            ReScheduled = 1,
            Confirmed = 2,
            Pending = 3,
            Completed = 4,
            Cancelled = 5,
            NoShow = 6
        }

        public AppointmentDTO appointmentDTO => new AppointmentDTO(AppointmentID, DoctorID
            , PatientID, AppointmentDateTime, Status);

        public int AppointmentID { get; set; }

        public int DoctorID { get; set; }

        public int PatientID { get; set; }

        public DateTime AppointmentDateTime { get; set; }

        public byte Status { get; set; }

        public int PaymentID { get; set; }

        public Appointment(AppointmentDTO appointmentDTO,enMode mode = enMode.AddNew)
        {
            AppointmentID = appointmentDTO.AppointmentID;
            DoctorID = appointmentDTO.DoctorID;
            PatientID = appointmentDTO.PatientID;
            AppointmentDateTime = appointmentDTO.AppointmentDateTime;
            if (mode == enMode.AddNew)
                Status = (byte)AppointmentStatus.Pending;
            else
                Status = appointmentDTO.AppointmentStatus;

            Mode = mode;
        }


        public static async Task<Appointment> GetAppointmentByAppointmentID(int appointmentID)
        {
            try
            {
               AppointmentDTO appointmentDTO = 
                    await AppointmentDataAccess.GetAppointmentByIdAsync(appointmentID);

                if (appointmentDTO != null)
                    return new Appointment(appointmentDTO, enMode.Update);
                else
                    return null;
            }
            catch (SqlException ex)
            {
                _ExceptionMessage = ex.Message;
                 Global.Log(_ExceptionMessage, EventLogEntryType.Error);
                throw ;
            }
            catch (Exception ex)
            {
                _ExceptionMessage = ex.Message;
                Global.Log(_ExceptionMessage, EventLogEntryType.Error);
                throw;
            }

        }


        public static async Task<List<AppointmentReportDTO>> GetAppointmentsByPatientID(int patientID)
        {
            try
            {
                return await AppointmentDataAccess.GetAppointmentsByPatientIdAsync(patientID);
            }
            catch (SqlException ex)
            {
                _ExceptionMessage = ex.Message;
                Global.Log(_ExceptionMessage, EventLogEntryType.Error);
                throw ;
            }
            catch (Exception ex)
            {
                _ExceptionMessage = ex.Message;
                Global.Log(_ExceptionMessage, EventLogEntryType.Error);
                throw;
            }

        }

        public static async Task<List<AppointmentReportDTO>> GetAppointmentsByDoctorID(int doctorID)
        {
            try
            {
                return await AppointmentDataAccess.GetAppointmentsByDoctorIdAsync(doctorID);
            }
            catch (SqlException ex)
            {
                _ExceptionMessage = ex.Message;
                Global.Log(_ExceptionMessage, EventLogEntryType.Error);
                throw ;
            }
            catch (Exception ex)
            {
                _ExceptionMessage = ex.Message;
                Global.Log(_ExceptionMessage, EventLogEntryType.Error);
                throw;
            }
        }

        public static async Task<List<AppointmentReportDTO>> GetAppointmentsByDate(DateOnly date)
        {
            try
            {
               return await AppointmentDataAccess.GetAppointmentsByDateAsync(date);
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


        public static async Task<List<AppointmentReportDTO>> GetAppointmentsByStatus(byte status)
        {
            try
            {
                return await AppointmentDataAccess.GetAppointmentsByStatusAsync(status);
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


        public static async Task<List<AppointmentReportDTO>> GetDoctorSchedulePerDay(int doctorID,DateOnly day)
        {
            try
            {
                return await AppointmentDataAccess.GetDoctorSchedulePerDayAsync(doctorID,day);
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

        public static async Task<bool> IsReserved(DateTime dateTime)
        {
            try
            {
                return await AppointmentDataAccess.IsReserved(dateTime);
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

        private async Task<bool> _AddNewAppointment()
        {
            try
            {
                AppointmentID = await AppointmentDataAccess.AddAppointmentAsync(appointmentDTO);
                return AppointmentID != 0;
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

        private async Task<bool> _UpdateAppointment()
        {
            try
            {
                return await AppointmentDataAccess.UpdateAppointmentAsync(appointmentDTO);
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
                        if (await _AddNewAppointment())
                        {

                            Mode = enMode.Update;
                            return true;
                        }
                        else
                        {
                            return false;
                        }

                    case enMode.Update:

                        return await _UpdateAppointment();

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
