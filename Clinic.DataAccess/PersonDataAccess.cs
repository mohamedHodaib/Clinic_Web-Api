using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Clinic.DataAccess.DTOs;

namespace Clinic.DataAccess
{
    public class PersonDataAccess
    {
        public static async Task<List<PersonDTO>> GetAllPersons()
        {
            try
            {
                var dataTable = await SqlHelper.ExecuteDataTableAsync("SP_GetAllPersons");
                return ConvertDataTableToPersonDTOs(dataTable);
            }
            catch
            {
                throw;
            }
        }

        public static async Task<List<PersonDTO>> GetPersonsByGender(bool gender)
        {
            try
            {
                SqlParameter param = new SqlParameter("@Gender", SqlDbType.Bit) { Value = gender };
                var dataTable = await SqlHelper.ExecuteDataTableAsync("SP_GetPersonsByGender", param);
                return ConvertDataTableToPersonDTOs(dataTable);
            }
            catch
            {
                throw;
            }
        }

        public static async Task<List<PersonDTO>> GetPersonsByName(string name)
        {
            try
            {
                SqlParameter param = new SqlParameter("@Name", SqlDbType.NVarChar) { Value = name };
                var dataTable = await SqlHelper.ExecuteDataTableAsync("SP_GetPersonsByName", param);
                return ConvertDataTableToPersonDTOs(dataTable);
            }
            catch
            {
                throw;
            }
        }

        public static async Task<List<PersonDTO>> GetPersonsByAddress(string address)
        {
            try
            {
                SqlParameter param = new SqlParameter("@Address", SqlDbType.NVarChar) { Value = address };
                var dataTable = await SqlHelper.ExecuteDataTableAsync("SP_GetPersonsByAddress", param);
                return ConvertDataTableToPersonDTOs(dataTable);
            }
            catch
            {
                throw;
            }
        }

        public static async Task<List<PersonDTO>> GetPersonsByDateOfBirth(DateOnly dateOfBirth)
        {
            try
            {
                SqlParameter param = new SqlParameter("@DateOfBirth", SqlDbType.Date) { Value = dateOfBirth };
                var dataTable = await SqlHelper.ExecuteDataTableAsync("SP_GetPersonsByDateOfBirth", param);
                return ConvertDataTableToPersonDTOs(dataTable);
            }
            catch
            {
                throw;
            }
        }

        public static async Task<PersonDTO> GetPersonInfoById(int id)
        {
            try
            {
                SqlParameter idParam = new SqlParameter("@PersonID", SqlDbType.Int) { Value = id };
                PersonDTO personDTO = null;

                await SqlHelper.ExecuteReaderAsync("SP_GetPersonInfoById", reader =>
                {
                    if (reader.Read())
                    {
                        personDTO = new PersonDTO(
                             reader.GetInt32(reader.GetOrdinal("PersonID")),
                            reader.GetString(reader.GetOrdinal("Name")),
                            DateOnly.FromDateTime(reader["DateOfBirth"] == DBNull.Value
                            ? DateTime.MinValue : Convert.ToDateTime(reader["DateOfBirth"])),
                            reader.GetBoolean(reader.GetOrdinal("Gender")),
                            reader.GetString(reader.GetOrdinal("PhoneNumber")),
                            reader["Email"] == DBNull.Value
                            ? string.Empty : reader["Email"].ToString(),
                            reader["Address"] == DBNull.Value
                            ? string.Empty : reader["Address"].ToString()
                        );
                    }
                }, idParam);

                return personDTO;
            }
            catch
            {
                throw;
            }
        }

