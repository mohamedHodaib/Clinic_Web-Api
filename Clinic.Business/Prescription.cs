using Clinic.DataAccess.DTOs;
using Clinic.DataAccess;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clinic.DataAccess.DTOs.Clinic.DataAccess.DTOs;

namespace Clinic.Business
{
    public class Prescription

    {
        public enum enMode { AddNew, Update };
        public enMode Mode = enMode.AddNew;

        private static string _ExceptionMessage = string.Empty;

        public PrescriptionDTO prescriptionDTO => new PrescriptionDTO(PrescriptionID, MedicalRecordID,
            MedicationName, Dosage, Frequency, StartDate, EndDate, SpecialInstructions);

        public int PrescriptionID { get; set; }
        public int MedicalRecordID { get; set; }
        public string MedicationName { get; set; }
        public string Dosage { get; set; }
        public string Frequency { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public string SpecialInstructions { get; set; }

        public Prescription(PrescriptionDTO prescriptionDTO, enMode mode = enMode.AddNew)
        {
            PrescriptionID = prescriptionDTO.PrescriptionID;
            MedicalRecordID = prescriptionDTO.MedicalRecordID;
            MedicationName = prescriptionDTO.MedicationName;
            Dosage = prescriptionDTO.Dosage;
            Frequency = prescriptionDTO.Frequency;
            StartDate = prescriptionDTO.StartDate;
            EndDate = prescriptionDTO.EndDate;
            SpecialInstructions = prescriptionDTO.SpecialInstructions;

            Mode = mode;
        }


        public static async Task<Prescription> GetPrescriptionByID(int id)
        {
            try
            {
                PrescriptionDTO prescriptionDTO =  await PrescriptionDataAccess.GetPrescriptionByID(id);

                if (prescriptionDTO != null)
                    return new Prescription(prescriptionDTO, enMode.Update);

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


        public static async Task<List<PrescriptionInfoDTO>> GetPrescriptionsByPatientID(int patientID)
        {
            try
            {
               return await PrescriptionDataAccess.GetPrescriptionsByPatientID(patientID);
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


        private async Task<bool> _AddNewPrescription()
        {
            try
            {
                PrescriptionID = await PrescriptionDataAccess.AddNewPrescription(prescriptionDTO);
                return PrescriptionID != 0;
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

            return false;
        }

        private async Task<bool> _UpdatePrescription()
        {
            try
            {
                return await PrescriptionDataAccess.UpdatePrescription(prescriptionDTO);
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
                        if (await _AddNewPrescription())
                        {

                            Mode = enMode.Update;
                            return true;
                        }
                        else
                        {
                            return false;
                        }

                    case enMode.Update:

                        return await _UpdatePrescription();

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
