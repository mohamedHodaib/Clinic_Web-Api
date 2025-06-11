using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clinic.DataAccess.DTOs;
using Microsoft.VisualBasic;


namespace Clinic.DataAccess
{
    
   
    public class DoctorDataAccess
    {

        
        public static async  Task<List<DoctorInfoDTO>>  GetAllDoctors()
        {
            try
            {
                var dataTable  = await SqlHelper.ExecuteDataTableAsync("SP_GetAllDoctors");

                return ConvertDataTableToDoctorInfoDTOs(dataTable);
            }
            catch
            {
                throw;
            }
        }


        public static async Task<List<DoctorInfoDTO>> GetDoctorsByGender(bool gender)
        {
            try
            {
                SqlParameter param = new SqlParameter("@Gender", SqlDbType.Bit)
                {
                    Value = gender
                };

                var dataTable = await SqlHelper.ExecuteDataTableAsync("SP_GetDoctorsByGender", param);

                return ConvertDataTableToDoctorInfoDTOs(dataTable);
            }
            catch
            {
                throw;
            }
        }


        public static async Task<List<DoctorInfoDTO>> GetDoctorsByName(string name)
        {
            try
            {
                SqlParameter param = new SqlParameter("@Name", SqlDbType.NVarChar)
                {
                    Value = name
                };

                var dataTable = await SqlHelper.ExecuteDataTableAsync("SP_GetDoctorsByName", param);

                return ConvertDataTableToDoctorInfoDTOs(dataTable);
            }
            catch
            {
                throw;
            }
        }

        public static async Task<List<DoctorInfoDTO>> GetDoctorsByAddress(string Address)
        {
            try
            {
                SqlParameter param = new SqlParameter("@Address", SqlDbType.NVarChar)
                {
                    Value = Address
                };

                var dataTable = await SqlHelper.ExecuteDataTableAsync("SP_GetDoctorsByAddress", param);

                return ConvertDataTableToDoctorInfoDTOs(dataTable);
            }
            catch
            {
                throw;
            }
        }


        public static async Task<List<DoctorInfoDTO>> GetDoctorsByDateOfBirth(DateOnly dateOfBirth)
        {
            try
            {
                SqlParameter param = new SqlParameter("@DateOfBirth", SqlDbType.Date)
                {
                    Value = dateOfBirth
                };

                var dataTable = await SqlHelper.ExecuteDataTableAsync("SP_GetDoctorsByDateOfBirth", param);

                 return ConvertDataTableToDoctorInfoDTOs(dataTable);
            }
            catch
            {
                throw;
            }
        }


        public static async Task<List<DoctorInfoDTO>> GetDoctorsBySpecialization(string specialization)
        {
            try
            {
                SqlParameter param = new SqlParameter("@Specialization", SqlDbType.NVarChar)
                {
                    Value = specialization
                };

                var dataTable = await SqlHelper.ExecuteDataTableAsync("SP_GetDoctorsBySpecialization", param);

                return ConvertDataTableToDoctorInfoDTOs(dataTable);
            }
            catch
            {
                throw;
            }
        }
        public static async Task<DoctorDTO> GetDoctorInfoById(int id)
        {
            try
            {

                SqlParameter idParam = new SqlParameter("@DoctorID", SqlDbType.Int)
                {
                    Value = id
                };
                DoctorDTO doctorDTO = null;
                await SqlHelper.ExecuteReaderAsync("SP_GetDoctorInfoById", reader =>
                {
                    if (reader.Read())
                    {
                        doctorDTO = new DoctorDTO
                       (
                           id,
                           reader.GetInt32(reader.GetOrdinal("PersonID")),
                           reader.GetString(reader["Specialization"] == DBNull.Value
                           ? string.Empty
                           : reader["Specialization"].ToString())
                       );
                    }

                },
                    idParam
                );

                return doctorDTO;
            }
            catch
            {
                throw;
            }

        }


        public static async Task<DoctorDTO> GetDoctorInfoByPersonId(int personID)
        {
            try
            {

                SqlParameter idParam = new SqlParameter("@PersonID", SqlDbType.Int)
                {
                    Value = personID
                };
                DoctorDTO doctorDTO = null;
                await SqlHelper.ExecuteReaderAsync("SP_GetDoctorInfoByPersonId", reader =>
                {
                    if (reader.Read())
                    {
                        doctorDTO = new DoctorDTO
                       (
                           reader.GetInt32(reader.GetOrdinal("DoctorID")),
                           personID,
                           reader.GetString(reader.GetString(reader["Specialization"] == DBNull.Value
                           ? string.Empty
                           : reader["Specialization"].ToString()))
                       );
                    }

                },
                    idParam
                );

                return doctorDTO;
            }
            catch
            {
                throw;
            }

        }


