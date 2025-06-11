using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Clinic.DataAccess.DTOs;
using System.Reflection.PortableExecutable;

namespace Clinic.DataAccess
{
    public class MedicalRecordDataAccess
    {
        public static async Task<List<MedicalRecordInfoDTO>> GetAllMedicalRecords()
        {
            try
            {
                var dataTable = await SqlHelper.ExecuteDataTableAsync("SP_GetAllMedicalRecords");
                return ConvertDataTableToMedicalRecordInfoDTOs(dataTable);
            }
            catch
            {
                throw;
            }
        }

        public static async Task<List<MedicalRecordInfoDTO>> GetMedicalRecordsByPatientID(int patientID)
        {
            try
            {
                SqlParameter param = new SqlParameter("@PatientID", SqlDbType.Int) { Value = patientID };
                var dataTable = await SqlHelper.ExecuteDataTableAsync("SP_GetMedicalRecordsByPatientID", param);
                return ConvertDataTableToMedicalRecordInfoDTOs(dataTable);
            }
            catch
            {
                throw;
            }
        }

        public static async Task<List<MedicalRecordInfoDTO>> GetMedicalRecordsByDoctorID(int doctorID)
        {
            try
            {
                SqlParameter param = new SqlParameter("@DoctorID", SqlDbType.Int) { Value = doctorID };
                var dataTable = await SqlHelper.ExecuteDataTableAsync("SP_GetMedicalRecordsByDoctorID", param);
                return ConvertDataTableToMedicalRecordInfoDTOs(dataTable);
            }
            catch
            {
                throw;
            }
        }


        public static async Task<MedicalRecordDTO> GetMedicalRecordByID(int medicalRecordID)
        {
            try
            {
                SqlParameter param = new SqlParameter("@MedicalRecordID", SqlDbType.Int) { Value = medicalRecordID };

                MedicalRecordDTO medicalRecordDTO = null;

                await SqlHelper.ExecuteReaderAsync("SP_GetMedicalRecordByID", reader =>
                {
                    while (reader.Read())
                    {
                        medicalRecordDTO = new MedicalRecordDTO(
                                 reader.GetInt32(reader.GetOrdinal("MedicalRecordID")),
                                 reader["VisitDescription"] == DBNull.Value
                                     ? string.Empty
                                     : reader["VisitDescription"].ToString(),
                                     reader["Diagnosis"] == DBNull.Value
                                     ? string.Empty
                                     : reader["Diagnosis"].ToString(),
                                     reader.GetString(reader["AdditionalNotes"] == DBNull.Value
                                     ? string.Empty
                                     : reader["AdditionalNotes"].ToString()),
                                 reader.GetInt32(reader.GetOrdinal("AppointmentID"))
                        );
                    }
                }

                   , param
                );

                return medicalRecordDTO;
            }
            catch
            {
                throw;
            }
        }


        public static async Task<MedicalRecordInfoDTO> GetMedicalRecordByAppointmentID(int appointmentID)
        {
            try
            {
                SqlParameter param = new SqlParameter("@AppointmentID", SqlDbType.Int) { Value = appointmentID };

                MedicalRecordInfoDTO medicalRecordInfoDTO = null;

                 await SqlHelper.ExecuteReaderAsync("SP_GetMedicalRecordByAppointmentID",reader =>
                    {
                        while (reader.Read())
                        {
                            medicalRecordInfoDTO = new MedicalRecordInfoDTO(
                                     reader.GetInt32(reader.GetOrdinal("MedicalRecordID")),
                                     reader.GetString(reader.GetOrdinal("PatientName")),
                                     reader.GetString(reader.GetOrdinal("DoctorName")),
                                     reader["VisitDescription"] == DBNull.Value
                                     ? string.Empty
                                     : reader["VisitDescription"].ToString(),
                                     reader["Diagnosis"] == DBNull.Value
                                     ? string.Empty
                                     : reader["Diagnosis"].ToString(),
                                     reader.GetString(reader["AdditionalNotes"] == DBNull.Value
                                     ? string.Empty
                                     : reader["AdditionalNotes"].ToString()),
                                     reader.GetDateTime(reader.GetOrdinal("MedicalRecordDateTime"))
                            );
                        }
                    }
                    
                    , param
                 );

                return medicalRecordInfoDTO;
            }
            catch
            {
                throw;
            }
        }


        public static async Task<bool> IsAppointmentHaveMedicalRecord(int appointmentID)
        {
            try
            {
                var param = new SqlParameter("@AppointmentID", SqlDbType.Int) { Value = appointmentID };
                var MedicalRecordID = await SqlHelper.ExecuteScalarAsync<int>("SP_IsAppointmentHaveMedicalRecord", param);
                return MedicalRecordID != 0;
            }
            catch
            {
                throw;
            }
        }

