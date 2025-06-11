using Clinic.Business;
using Clinic.DataAccess.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.Api.Controllers
{
    [Route("api/MedicalRecordApiController")]
    [ApiController]
    public class MedicalRecordApiController : ControllerBase
    {
        [HttpGet("GetMedicalRecordByID", Name = "GetMedicalRecordByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetMedicalRecordByID(int id)
        {

            try
            {
                if (id < 1) return BadRequest($"Not Accepted ID : {id}");

                MedicalRecords medicalRecord = await MedicalRecords.GetMedicalRecordByID(id);

                return medicalRecord == null
                ? NotFound($"medicalRecord With ID {id} Not Found")
                : Ok(medicalRecord.medicalRecordDTO);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An unexpected Error occured");
            }
        }



        [HttpGet("GetMedicalRecordsByPatientID", Name = "GetMedicalRecordsByPatientID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetMedicalRecordsByPatientID(int id)
        {

            try
            {
                if (id < 1) return BadRequest($"Not Accepted Patient ID : {id}");

                List<MedicalRecordInfoDTO> medicalRecords = await MedicalRecords.GetMedicalRecordsByPatientID(id);

                return medicalRecords.Count == 0
                ? NotFound($"Patient With ID {id} don't have Medical Records.")
                : Ok(medicalRecords);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An unexpected Error occured");
            }
        }

        [HttpGet("GetMedicalRecordsByDoctorID", Name = "GetMedicalRecordsByDoctorID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetMedicalRecordsByDoctorID(int id)
        {

            try
            {
                if (id < 1) return BadRequest($"Not Accepted Doctor ID : {id}");

                List<MedicalRecordInfoDTO> medicalRecords = await MedicalRecords.GetMedicalRecordsByDoctorID(id);

                return medicalRecords.Count == 0
                ? NotFound($"Doctor With ID {id} don't have Medical Records.")
                : Ok(medicalRecords);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An unexpected Error occured");
            }
        }


        [HttpGet("GetMedicalRecordByAppointmentID", Name = "GetMedicalRecordByAppointmentID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetMedicalRecordByAppointmentID(int id)
        {

            try
            {
                if (id < 1) return BadRequest($"Not Accepted Appointment ID : {id}");

                MedicalRecordInfoDTO medicalRecord = await MedicalRecords.GetMedicalRecordByAppointmentID(id);

                return medicalRecord == null
                ? NotFound($"This Appointment don't have any Medical Records")
                : Ok(medicalRecord);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An unexpected Error occured");
            }
        }



        [HttpPost(Name = "AddNewMedicalRecord")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddNewMedicalRecord(MedicalRecordDTO medicalRecordDTO)
        {
            try
            {
                if (!medicalRecordDTO.IsValid())
                    return BadRequest("Invalid Medical Record Data");

                if (await Appointment.GetAppointmentByAppointmentID(medicalRecordDTO.AppointmentID) == null)
                    return BadRequest("This appointment not exist.");

                if (await MedicalRecords.IsAppointmentHaveMedicalRecord(medicalRecordDTO.AppointmentID))
                    return BadRequest("Appointment already Have medical Record.");

                MedicalRecords medicalRecord = new MedicalRecords(medicalRecordDTO);

                if (!await medicalRecord.Save())
                    return BadRequest("Invalid Doctor Data");

                medicalRecordDTO.MedicalRecordID = medicalRecord.MedicalRecordID;

                return CreatedAtAction("GetMedicalRecordByID", new { id = medicalRecordDTO.MedicalRecordID }, medicalRecordDTO);

            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An unexpected Error occured");
            }
        }


    }
}
