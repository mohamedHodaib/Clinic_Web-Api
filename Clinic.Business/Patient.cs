using Clinic.DataAccess.DTOs;
using Clinic.DataAccess;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net;
using System.Reflection;
using System.Numerics;

namespace Clinic.Business
{
    public class Patient : Person
    {
        public enum enMode { AddNew, Update };
        public enMode Mode = enMode.AddNew;

        private static string _ExceptionMessage = string.Empty;

        public PatientInfoDTO patientInfoDTO => new PatientInfoDTO(PatientID, PersonID, Name,
            DateOfBirth, Gender, PhoneNumber, Email, Address);

        public PatientDTO patientDTO => new PatientDTO(PatientID, PersonID);

        public int PatientID { get; set; }

        public int PersonID { get; set; }
        

        public Patient(PatientInfoDTO patientInfoDTO, enMode mode = enMode.AddNew)
            :base(patientInfoDTO,(Person.enMode)mode)
        {
            PatientID = patientInfoDTO.PatientID;
            PersonID = patientInfoDTO.PersonID;

            Mode = mode;
        }

      
        public static async Task<Patient> GetPatientByID(int id)
        {
            try
            {
                PatientDTO patientDTO =  await PatientDataAccess.GetPatientInfoById(id);

                if (patientDTO != null)
                {
                    PersonDTO personDTO = (await GetPersonByID(patientDTO.PersonID)).personDTO;
                    if (personDTO != null)
                    {
                        PatientInfoDTO patientInfoDTO = new PatientInfoDTO(patientDTO.PatientID, patientDTO.PersonID
                            ,personDTO.Name,personDTO.DateOfBirth,personDTO.Gender,personDTO.PhoneNumber
                            ,personDTO.Email,personDTO.Address);

                        return new Patient(patientInfoDTO, enMode.Update);
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
        public static async Task<Patient> GetPatientInfoByPhone(string phone)
        {
            try
            {
                PersonDTO personDTO = await PersonDataAccess.GetPersonInfoByPhone(phone);

                if (personDTO != null)
                {
                    PatientDTO patientDTO = await PatientDataAccess.GetPatientInfoByPersonID(personDTO.PersonID);
                    if (personDTO != null)
                    {
                        PatientInfoDTO patientInfoDTO = new PatientInfoDTO(patientDTO.PatientID, patientDTO.PersonID
                            , personDTO.Name, personDTO.DateOfBirth, personDTO.Gender, personDTO.PhoneNumber
                            , personDTO.Email, personDTO.Address);

                        return new Patient(patientInfoDTO, enMode.Update);
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


        public static async Task<Patient> GetPatientInfoByEmail(string email)
        {
            try
            {
                PersonDTO personDTO = await PersonDataAccess.GetPersonInfoByEmail(email);

                if (personDTO != null)
                {
                    PatientDTO patientDTO = await PatientDataAccess.GetPatientInfoByPersonID(personDTO.PersonID);
                    if (personDTO != null)
                    {
                        PatientInfoDTO patientInfoDTO = new PatientInfoDTO(patientDTO.PatientID, patientDTO.PersonID
                            , personDTO.Name, personDTO.DateOfBirth, personDTO.Gender, personDTO.PhoneNumber
                            , personDTO.Email, personDTO.Address);

                        return new Patient(patientInfoDTO, enMode.Update);
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

        public async Task<PatientHistoryDTO> GetPatientHistory()
        {
            try
            {
                return await PatientDataAccess.GetPatientHistorySummaryById(PatientID);
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


        public static async Task<List<PatientInfoDTO>> GetAllPatients()
        {
            try
            {
                return await PatientDataAccess.GetAllPatients();
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



        public static async Task<List<PatientInfoDTO>> GetPatientsByGender(bool gender)
        {
            try
            {
                return await PatientDataAccess.GetPatientsByGender(gender);
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

        public static async Task<List<PatientInfoDTO>> GetPatientsByAddress(string address)
        {
            try
            {
                return await PatientDataAccess.GetPatientsByAddress(address);
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

        public static async Task<List<PatientInfoDTO>> GetPatientsByName(string name)
        {
            try
            {
                return await PatientDataAccess.GetPatientsByName(name);
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

        public static async Task<List<PatientInfoDTO>> GetPatientsByDateOfBirth(DateOnly dateOfBirth)
        {
            try
            {
                return await PatientDataAccess.GetPatientsByDateOfBirth(dateOfBirth);
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

        private async Task<bool> _AddNewPatient()
        {
            try
            {
                PatientID = await PatientDataAccess.AddNewPatient(patientDTO);
                return PatientID != 0;
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
                //delete the Patient 
                if (await PatientDataAccess.DeletePatient(PatientID))
                    return false;

                //delete the person that Is connected to Patient
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
                        if (await _AddNewPatient())
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