        public static async Task<int> AddNewMedicalRecord(MedicalRecordDTO mRecordDTO)
        {
            try
            {
                SqlParameter visitDescriptionParam = new SqlParameter("@VisitDescription", SqlDbType.NVarChar) { Value = string.IsNullOrEmpty(mRecordDTO.VisitDescription) ? DBNull.Value 
                    : mRecordDTO.VisitDescription };
                SqlParameter diagnosisParam = new SqlParameter("@Diagnosis", SqlDbType.NVarChar) { Value = string.IsNullOrEmpty(mRecordDTO.Diagnosis) ? DBNull.Value
                    : mRecordDTO.Diagnosis
                };
                SqlParameter additionalNotesParam = new SqlParameter("@AdditionalNotes", SqlDbType.NVarChar) { Value = string.IsNullOrEmpty(mRecordDTO.AdditionalNotes) ? DBNull.Value
                    : mRecordDTO.AdditionalNotes
                };
                SqlParameter appointmentIDParam = new SqlParameter("@AppointmentID", SqlDbType.Int) { Value = mRecordDTO.AppointmentID };
                SqlParameter medicalRecordIDParam = new SqlParameter("@MedicalRecordID", SqlDbType.Int) { Direction = ParameterDirection.Output };

                return await SqlHelper.ExecuteWithOutputParameterAsync<int>("SP_AddNewMedicalRecord", visitDescriptionParam, diagnosisParam,
                    additionalNotesParam,appointmentIDParam,medicalRecordIDParam);
            }
            catch
            {
                throw;
            }
        }

        public static async Task<bool> UpdateMedicalRecord(MedicalRecordDTO mRecordDTO)
        {
            try
            {
                SqlParameter medicalRecordIDParam = new SqlParameter("@MedicalRecordID", SqlDbType.Int) { Value = mRecordDTO.MedicalRecordID };
                SqlParameter visitDescriptionParam = new SqlParameter("@VisitDescription", SqlDbType.NVarChar)
                {
                    Value = string.IsNullOrEmpty(mRecordDTO.VisitDescription) ? DBNull.Value
                     : mRecordDTO.VisitDescription
                };
                SqlParameter diagnosisParam = new SqlParameter("@Diagnosis", SqlDbType.NVarChar)
                {
                    Value = string.IsNullOrEmpty(mRecordDTO.Diagnosis) ? DBNull.Value
                    : mRecordDTO.Diagnosis
                };
                SqlParameter additionalNotesParam = new SqlParameter("@AdditionalNotes", SqlDbType.NVarChar)
                {
                    Value = string.IsNullOrEmpty(mRecordDTO.AdditionalNotes) ? DBNull.Value
                    : mRecordDTO.AdditionalNotes
                };

                int affectedRows = await SqlHelper.ExecuteWithOutputParameterAsync<int>("SP_UpdateMedicalRecord", medicalRecordIDParam, visitDescriptionParam, diagnosisParam, additionalNotesParam);
                return affectedRows > 0;
            }
            catch
            {
                throw;
            }
        }

        private static List<MedicalRecordInfoDTO> ConvertDataTableToMedicalRecordInfoDTOs(DataTable dataTable)
        {

            if (dataTable?.Rows == null || dataTable.Rows.Count == 0)
                return new List<MedicalRecordInfoDTO>();

            var records = new List<MedicalRecordInfoDTO>();

            var visitDescriptionOrdinal = dataTable.Columns["VisitDescription"].Ordinal;
            var diagnosisOrdinal = dataTable.Columns["Diagnosis"].Ordinal;
            var additionalNotesOrdinal = dataTable.Columns["AdditionalNotes"].Ordinal;

            foreach (DataRow row in dataTable.Rows)
            {
                records.Add(new MedicalRecordInfoDTO(
                     Convert.ToInt32(row["MedicalRecordID"]),
                     row["PatientName"]?.ToString() ?? string.Empty,
                     row["DoctorName"]?.ToString() ?? string.Empty,
                     row.Field<string>(visitDescriptionOrdinal) ?? string.Empty,
                     row.Field<string>(diagnosisOrdinal) ?? string.Empty,
                     row.Field<string>(additionalNotesOrdinal) ?? string.Empty,
                     Convert.ToDateTime(row["MedicalRecordDateTime"]))
                    );
            }

            return records;
        }

        
    }
}
