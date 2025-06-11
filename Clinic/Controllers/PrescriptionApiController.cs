using Clinic.Business;
using Clinic.DataAccess.DTOs;
using Clinic.DataAccess.DTOs.Clinic.DataAccess.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.Api.Controllers
{
    [Route("api/PrescriptionApiController")]
    [ApiController]
    public class PrescriptionApiController : ControllerBase
    {

        [HttpGet("GetPrescriptionByID", Name = "GetPrescriptionByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetPrescriptionByID(int id)
        {

            try
            {
                if (id < 1) return BadRequest($"Not Accepted Prescription ID : {id}");

                Prescription presc = await Prescription.GetPrescriptionByID(id);

                return presc == null
                ? NotFound($"This Prescription Not Found")
                : Ok(presc.prescriptionDTO);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An unexpected Error occured");
            }
        }




        [HttpGet("GetPrescriptionsByPatientID", Name = "GetPrescriptionsByPatientID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetPrescriptionsByPatientID(int id)
        {

            try
            {
                if (id < 1) return BadRequest($"Not Accepted Appointment ID : {id}");

                List<PrescriptionInfoDTO> prescriptionInfoDTOs = await Prescription.GetPrescriptionsByPatientID(id);

                return prescriptionInfoDTOs.Count == 0
                ? NotFound($"Patient don't have any Prescriptions.")
                : Ok(prescriptionInfoDTOs);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An unexpected Error occured");
            }
        }


        [HttpPost(Name = "AddNewPrescription")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddNewPrescription(PrescriptionDTO newprescriptionDTO)
        {
            try
            {
                if (!newprescriptionDTO.IsValid())
                    return BadRequest("Invalid Prescription Data");

                if (await MedicalRecords.GetMedicalRecordByID(newprescriptionDTO.MedicalRecordID) == null)
                    return BadRequest("the medical Record doesn't exist.");

                Prescription prescription = new Prescription(newprescriptionDTO);

                if (!await prescription.Save())
                    return BadRequest("Invalid Prescription Data");

                newprescriptionDTO.PrescriptionID = prescription.PrescriptionID;

                return CreatedAtRoute("GetPrescriptionByID", 
                    new { id = newprescriptionDTO.PrescriptionID }, newprescriptionDTO);

            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An unexpected Error occured");
            }
        }
    }
}
