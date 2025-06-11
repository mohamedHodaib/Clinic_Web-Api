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
    public class MedicalRecords
    {
        public enum enMode { AddNew, Update };
        public enMode Mode = enMode.AddNew;

        private static string _ExceptionMessage = string.Empty;
       
        public MedicalRecordDTO medicalRecordDTO => new MedicalRecordDTO(MedicalRecordID,VisitDescription,
            Diagnosis,AdditionalNotes,AppointmentID);

        public int MedicalRecordID { get; set; }
        public string VisitDescription { get; set; }
        public string Diagnosis { get; set; }
        public string AdditionalNotes { get; set; }
        public int AppointmentID { get; set; }


        public MedicalRecords(MedicalRecordDTO medicalRecordDTO, enMode mode = enMode.AddNew)
        {
            MedicalRecordID = medicalRecordDTO.MedicalRecordID;
            VisitDescription = medicalRecordDTO.VisitDescription;
            Diagnosis = medicalRecordDTO.Diagnosis;
            AdditionalNotes = medicalRecordDTO.AdditionalNotes;
            AppointmentID = medicalRecordDTO.AppointmentID;

            Mode = mode;
        }


        public static async Task<List<MedicalRecordInfoDTO>> GetMedicalRecordsByPatientID(int patientID)
        {
            try
            {
               return await MedicalRecordDataAccess.GetMedicalRecordsByPatientID(patientID);
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

        public static async Task<List<MedicalRecordInfoDTO>> GetMedicalRecordsByDoctorID(int doctorID)
        {
            try
            {
              return  await MedicalRecordDataAccess.GetMedicalRecordsByDoctorID(doctorID);
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

        public static async Task<MedicalRecords> GetMedicalRecordByID(int medicalRecordID)
        {
            try
            {
               MedicalRecordDTO medicalRecordDTO = 
                    await MedicalRecordDataAccess.GetMedicalRecordByID(medicalRecordID);

                if (medicalRecordDTO != null)
                    return new MedicalRecords(medicalRecordDTO, enMode.Update);

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


        public static async Task<MedicalRecordInfoDTO> GetMedicalRecordByAppointmentID(int appointmentID)
        {
            try
            {
                return await MedicalRecordDataAccess.GetMedicalRecordByAppointmentID(appointmentID);
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



        private async Task<bool> _AddNewMedicalRecord()
        {
            try
            {
                MedicalRecordID = await MedicalRecordDataAccess.AddNewMedicalRecord(medicalRecordDTO);
                return MedicalRecordID != 0;
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


        public static async Task<bool> IsAppointmentHaveMedicalRecord(int appointmentID)
        {
            try
            {
                return await MedicalRecordDataAccess.IsAppointmentHaveMedicalRecord(appointmentID);
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

        private async Task<bool> _UpdateMedicalRecord()
        {
            try
            {
                return await MedicalRecordDataAccess.UpdateMedicalRecord(medicalRecordDTO);
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
                        if (await _AddNewMedicalRecord())
                        {

                            Mode = enMode.Update;
                            return true;
                        }
                        else
                        {
                            return false;
                        }

                    case enMode.Update:

                        return await _UpdateMedicalRecord();

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