        public static async Task<int> AddNewDoctor(DoctorDTO doctorDTO)
        {

            try
            {

                SqlParameter personIDParam = new SqlParameter("@PersonID", SqlDbType.Int) { Value = doctorDTO.PersonID };
                SqlParameter specializationParam = new SqlParameter("@Specialization", SqlDbType.NVarChar) { Value = string.IsNullOrEmpty(doctorDTO.Specialization) ? DBNull.Value 
                    : doctorDTO.Specialization };
                SqlParameter doctorIDParam = new SqlParameter("@DoctorID", SqlDbType.Int) { Direction = ParameterDirection.Output };

                return await SqlHelper.ExecuteWithOutputParameterAsync<int>("SP_AddNewDoctor", personIDParam,specializationParam, doctorIDParam);

            }
            catch
            {
                throw;
            }

        }


        public static async Task<int> UpdateDoctor(DoctorDTO doctorDTO)
        {

            try
            {
                SqlParameter doctorIDParam = new SqlParameter("@DoctorID", SqlDbType.Int) { Value = doctorDTO.DoctorID };
                SqlParameter personIDParam = new SqlParameter("@PersonID", SqlDbType.Int) { Value = doctorDTO.PersonID };
                SqlParameter specializationParam = new SqlParameter("@Specialization", SqlDbType.NVarChar) { Value = string.IsNullOrEmpty(doctorDTO.Specialization) ? DBNull.Value
                    : doctorDTO.Specialization
                };

                return await SqlHelper.ExecuteNonQueryAsync("SP_UpdateDoctor", personIDParam, specializationParam, specializationParam);

            }
            catch
            {
                throw;
            }

        }



        public static async Task<bool> DeleteDoctor(int personID)
        {

            try
            {
                SqlParameter idParam = new SqlParameter("@DoctorID", SqlDbType.Int)
                {
                    Value = personID
                };

                int affectedRows =  await SqlHelper.ExecuteNonQueryAsync("SP_DeleteDoctor", idParam);
                return affectedRows > 0;
            }
            catch
            {
                throw;
            }

        }

       private static List<DoctorInfoDTO> ConvertDataTableToDoctorInfoDTOs(DataTable dataTable)
{
    if (dataTable?.Rows == null || dataTable.Rows.Count == 0) 
        return new List<DoctorInfoDTO>();

    // Pre-allocate list with known capacity
    var doctors = new List<DoctorInfoDTO>(dataTable.Rows.Count);
    
    // Cache column ordinals for better performance
    var doctorIdOrdinal = dataTable.Columns["DoctorID"].Ordinal;
    var personIdOrdinal = dataTable.Columns["PersonID"].Ordinal;
    var nameOrdinal = dataTable.Columns["Name"].Ordinal;
    var dobOrdinal = dataTable.Columns["DateOfBirth"].Ordinal;
    var genderOrdinal = dataTable.Columns["Gender"].Ordinal;
    var phoneOrdinal = dataTable.Columns["PhoneNumber"].Ordinal;
    var emailOrdinal = dataTable.Columns["Email"].Ordinal;
    var addressOrdinal = dataTable.Columns["Address"].Ordinal;
    var specializationOrdinal = dataTable.Columns["Specialization"].Ordinal;

    foreach (DataRow row in dataTable.Rows)
    {
        doctors.Add(new DoctorInfoDTO(
            row.Field<int>(doctorIdOrdinal), // Using Field<T> is safer and more efficient
            row.Field<int>(personIdOrdinal),
            row.Field<string>(nameOrdinal) ?? string.Empty,
            row.IsNull(dobOrdinal) ? DateOnly.MinValue 
                : DateOnly.FromDateTime(row.Field<DateTime>(dobOrdinal)),
            row.Field<bool>(genderOrdinal),
            row.Field<string>(phoneOrdinal) ?? string.Empty,
            row.Field<string>(emailOrdinal) ?? string.Empty,
            row.Field<string>(addressOrdinal) ?? string.Empty,
            row.Field<string>(specializationOrdinal) ?? string.Empty
        ));
    }
    
    return doctors;
}


    }
}