        public static async Task<PersonDTO> GetPersonInfoByPhone(string phoneNumber)
        {
            try
            {
                SqlParameter param = new SqlParameter("@PhoneNumber", SqlDbType.NVarChar) { Value = phoneNumber };
                PersonDTO personDTO = null;

                await SqlHelper.ExecuteReaderAsync("SP_GetPersonInfoByPhone", reader =>
                {
                    if (reader.Read())
                    {
                        personDTO = new PersonDTO(
                            reader.GetInt32(reader.GetOrdinal("PersonID")),
                            reader.GetString(reader.GetOrdinal("Name")),
                            DateOnly.FromDateTime(reader["DateOfBirth"] == DBNull.Value
                            ? DateTime.MinValue : Convert.ToDateTime(reader["DateOfBirth"])),
                            reader.GetBoolean(reader.GetOrdinal("Gender")),
                            reader.GetString(reader.GetOrdinal("PhoneNumber")),
                            reader["Email"] == DBNull.Value
                            ? string.Empty : reader["Email"].ToString(),
                            reader["Address"] == DBNull.Value
                            ? string.Empty : reader["Address"].ToString()
                        );
                    }
                }, param);

                return personDTO;
            }
            catch
            {
                throw;
            }
        }

        public static async Task<PersonDTO> GetPersonInfoByEmail(string email)
        {
            try
            {
                SqlParameter param = new SqlParameter("@Email", SqlDbType.NVarChar) { Value = email };
                PersonDTO personDTO = null;

                await SqlHelper.ExecuteReaderAsync("SP_GetPersonInfoByEmail", reader =>
                {
                    if (reader.Read())
                    {
                        personDTO = new PersonDTO(
                            reader.GetInt32(reader.GetOrdinal("PersonID")),
                            reader.GetString(reader.GetOrdinal("Name")),
                            DateOnly.FromDateTime(reader["DateOfBirth"] == DBNull.Value
                            ? DateTime.MinValue : Convert.ToDateTime(reader["DateOfBirth"])),
                            reader.GetBoolean(reader.GetOrdinal("Gender")),
                            reader.GetString(reader.GetOrdinal("PhoneNumber")),
                            reader["Email"] == DBNull.Value
                            ? string.Empty : reader["Email"].ToString(),
                            reader["Address"] == DBNull.Value
                            ? string.Empty : reader["Address"].ToString()
                        )
                        {

                        };
                    }
                }, param);

                return personDTO;
            }
            catch
            {
                throw;
            }
        }

        public static async Task<bool> IsEmailUsed(string email)
        {
            try
            {
                var param = new SqlParameter("@Email", SqlDbType.NVarChar) { Value = email };
                var personID = await SqlHelper.ExecuteScalarAsync<int>("SP_IsEmailUsed", param);
                return personID != 0;
            }
            catch
            {
                throw;
            }
        }

        public static async Task<bool> IsPhoneNumberUsed(string phoneNumber)
        {
            try
            {
                var param = new SqlParameter("@PhoneNumber", SqlDbType.NVarChar) { Value = phoneNumber };
                var personID = await SqlHelper.ExecuteScalarAsync<int>("SP_IsPhoneNumberUsed", param);
                return personID != 0;
            }
            catch
            {
                throw;
            }
        }

        public static async Task<bool> IsEmailUsed(int personID, string email)
        {
            try
            {
                var idParam = new SqlParameter("@PersonID", SqlDbType.NVarChar) { Value = personID };
                var emailParam = new SqlParameter("@Email", SqlDbType.NVarChar) { Value = email };

                var currentPersonID = await SqlHelper.ExecuteScalarAsync<int>("SP_IsEmailUsedExceptPerson", idParam,emailParam);
                return currentPersonID != 0;
            }
            catch
            {
                throw;
            }
        }

