using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Clinic.DataAccess.DTOs;
using Clinic.DataAccess.DTOs.Clinic.DataAccess.DTOs;

namespace Clinic.DataAccess
{
    public class PrescriptionDataAccess
    {
        public static async Task<List<PrescriptionInfoDTO>> GetAllPrescriptions()
        {
            try
            {
                var dataTable = await SqlHelper.ExecuteDataTableAsync("SP_GetAllPrescriptions");
                return ConvertDataTableToPrescriptionInfoDTOs(dataTable);
            }
            catch
            {
                throw;
            }
        }

        public static async Task<PrescriptionDTO> GetPrescriptionByID(int id)
        {
            try
            {
                SqlParameter param = new SqlParameter("@PrescriptionID", SqlDbType.Int) { Value = id };

                PrescriptionDTO prescriptionDTO = null;
                 await SqlHelper.ExecuteReaderAsync("SP_GetPrescriptionByID", reader =>
                {
                    if (reader.Read())
                    {
                        prescriptionDTO =  new PrescriptionDTO
                        (
                            reader.GetInt32(reader.GetOrdinal("PrescriptionID")),
                            reader.GetInt32(reader.GetOrdinal("MedicalRecordID")),
                            reader.GetString(reader.GetOrdinal("MedicationName")),
                            reader.GetString(reader.GetOrdinal("Dosage")),
                            reader.GetString(reader.GetOrdinal("Frequency")),
                            DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("StartDate"))),
                            DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("EndDate"))),
                            reader["SpecialInstructions"] == DBNull.Value
                            ? string.Empty : reader["SpecialInstructions"].ToString()
                        );
                    }
                }, param);

                return prescriptionDTO;
            }
            catch
            {
                throw;
            }
        }

        public static async Task<List<PrescriptionInfoDTO>> GetPrescriptionsByPatientID(int patientID)
        {
            try
            {
                SqlParameter param = new SqlParameter("@PatientID", SqlDbType.Int) { Value = patientID };
                var dataTable = await SqlHelper.ExecuteDataTableAsync("SP_GetPrescriptionsByPatientID", param);
                return  ConvertDataTableToPrescriptionInfoDTOs(dataTable);
            }
            catch
            {
                throw;
            }
        }



        public static async Task<int> AddNewPrescription(PrescriptionDTO prescription)
        {
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@PrescriptionID", SqlDbType.Int) { Direction = ParameterDirection.Output},
                    new SqlParameter("@MedicalRecordID", SqlDbType.Int) { Value = prescription.MedicalRecordID },
                    new SqlParameter("@MedicationName", SqlDbType.NVarChar) { Value = prescription.MedicationName },
                    new SqlParameter("@Dosage", SqlDbType.NVarChar) { Value = prescription.Dosage },
                    new SqlParameter("@Frequency", SqlDbType.NVarChar) { Value = prescription.Frequency },
                    new SqlParameter("@StartDate", SqlDbType.Date) { Value = prescription.StartDate },
                    new SqlParameter("@EndDate", SqlDbType.Date) { Value = prescription.EndDate },
                    new SqlParameter("@SpecialInstructions", SqlDbType.NVarChar) 
                    { Value = string.IsNullOrEmpty(prescription.SpecialInstructions) ? DBNull.Value : prescription.SpecialInstructions}
                };

                return await SqlHelper.ExecuteWithOutputParameterAsync<int>("SP_AddNewPrescription",parameters);
            }
            catch
            {
                throw;
            }
        }

        public static async Task<bool> UpdatePrescription(PrescriptionDTO prescription)
        {
            try
            {
                SqlParameter[] parameters =
                {
                    new SqlParameter("@PrescriptionID", SqlDbType.Int) { Value = prescription.PrescriptionID },
                    new SqlParameter("@MedicalRecordID", SqlDbType.Int) { Value = prescription.MedicalRecordID },
                    new SqlParameter("@MedicationName", SqlDbType.NVarChar) { Value = prescription.MedicationName },
                    new SqlParameter("@Dosage", SqlDbType.NVarChar) { Value = prescription.Dosage },
                    new SqlParameter("@Frequency", SqlDbType.NVarChar) { Value = prescription.Frequency },
                    new SqlParameter("@StartDate", SqlDbType.Date) { Value = prescription.StartDate },
                    new SqlParameter("@EndDate", SqlDbType.Date) { Value = prescription.EndDate },
                    new SqlParameter("@SpecialInstructions", SqlDbType.NVarChar)
                    { Value = string.IsNullOrEmpty(prescription.SpecialInstructions) ? DBNull.Value : prescription.SpecialInstructions}
                };

                int affectedRows = await SqlHelper.ExecuteWithOutputParameterAsync<int>("SP_UpdatePrescription", parameters);
                return affectedRows > 0;
            }
            catch
            {
                throw;
            }
        }

        private static List<PrescriptionInfoDTO> ConvertDataTableToPrescriptionInfoDTOs(DataTable table)
        {
            var list = new List<PrescriptionInfoDTO>();

            if (table?.Rows == null) return list;

            var specialInOrdinal = table.Columns["SpecialInstructions"].Ordinal;
            
            foreach (DataRow row in table.Rows)
            {
                list.Add(new PrescriptionInfoDTO(
                    Convert.ToInt32(row["PrescriptionID"]),
                    row["Diagnosis"]?.ToString() ?? string.Empty,
                    row["MedicationName"]?.ToString() ?? string.Empty,
                    row["Dosage"]?.ToString() ?? string.Empty,
                    row["Frequency"]?.ToString() ?? string.Empty,
                    (DateOnly)row["StartDate"],
                    (DateOnly)row["EndDate"],
                    row.Field<string>(specialInOrdinal) ?? string.Empty,
                    row["PatientName"]?.ToString() ?? string.Empty,
                    row["DoctorName"]?.ToString() ?? string.Empty
                ));
            }

            return list;
        }
    }
}
