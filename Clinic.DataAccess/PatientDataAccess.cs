using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Clinic.DataAccess.DTOs;
using Clinic.DataAccess.DTOs.Clinic.DataAccess.DTOs;

namespace Clinic.DataAccess
{
    public class PatientDataAccess
    {
        public static async Task<List<PatientInfoDTO>> GetAllPatients()
        {
            try
            {
                var dataTable = await SqlHelper.ExecuteDataTableAsync("SP_GetAllPatients");
                return ConvertDataTableToPatientInfoDTOs(dataTable);
            }
            catch
            {
                throw;
            }
        }

        public static async Task<List<PatientInfoDTO>> GetPatientsByGender(bool gender)
        {
            try
            {
                SqlParameter param = new SqlParameter("@Gender", SqlDbType.Bit) { Value = gender };
                var dataTable = await SqlHelper.ExecuteDataTableAsync("SP_GetPatientsByGender", param);
                return ConvertDataTableToPatientInfoDTOs(dataTable);
            }
            catch
            {
                throw;
            }
        }

        public static async Task<List<PatientInfoDTO>> GetPatientsByName(string name)
        {
            try
            {
                SqlParameter param = new SqlParameter("@Name", SqlDbType.NVarChar) { Value = name };
                var dataTable = await SqlHelper.ExecuteDataTableAsync("SP_GetPatientsByName", param);
                return ConvertDataTableToPatientInfoDTOs(dataTable);
            }
            catch
            {
                throw;
            }
        }

        public static async Task<List<PatientInfoDTO>> GetPatientsByAddress(string address)
        {
            try
            {
                SqlParameter param = new SqlParameter("@Address", SqlDbType.NVarChar) { Value = address };
                var dataTable = await SqlHelper.ExecuteDataTableAsync("SP_GetPatientsByAddress", param);
                return ConvertDataTableToPatientInfoDTOs(dataTable);
            }
            catch
            {
                throw;
            }
        }

        public static async Task<List<PatientInfoDTO>> GetPatientsByDateOfBirth(DateOnly dateOfBirth)
        {
            try
            {
                SqlParameter param = new SqlParameter("@DateOfBirth", SqlDbType.Date) { Value = dateOfBirth };
                var dataTable = await SqlHelper.ExecuteDataTableAsync("SP_GetPatientsByDateOfBirth", param);
                return ConvertDataTableToPatientInfoDTOs(dataTable);
            }
            catch
            {
                throw;
            }
        }

        public static async Task<PatientDTO> GetPatientInfoById(int id)
        {
            try
            {
                SqlParameter idParam = new SqlParameter("@PatientID", SqlDbType.Int) { Value = id };
                PatientDTO patientDTO = null;
                await SqlHelper.ExecuteReaderAsync("SP_GetPatientInfoById", reader =>
                {
                    if (reader.Read())
                    {
                        patientDTO = new PatientDTO(
                            id,
                            reader.GetInt32(reader.GetOrdinal("PersonID"))
                        );
                    }
                }, idParam);

                return patientDTO;
            }
            catch
            {
                throw;
            }
        }

        public static async Task<PatientDTO> GetPatientInfoByPersonID(int personId)
        {
            try
            {
                SqlParameter idParam = new SqlParameter("@PersonID", SqlDbType.Int) { Value = personId };
                PatientDTO patientDTO = null;
                await SqlHelper.ExecuteReaderAsync("SP_GetPatientInfoByPersonId", reader =>
                {
                    if (reader.Read())
                    {
                        patientDTO = new PatientDTO(
                             reader.GetInt32(reader.GetOrdinal("PatientID")),
                            personId
                        );
                    }
                }, idParam);

                return patientDTO;
            }
            catch
            {
                throw;
            }
        }

        public static async Task<PatientHistoryDTO> GetPatientHistorySummaryById(int id)
        {
            try
            {
                SqlParameter idParam = new SqlParameter("@PatientID", SqlDbType.Int) { Value = id };
                PatientHistoryDTO patientHistoryDTO = null;
                await SqlHelper.ExecuteReaderAsync("SP_GetPatientHistorySummary", reader =>
                {
                    if (reader.Read())
                    {
                        patientHistoryDTO = new PatientHistoryDTO(
                            id,
                            reader.GetInt32(reader.GetOrdinal("AppointmentsCount")),
                            reader.GetInt32(reader.GetOrdinal("CompletedAppointments")),
                            reader.GetInt32(reader.GetOrdinal("ConfirmedAppointments")),
                            reader.GetInt32(reader.GetOrdinal("PendingAppointments")),
                            reader.GetInt32(reader.GetOrdinal("CanceledAppointments")),
                            reader.GetInt32(reader.GetOrdinal("NoShowAppointments")),
                            reader.GetInt32(reader.GetOrdinal("MedicalRecordsCount")),
                            reader.GetInt32(reader.GetOrdinal("PrescriptionCount"))
                        );
                    }
                }, idParam);

                return patientHistoryDTO;
            }
            catch
            {
                throw;
            }
        }

        public static async Task<int> AddNewPatient(PatientDTO patientDTO)
        {
            try
            {
                SqlParameter personIDParam = new SqlParameter("@PersonID", SqlDbType.Int) { Value = patientDTO.PersonID };
                SqlParameter patientIDParam = new SqlParameter("@PatientID", SqlDbType.Int) { Direction = ParameterDirection.Output };
                return await SqlHelper.ExecuteWithOutputParameterAsync<int>("SP_AddNewPatient", personIDParam, patientIDParam);
            }
            catch
            {
                throw;
            }
        }

        public static async Task<bool> DeletePatient(int personID)
        {
            try
            {
                SqlParameter idParam = new SqlParameter("@PatientID", SqlDbType.Int) { Value = personID };
                int affectedRows = await SqlHelper.ExecuteNonQueryAsync("SP_DeletePatient", idParam);
                return affectedRows > 0;
            }
            catch
            {
                throw;
            }
        }

        private static List<PatientInfoDTO> ConvertDataTableToPatientInfoDTOs(DataTable dataTable)
        {
            var patients = new List<PatientInfoDTO>();
            if (dataTable?.Rows == null || dataTable.Rows.Count == 0) return patients;

            var dobOrdinal = dataTable.Columns["DateOfBirth"].Ordinal;
            var emailOrdinal = dataTable.Columns["Email"].Ordinal;
            var addressOrdinal = dataTable.Columns["Address"].Ordinal;


            foreach (DataRow row in dataTable.Rows)
            {
                patients.Add(new PatientInfoDTO(
                    Convert.ToInt32(row["PatientID"]),
                    Convert.ToInt32(row["PersonID"]),
                    row["Name"].ToString(),
                    DateOnly.FromDateTime(row.Field<DateTime?>(dobOrdinal) ?? DateTime.MinValue),
                    Convert.ToBoolean(row["Gender"]),
                    row["PhoneNumber"]?.ToString() ?? string.Empty,
                    row.Field<string>(emailOrdinal) ?? string.Empty,
                    row.Field<string>(addressOrdinal) ?? string.Empty
                ));
            }

            return patients;
        }
    }
}
