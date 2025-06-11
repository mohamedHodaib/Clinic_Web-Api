using Clinic.DataAccess.DTOs;
using Clinic.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Diagnostics;
using System.Numerics;

namespace Clinic.Business
{
    public class Person
    {
        public enum enMode { AddNew, Update };
        public enMode Mode = enMode.AddNew;

        private static string _ExceptionMessage = string.Empty;

        public PersonDTO personDTO => new PersonDTO(PersonID, Name, DateOfBirth
            , Gender, PhoneNumber, Email, Address);

        public int PersonID { get; set; }

        public string Name { get; set; }

        public DateOnly DateOfBirth { get; set; }

        public bool Gender { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public Person(PersonDTO personDTO, enMode mode = enMode.AddNew)
        {
            PersonID = personDTO.PersonID;
            Name = personDTO.Name;
            DateOfBirth = personDTO.DateOfBirth;
            Gender = personDTO.Gender;
            PhoneNumber = personDTO.PhoneNumber;
            Email = personDTO.Email;
            Address = personDTO.Address;

            Mode = mode;
        }

 
        public static async Task<Person> GetPersonByID(int id)
        {
            try
            {
                PersonDTO personDTO =  await PersonDataAccess.GetPersonInfoById(id);

                if (personDTO != null)
                    return new Person(personDTO, enMode.Update);

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
        public static async Task<Person> GetPersonInfoByPhone(string phone)
        {
            try
            {
                PersonDTO personDTO =  await PersonDataAccess.GetPersonInfoByPhone(phone);

                if (personDTO != null)
                    return new Person(personDTO, enMode.Update);

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
        public static async Task<Person> GetPersonInfoByEmail(string email)
        {
            try
            {
                PersonDTO personDTO = await PersonDataAccess.GetPersonInfoByEmail(email);

                if (personDTO != null)
                    return new Person(personDTO, enMode.Update);
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


        public static async Task<List<PersonDTO>> GetAllPersons()
        {
            try
            {
                return await PersonDataAccess.GetAllPersons();
            }
            catch (SqlException ex)
            {
                _ExceptionMessage = ex.Message;
            }
            catch (Exception ex)
            {
                _ExceptionMessage = ex.Message;
            }

            Global.Log(_ExceptionMessage, EventLogEntryType.Error);

            return null;
        }

        public static async Task<List<PersonDTO>> GetPersonsByGender(bool gender)
        {
            try
            {
                return await PersonDataAccess.GetPersonsByGender(gender);
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

        public static async Task<List<PersonDTO>> GetPersonsByAddress(string address)
        {
            try
            {
                return await PersonDataAccess.GetPersonsByAddress(address);
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

        public static async Task<List<PersonDTO>> GetPersonsByName(string name)
        {
            try
            {
                return await PersonDataAccess.GetPersonsByName(name);
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

        public static async Task<List<PersonDTO>> GetPersonsByDateOfBirth(DateOnly dateOfBirth)
        {
            try
            {
                return await PersonDataAccess.GetPersonsByDateOfBirth(dateOfBirth);
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



        public async Task<bool> Delete()
        {
            try
            {
                return await PersonDataAccess.DeletePerson(PersonID);
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

        public static async Task<bool> IsEmailUsed(string Email)
        {
            try
            {
                return await PersonDataAccess.IsEmailUsed(Email);
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


        public static async Task<bool> IsPhoneNumberUsed(string phoneNumber)
        {
            try
            {
                return await PersonDataAccess.IsPhoneNumberUsed(phoneNumber);
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

        public static async Task<bool> IsEmailUsed(int personID,string Email)
        {
            try
            {
                return await PersonDataAccess.IsEmailUsed(personID,Email);
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


        public static async Task<bool> IsPhoneNumberUsed(int personID, string phoneNumber)
        {
            try
            {
                return await PersonDataAccess.IsPhoneNumberUsed( personID, phoneNumber);
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

        private async Task<bool> _AddNewPerson()
        {
            try
            {
                PersonID = await PersonDataAccess.AddNewPerson(personDTO);
                return PersonID != 0;
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

        private async Task<bool> _UpdatePerson()
        {
            try
            {
                return await PersonDataAccess.UpdatePerson(personDTO);
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
                        if (await _AddNewPerson())
                        {

                            Mode = enMode.Update;
                            return true;
                        }
                        else
                        {
                            return false;
                        }

                    case enMode.Update:

                        return await _UpdatePerson();

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
