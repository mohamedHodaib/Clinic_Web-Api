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
    public class Doctor: Person
    {
        public enum enMode { AddNew, Update };
        public enMode Mode = enMode.AddNew;

        private static string _ExceptionMessage = string.Empty;

        public DoctorInfoDTO doctorInfoDTO => new DoctorInfoDTO(DoctorID, PersonID,Name
            ,DateOfBirth,Gender,PhoneNumber,Email,Address,Specialization);

        public DoctorDTO doctorDTO => new DoctorDTO(DoctorID, PersonID,Specialization);
        public int DoctorID { get; set; }
        public int PersonID { get; set; }

        public string Specialization { get; set; }


        public Doctor(DoctorInfoDTO doctorInfoDTO, enMode mode = enMode.AddNew)
            : base(doctorInfoDTO, (Person.enMode)mode)
        {
            DoctorID = doctorInfoDTO.DoctorID;
            PersonID = doctorInfoDTO.PersonID;
            Specialization = doctorInfoDTO.Specialization;

            Mode = mode;
        }

        public static async Task<Doctor> GetDoctorByID(int id)
        {
            try
            {
                DoctorDTO doctorDTO = await DoctorDataAccess.GetDoctorInfoById(id);

                if (doctorDTO != null)
                {
                    PersonDTO personDTO = (await GetPersonByID(doctorDTO.PersonID)).personDTO;
                    if (personDTO != null)
                    {
                        DoctorInfoDTO doctorInfoDTO = new DoctorInfoDTO(doctorDTO.DoctorID, doctorDTO.PersonID
                            , personDTO.Name, personDTO.DateOfBirth, personDTO.Gender, personDTO.PhoneNumber
                            , personDTO.Email, personDTO.Address,doctorDTO.Specialization);

                        return new Doctor(doctorInfoDTO, enMode.Update);
                    }

                    else
                        return null;
                }
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
        public static async Task<Doctor> GetDoctorInfoByPhone(string phone)
        {
            try
            {
                PersonDTO personDTO = await PersonDataAccess.GetPersonInfoByPhone(phone);

                if (personDTO != null)
                {
                    DoctorDTO doctorDTO = await DoctorDataAccess.GetDoctorInfoByPersonId(personDTO.PersonID);
                    if (personDTO != null)
                    {
                        DoctorInfoDTO doctorInfoDTO = new DoctorInfoDTO(doctorDTO.DoctorID, doctorDTO.PersonID
                            , personDTO.Name, personDTO.DateOfBirth, personDTO.Gender, personDTO.PhoneNumber
                            , personDTO.Email, personDTO.Address,doctorDTO.Specialization);

                        return new Doctor(doctorInfoDTO, enMode.Update);
                    }

                    else
                        return null;
                }
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


        public static async Task<Doctor> GetDoctorInfoByEmail(string email)
        {
            try
            {
                PersonDTO personDTO = await PersonDataAccess.GetPersonInfoByEmail(email);

                if (personDTO != null)
                {
                    DoctorDTO doctorDTO = await DoctorDataAccess.GetDoctorInfoByPersonId(personDTO.PersonID);
                    if (personDTO != null)
                    {
                        DoctorInfoDTO doctorInfoDTO = new DoctorInfoDTO(doctorDTO.DoctorID, doctorDTO.PersonID
                            , personDTO.Name, personDTO.DateOfBirth, personDTO.Gender, personDTO.PhoneNumber
                            , personDTO.Email, personDTO.Address,doctorDTO.Specialization);

                        return new Doctor(doctorInfoDTO, enMode.Update);
                    }

                    else
                        return null;
                }
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


        public static async Task<List<DoctorInfoDTO>> GetAllDoctors()
        {
            try
            {
                return await DoctorDataAccess.GetAllDoctors();
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

        public static async Task<List<DoctorInfoDTO>> GetDoctorsByGender(bool gender)
        {
            try
            {
                return await DoctorDataAccess.GetDoctorsByGender(gender);
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

        public static async Task<List<DoctorInfoDTO>> GetDoctorsByAddress(string address)
        {
            try
            {
                return await DoctorDataAccess.GetDoctorsByAddress(address);
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

        public static async Task<List<DoctorInfoDTO>> GetDoctorsByName(string name)
        {
            try
            {
                return await DoctorDataAccess.GetDoctorsByName(name);
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

        public static async Task<List<DoctorInfoDTO>> GetDoctorsByDateOfBirth(DateOnly dateOfBirth)
        {
            try
            {
                return await DoctorDataAccess.GetDoctorsByDateOfBirth(dateOfBirth);
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

        public static async Task<List<DoctorInfoDTO>> GetDoctorsBySpecialization(string specialization)
        {
            try
            {
                return await DoctorDataAccess.GetDoctorsBySpecialization(specialization);
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
                //delete the Doctor 
                if (await DoctorDataAccess.DeleteDoctor(DoctorID))
                    return false;

                //delete the person that Is connected to Doctor
                return await base.Delete();
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

        private async Task<bool> _AddNewDoctor()
        {
            try
            {
                DoctorID = await DoctorDataAccess.AddNewDoctor(doctorDTO);
                return DoctorID != 0;
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
                base.Mode = (Person.enMode)Mode;

                bool isSuccess = base.Save().Result;


                if (!isSuccess)
                    return false;

                if (Mode == enMode.AddNew)
                    PersonID = base.PersonID;

                switch (Mode)
                {
                    case enMode.AddNew:
                        if (await _AddNewDoctor())
                        {

                            Mode = enMode.Update;
                            return true;
                        }
                        else
                        {
                            return false;
                        }

                    case enMode.Update:

                        return isSuccess;

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
