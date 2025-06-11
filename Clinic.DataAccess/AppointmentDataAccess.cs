using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Linq.Expressions;
using Clinic.DataAccess.DTOs;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Clinic.DataAccess
{
    

    public class AppointmentDataAccess
    {
        #region Public Methods

        public static async Task<List<AppointmentReportDTO>> GetAllAppointmentsAsync()
        {
            try
            {
                var dataTable = await SqlHelper.ExecuteDataTableAsync("SP_GetAllAppointments");
                return ConvertDataTableToAppointmentReportDTOs(dataTable);
            }
            catch
            {
                throw;
            }
        }

        public static async Task<List<AppointmentReportDTO>> GetAppointmentsByStatusAsync(byte status)
        {
            try
            {
                var param = new SqlParameter("@AppointmentStatus", SqlDbType.TinyInt) { Value = status };
                var dataTable = await SqlHelper.ExecuteDataTableAsync("SP_GetAppointmentsByStatus", param);
                return ConvertDataTableToAppointmentReportDTOs(dataTable);
            }
            catch
            {
                throw;
            }
        }

        public static async Task<List<AppointmentReportDTO>> GetAppointmentsByDateAsync(DateOnly appointmentDate)
        {
            try
            {
                var param = new SqlParameter("@AppointmentDate", SqlDbType.Date) { Value = appointmentDate };
                var dataTable = await SqlHelper.ExecuteDataTableAsync("SP_GetAppointmentsByDate", param);
                return ConvertDataTableToAppointmentReportDTOs(dataTable);
            }
            catch
            {
                throw;
            }
        }

        public static async Task<List<AppointmentReportDTO>> GetAppointmentsByPatientIdAsync(int patientId)
        {
            if (patientId <= 0)
                throw new ArgumentException("Patient ID must be greater than zero", nameof(patientId));

            try
            {
                var param = new SqlParameter("@PatientID", SqlDbType.Int) { Value = patientId };
                var dataTable = await SqlHelper.ExecuteDataTableAsync("SP_GetAppointmentsByPatientID", param);
                return ConvertDataTableToAppointmentReportDTOs(dataTable);
            }
            catch
            {
                throw;
            }
        }

        public static async Task<List<AppointmentReportDTO>> GetAppointmentsByDoctorIdAsync(int doctorId)
        {
            if (doctorId <= 0)
                throw new ArgumentException("Doctor ID must be greater than zero", nameof(doctorId));

            try
            {
                var param = new SqlParameter("@DoctorID", SqlDbType.Int) { Value = doctorId };
                var dataTable = await SqlHelper.ExecuteDataTableAsync("SP_GetAppoinmentsByDoctorID", param);
                return ConvertDataTableToAppointmentReportDTOs(dataTable);
            }
            catch
            {
                throw;
            }
        }

        public static async Task<List<AppointmentReportDTO>> GetAppointmentsByPatientAndDoctorAsync(int patientId, int doctorId)
        {
            if (patientId <= 0)
                throw new ArgumentException("Patient ID must be greater than zero", nameof(patientId));
            if (doctorId <= 0)
                throw new ArgumentException("Doctor ID must be greater than zero", nameof(doctorId));

            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@PatientID", SqlDbType.Int) { Value = patientId },
                    new SqlParameter("@DoctorID", SqlDbType.Int) { Value = doctorId }
                };

                var dataTable = await SqlHelper.ExecuteDataTableAsync("SP_GetAppointmentsByPatientIDAndDoctorID", parameters);
                return ConvertDataTableToAppointmentReportDTOs(dataTable);
            }
            catch
            {
                throw;
            }
        }

        public static async Task<List<AppointmentReportDTO>> GetDoctorSchedulePerDayAsync(int doctorId, DateOnly day)
        {
            if (doctorId <= 0)
                throw new ArgumentException("Doctor ID must be greater than zero", nameof(doctorId));

            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@AppointmentDate", SqlDbType.Date) { Value = day },
                    new SqlParameter("@DoctorID", SqlDbType.Int) { Value = doctorId }
                };

                var dataTable = await SqlHelper.ExecuteDataTableAsync("SP_GetDoctorSchedulePerDay", parameters);
                return ConvertDataTableToAppointmentReportDTOs(dataTable);
            }
            catch
            {
                throw;
            }
        }

        public static async Task<AppointmentDTO> GetAppointmentByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Appointment ID must be greater than zero", nameof(id));

            try
            {
                var param = new SqlParameter("@AppointmentID", SqlDbType.Int) { Value = id };
                AppointmentDTO appointment = null;

                await SqlHelper.ExecuteReaderAsync("SP_GetAppointmentInfoById", reader =>
                {
                    if (reader.Read())
                    {
                        appointment = MapReaderToAppointment(reader, id);
                    }
                }, param);

                return appointment;
            }
            catch
            {
                throw ;
            }
        }

        public static async Task<AppointmentDTO> GetAppointmentByDateTimeAsync(DateTime dateTime)
        {
            try
            {
                var param = new SqlParameter("@AppointmentDateTime", SqlDbType.DateTime) { Value = dateTime };
                AppointmentDTO appointment = null;

                await SqlHelper.ExecuteReaderAsync("SP_GetAppointmentInfoByDateTime", reader =>
                {
                    if (reader.Read())
                    {
                        appointment = MapReaderToAppointment(reader);
                    }
                }, param);

                return appointment;
            }
            catch
            {
                throw;
            }
        }

        public static async Task<int> AddAppointmentAsync(AppointmentDTO appointment)
        {
            if (appointment == null)
                throw new ArgumentNullException(nameof(appointment));


            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@AppointmentID", SqlDbType.Int) { Direction = ParameterDirection.Output },
                    new SqlParameter("@PatientID", SqlDbType.Int) { Value = appointment.PatientID },
                    new SqlParameter("@DoctorID", SqlDbType.Int) { Value = appointment.DoctorID },
                    new SqlParameter("@AppointmentDateTime", SqlDbType.DateTime) { Value = appointment.AppointmentDateTime },
                    new SqlParameter("@AppointmentStatus", SqlDbType.TinyInt) { Value = appointment.AppointmentStatus }
                };

                return await SqlHelper.ExecuteWithOutputParameterAsync<int>("SP_AddNewAppointment", parameters);
            }
            catch
            {
                throw;
            }
        }

        public static async Task<bool> UpdateAppointmentAsync(AppointmentDTO appointment)
        {
            if (appointment == null)
                throw new ArgumentNullException(nameof(appointment));


            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@AppointmentID", SqlDbType.Int) { Value = appointment.AppointmentID },
                    new SqlParameter("@PatientID", SqlDbType.Int) { Value = appointment.PatientID },
                    new SqlParameter("@DoctorID", SqlDbType.Int) { Value = appointment.DoctorID },
                    new SqlParameter("@AppointmentDateTime", SqlDbType.DateTime) { Value = appointment.AppointmentDateTime },
                    new SqlParameter("@AppointmentStatus", SqlDbType.TinyInt) { Value = appointment.AppointmentStatus }
                };

                var rowsAffected = await SqlHelper.ExecuteNonQueryAsync("SP_UpdateAppointment", parameters);
                return rowsAffected > 0;
            }
            catch
            {
                throw;
            }
        }



        public static async Task<bool> UpdateAppointmentStatusAsync(int appointmentId, byte status)
        {
            if (appointmentId <= 0)
                throw new ArgumentException("Appointment ID must be greater than zero", nameof(appointmentId));

            try
            {
                var parameters = new[]
                {
                    new SqlParameter("@AppointmentID", SqlDbType.Int) { Value = appointmentId },
                    new SqlParameter("@AppointmentStatus", SqlDbType.TinyInt) { Value = status }
                };

                var rowsAffected = await SqlHelper.ExecuteNonQueryAsync("SP_Updatebyte", parameters);
                return rowsAffected > 0;
            }
            catch
            {
                throw;
            }
        }

        public static async Task<bool> DeleteAppointmentAsync(int appointmentId)
        {
            if (appointmentId <= 0)
                throw new ArgumentException("Appointment ID must be greater than zero", nameof(appointmentId));

            try
            {
                var param = new SqlParameter("@AppointmentID", SqlDbType.Int) { Value = appointmentId };
                var rowsAffected = await SqlHelper.ExecuteNonQueryAsync("SP_DeleteAppointment", param);
                return rowsAffected > 0;
            }
            catch
            {
                throw;
            }
        }


        public static async Task<bool> IsReserved(DateTime dateTime)
        {
            try
            {
                var param = new SqlParameter("@AppointmentDateTime", SqlDbType.DateTime) { Value = dateTime };
                var appointmentID = await SqlHelper.ExecuteScalarAsync<int>("SP_IsAppointmentReserved", param);
                return appointmentID != 0;
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region Private Helper Methods

        private static List<AppointmentReportDTO> ConvertDataTableToAppointmentReportDTOs(DataTable dataTable)
        {
            var appointments = new List<AppointmentReportDTO>();

            if (dataTable?.Rows == null) return appointments;

            foreach (DataRow row in dataTable.Rows)
            {
                appointments.Add(new AppointmentReportDTO
                (
                    Convert.ToInt32(row["AppointmentID"]),
                    row["DoctorName"].ToString(),
                    row["PatientName"].ToString(),
                    TimeOnly.FromTimeSpan((TimeSpan)row["AppointmentTime"]),
                    Convert.ToByte(row["AppointmentStatus"])
                ));
            }

            return appointments;
        }

        private static AppointmentDTO MapReaderToAppointment(IDataReader reader, int? appointmentId = null)
        {
            return new AppointmentDTO(
                appointmentId ?? reader.GetInt32(reader.GetOrdinal("AppointmentID")),
                reader.GetInt32(reader.GetOrdinal("DoctorID")),
                reader.GetInt32(reader.GetOrdinal("PatientID")),
                reader.GetDateTime(reader.GetOrdinal("AppointmentDateTime")),
                reader.GetByte(reader.GetOrdinal("AppointmentStatus"))
                );
        }

        #endregion
    }
}
