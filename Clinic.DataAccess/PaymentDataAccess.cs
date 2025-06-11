using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Clinic.DataAccess.DTOs;

namespace Clinic.DataAccess
{
    public class PaymentDataAccess
    {
        public static async Task<List<PaymentHistoryDTO>> GetPaymentHistoryByPatientID(int patientID)
        {
            try
            {
                SqlParameter param = new SqlParameter("@PatientID", SqlDbType.Int) { Value = patientID };

                var dataTable = await SqlHelper.ExecuteDataTableAsync("SP_GetPaymentHistoryByPatientID", param);

                return ConvertDataTableToPaymentHistoryDTOs(dataTable);
            }
            catch
            {
                throw;
            }
        }

        public static async Task<PaymentHistoryDTO> GetPaymentHistoryByAppointmentID(int appointmentID)
        {
            try
            {
                SqlParameter param = new SqlParameter("@AppointmentID", SqlDbType.Int) { Value = appointmentID };

                PaymentHistoryDTO paymentHistoryDTO = null;
                 await SqlHelper.ExecuteReaderAsync("SP_GetPaymentHistoryByAppointmentID",reader =>
                        {
                            if(reader.Read())
                            {
                                paymentHistoryDTO = new PaymentHistoryDTO(
                                    reader.GetInt32(reader.GetOrdinal("PaymentID")),
                                    DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("PaymentDate"))),
                                    reader["PaymentMethod"] == DBNull.Value ? string.Empty : reader["PaymentMethod"].ToString(),
                                    reader.GetDecimal(reader.GetOrdinal("AmountPaid")),
                                    reader["AdditionalNotes"] == DBNull.Value ? string.Empty : reader["AdditionalNotes"].ToString(),
                                    reader.GetString(reader.GetOrdinal("PatientName")),
                                    reader.GetString(reader.GetOrdinal("DoctorName")),
                                    reader.GetByte(reader.GetOrdinal("AppointmentStatus"))
                                );
                            }
                        }
                    , param);

                return paymentHistoryDTO;
            }
            catch
            {
                throw;
            }
        }


        public static async Task<PaymentDTO> GetPaymentByID(int id)
        {
            try
            {
                SqlParameter param = new SqlParameter("@PaymentID", SqlDbType.Int) { Value = id };

                PaymentDTO paymentDTO = null;
                int additionalNotesOrdinal = 0,PaymentMethodOrdinal = 0;
                await SqlHelper.ExecuteReaderAsync("SP_GetPaymentByID", reader =>
                {
                    if (reader.Read())
                    {
                        additionalNotesOrdinal = reader.GetOrdinal("AdditionalNotes");
                        PaymentMethodOrdinal = reader.GetOrdinal("PaymentMethod");

                        paymentDTO = new PaymentDTO(
                            reader.GetInt32(reader.GetOrdinal("PaymentID")),
                            DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("PaymentDate"))),
                            reader["PaymentMethod"] == DBNull.Value ? string.Empty : reader["PaymentMethod"].ToString(),
                            reader.GetDecimal(reader.GetOrdinal("AmountPaid")),
                            reader["AdditionalNotes"] == DBNull.Value ? string.Empty : reader["AdditionalNotes"].ToString(),
                        reader.GetInt32(reader.GetOrdinal("AppointmentID"))
                        );
                    }
                }
                   , param);

                return paymentDTO;
            }
            catch
            {
                throw;
            }
        }

        public static async Task<List<PaymentHistoryDTO>> GetPaymentHistoryWithinDateRange(DateOnly start, DateOnly end)
        {
            try
            {
                SqlParameter startParam = new SqlParameter("@Start", SqlDbType.Date) { Value = start };
                SqlParameter endParam = new SqlParameter("@End", SqlDbType.Date) { Value = end };

                var dataTable = await SqlHelper.ExecuteDataTableAsync("SP_GetPaymentHistoryWithinDateRange", startParam, endParam);

                return ConvertDataTableToPaymentHistoryDTOs(dataTable);
            }
            catch
            {
                throw;
            }
        }


        public static async Task<bool> IsAppointmentPaidFor(int appointmentID)
        {
            try
            {
                var idParam = new SqlParameter("@AppointmentID", SqlDbType.NVarChar) { Value = appointmentID };

                var paymentID = await SqlHelper.ExecuteScalarAsync<int>("SP_IsAppointmentPaidFor", idParam);
                return paymentID != 0;
            }
            catch
            {
                throw;
            }
        }


        public static async Task<int> AddNewPaymentRecord(PaymentDTO paymentDTO)
        {
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@PaymentDate", SqlDbType.Date) { Value = paymentDTO.PaymentDate },
                    new SqlParameter("@PaymentID", SqlDbType.Int) {Direction = ParameterDirection.Output },
                    new SqlParameter("@PaymentMethod", SqlDbType.NVarChar)
                    { Value = string.IsNullOrEmpty(paymentDTO.PaymentMethod) ? DBNull.Value:paymentDTO.PaymentMethod },
                    new SqlParameter("@AmountPaid", SqlDbType.Decimal) { Value = paymentDTO.AmountPaid },
                    new SqlParameter("@AdditionalNotes", SqlDbType.NVarChar) 
                    { Value = string.IsNullOrEmpty(paymentDTO.AdditionalNotes) ? DBNull.Value:paymentDTO.AdditionalNotes },
                    new SqlParameter("@AppointmentID", SqlDbType.Int) { Value = paymentDTO.AppointmentID }
                };

                return await SqlHelper.ExecuteWithOutputParameterAsync<int>("SP_AddNewPaymentRecord", parameters);
            }
            catch
            {
                throw;
            }
        }

        private static List<PaymentHistoryDTO> ConvertDataTableToPaymentHistoryDTOs(DataTable dataTable)
        {
            var list = new List<PaymentHistoryDTO>();

            if (dataTable?.Rows == null || dataTable.Rows.Count == 0) return list;

            var paymentMethodOrdinal = dataTable.Columns["PaymentMethod"].Ordinal;
            var additionalNotesOrdinal = dataTable.Columns["AdditionalNotes"].Ordinal;


            foreach (DataRow row in dataTable.Rows)
            {
                list.Add(new PaymentHistoryDTO(
                    Convert.ToInt32(row["PaymentID"]),
                    DateOnly.FromDateTime(Convert.ToDateTime(row["PaymentDate"])),
                    row.Field<string>(paymentMethodOrdinal) ?? string.Empty,
                    Convert.ToDecimal(row["AmountPaid"]),
                    row.Field<string>(additionalNotesOrdinal) ?? string.Empty,
                    row["PatientName"]?.ToString() ?? string.Empty,
                    row["DoctorName"]?.ToString() ?? string.Empty,
                    Convert.ToByte(row["AppointmentStatus"])
                ));
            }

            return list;
        }
    }
}