        public static async Task<bool> IsPhoneNumberUsed(int personID, string phoneNumber)
        {
            try
            {
                var idParam = new SqlParameter("@PersonID", SqlDbType.NVarChar) { Value = personID };
                var phoneParam = new SqlParameter("@PhoneNumber", SqlDbType.NVarChar) { Value = phoneNumber };
                var currentPersonID = await SqlHelper.ExecuteScalarAsync<int>("SP_IsPhoneNumberUsedExceptPerson", phoneParam,idParam);
                return currentPersonID != 0;
            }
            catch
            {
                throw;
            }
        }
        public static async Task<int> AddNewPerson(PersonDTO personDTO)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@Name", SqlDbType.NVarChar) { Value = personDTO.Name },
                    new SqlParameter("@DateOfBirth", SqlDbType.Date) { Value = personDTO.DateOfBirth == null ? DBNull.Value
                    : personDTO.DateOfBirth },
                    new SqlParameter("@Gender", SqlDbType.Bit) { Value = personDTO.Gender },
                    new SqlParameter("@PhoneNumber", SqlDbType.NVarChar) { Value = personDTO.PhoneNumber },
                    new SqlParameter("@PersonID", SqlDbType.Int) { Direction = ParameterDirection.Output },
                    new SqlParameter("@Email", SqlDbType.NVarChar) { Value = string.IsNullOrEmpty( personDTO.Email) ? DBNull.Value
                    : personDTO.Email},
                    new SqlParameter("@Address", SqlDbType.NVarChar) { Value = string.IsNullOrEmpty( personDTO.Address) ? DBNull.Value
                    : personDTO.Address}
                };

                return await SqlHelper.ExecuteWithOutputParameterAsync<int>("SP_AddNewPerson", parameters);
            }
            catch
            {
                throw;
            }
        }

        public static async Task<bool> UpdatePerson(PersonDTO personDTO)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@PersonID", SqlDbType.Int) { Value = personDTO.PersonID },
                    new SqlParameter("@Name", SqlDbType.NVarChar) { Value = personDTO.Name },
                    new SqlParameter("@DateOfBirth", SqlDbType.Date) { Value = personDTO.DateOfBirth == null ? DBNull.Value 
                    : personDTO.DateOfBirth },
                    new SqlParameter("@Gender", SqlDbType.Bit) { Value = personDTO.Gender },
                    new SqlParameter("@PhoneNumber", SqlDbType.NVarChar) { Value = personDTO.PhoneNumber },
                    new SqlParameter("@Email", SqlDbType.NVarChar) { Value = string.IsNullOrEmpty( personDTO.Email) ? DBNull.Value
                    : personDTO.Email},
                    new SqlParameter("@Address", SqlDbType.NVarChar) { Value = string.IsNullOrEmpty( personDTO.Address) ? DBNull.Value
                    : personDTO.Address}
                };

                int affectedRows = await SqlHelper.ExecuteNonQueryAsync("SP_UpdatePerson", parameters);

                return affectedRows > 0;
            }
            catch
            {
                throw;
            }
        }

        public static async Task<bool> DeletePerson(int personID)
        {
            try
            {
                SqlParameter param = new SqlParameter("@PersonID", SqlDbType.Int) { Value = personID };
                int affectedRows = await SqlHelper.ExecuteNonQueryAsync("SP_DeletePerson", param);

                return affectedRows > 0;
            }
            catch
            {
                throw;
            }
        }

        private static List<PersonDTO> ConvertDataTableToPersonDTOs(DataTable dataTable)
        {
            var persons = new List<PersonDTO>();
            if (dataTable?.Rows == null || dataTable.Rows.Count == 0) return persons;

            var dobOrdinal = dataTable.Columns["DateOfBirth"].Ordinal;
            var emailOrdinal = dataTable.Columns["Email"].Ordinal;
            var addressOrdinal = dataTable.Columns["Address"].Ordinal;

            foreach (DataRow row in dataTable.Rows)
            {
                persons.Add(new PersonDTO(
                    Convert.ToInt32(row["PersonID"]),
                    row["Name"]?.ToString() ?? string.Empty,
                    DateOnly.FromDateTime(row.Field<DateTime?>(dobOrdinal) ?? DateTime.MinValue),
                    Convert.ToBoolean(row["Gender"]),
                    row["PhoneNumber"]?.ToString() ?? string.Empty,
                    row.Field<string>(emailOrdinal) ?? string.Empty,
                    row.Field<string>(addressOrdinal) ?? string.Empty
                ));
            }

            return persons;
        }
    }
}
