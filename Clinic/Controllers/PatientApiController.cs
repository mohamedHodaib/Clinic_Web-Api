using Clinic.Business;
using Clinic.DataAccess.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;

namespace Clinic.Api.Controllers
{
    [Route("api/PatientApiController")]
    [ApiController]
  public class PatientApiController : ControllerBase
  {
        [HttpGet("GetPatientByID", Name = "GetPatientByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetPatientByID(int id)
        {

            try
            {
                if (id < 1) return BadRequest($"Not Accepted ID : {id}");

                Patient patient = await Patient.GetPatientByID(id);

                return patient == null
                ? NotFound($"Patient With ID {id} Not Found")
                : Ok(patient.patientInfoDTO);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An unexpected Error occured");
            }
        }


            [HttpGet("GetPatientHistoryByID", Name = "GetPatientHistoryByID")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status204NoContent)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            [ProducesResponseType(StatusCodes.Status500InternalServerError)]

            public async Task<IActionResult> GetPatientHistoryByID(int id)
            {

                try
                {
                    if (id < 1) return BadRequest($"Not Accepted ID : {id}");

                    Patient patient = await Patient.GetPatientByID(id);

                    if (patient == null)
                        NotFound($"Patient With ID {id} Not Found");

                    PatientHistoryDTO patientHistoryDTO = await patient.GetPatientHistory();

                    if (patientHistoryDTO == null)
                        return NoContent();

                    return Ok(patientHistoryDTO);
                }
                catch
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        "An unexpected Error occured");
                }
            }



        [HttpGet("GetPatientByPhone", Name = "GetPatientByPhone")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPatientByPhone(string phoneNumber)
        {
            try
            {
                if (!PersonDTO.IsValidPhone(phoneNumber))
                    return BadRequest($"Not Accepted phone number : {phoneNumber}");

                Patient patient = await Patient.GetPatientInfoByPhone(phoneNumber);

                return patient == null
                ? NotFound($"Patient With phone Number {phoneNumber} Not Found")
                : Ok(patient.patientInfoDTO);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An unexpected Error occured");
            }
        }



        [HttpGet("GetPatientByEmail", Name = "GetPatientByEmail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetPatientByEmail(string email)
        {
            try
            {
                if (!PersonDTO.IsValidEmail(email))
                        return BadRequest($"Not Accepted Email : {email}");

                Patient patient = await Patient.GetPatientInfoByEmail(email);

                return patient == null
                ? NotFound($"Patient With Email {email} Not Found")
                : Ok(patient.patientInfoDTO);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An unexpected Error occured");
            }
        }



        [HttpGet("GetAllPatients", Name = "GetAllPatients")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllPatients()
        {
            try
            {

                List<PatientInfoDTO> patientInfoDTOs = await Patient.GetAllPatients();

                return patientInfoDTOs.Count == 0
                ? NotFound($"there is No Patients")
                : Ok(patientInfoDTOs);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An unexpected Error occured");
            }
        }



        [HttpGet("GetPatientsByGender", Name = "GetPatientsByGender")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPatientsByGender(bool gender)
        {

            try
            {
                List<PatientInfoDTO> patientInfoDTOs = await Patient.GetPatientsByGender(gender);

                return patientInfoDTOs.Count == 0
                ? NotFound($"There is no Patients with Gender {(gender ? "Male" : "Female")}")
                : Ok(patientInfoDTOs);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An unexpected Error occured");
            }
        }



        [HttpGet("GetPatientsByAddress", Name = "GetPatientsByAddress")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPatientsByAddress(string address)
        {

            try
            {
                    if (string.IsNullOrEmpty(address))
                        return BadRequest($"Not Accepted address: {address}");

                    List<PatientInfoDTO> patientInfoDTOs = await Patient.GetPatientsByAddress(address);

                return patientInfoDTOs.Count == 0
                ? NotFound($"There is no Patient has address starts with {address}")
                : Ok(patientInfoDTOs);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An unexpected Error occured");
            }
        }


        [HttpGet("GetPatientsByName", Name = "GetPatientsByName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPatientsByName(string name)
        {

            try
            {
                    if (string.IsNullOrEmpty(name))
                        return BadRequest($"Not Accepted Name: {name}");

                    List<PatientInfoDTO> patientInfoDTOs = await Patient.GetPatientsByName(name);

                return patientInfoDTOs.Count == 0
                ? NotFound($"There is no Patients with name starts with {name}")
                : Ok(patientInfoDTOs);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An unexpected Error occured");
            }
        }



        [HttpGet("GetPatientsByDateOfBirth", Name = "GetPatientsByDateOfBirth")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPatientsByDateOfBirth(DateOnly dateOfBirth)
        {
            try
            {
                    if (dateOfBirth == default)
                        return BadRequest($"Not Accepted DateOfBirth: {dateOfBirth},must be dd/mm/yyyy");

                    List<PatientInfoDTO> patientInfoDTOs = await Patient.GetPatientsByDateOfBirth(dateOfBirth);

                return patientInfoDTOs.Count == 0
                ? NotFound($"There is no Patients with date of birth {dateOfBirth}")
                : Ok(patientInfoDTOs);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An unexpected Error occured");
            }
        }


        [HttpPost(Name = "AddPatient")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddPatient(PatientInfoDTO newPatientDTO)
        {
            try
            {
                if (!newPatientDTO.IsValid())
                    return BadRequest("Invalid Patient Data");


                if (await Patient.IsEmailUsed(newPatientDTO.Email))
                    return BadRequest("This Email Already Used.");

                if (await Patient.IsPhoneNumberUsed(newPatientDTO.PhoneNumber))
                    return BadRequest("This Phone Number Already Used.");


                Patient patient = new Patient(newPatientDTO);

                if(!await patient.Save())
                    return BadRequest("Invalid Patient Data");


                newPatientDTO.PatientID = patient.PatientID;
                newPatientDTO.PersonID = patient.PersonID;

                return CreatedAtAction("GetPatientByID", new { id = newPatientDTO.PatientID }, newPatientDTO);

            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An unexpected Error occured");
            }
        }


        [HttpPut(Name = "UpdatePatient")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdatePatient(int id, PatientInfoDTO newPatientDTO)
        {
            try
            {
                

                Patient patient = await Patient.GetPatientByID(id);


                    if (patient == null)
                        return NotFound($"patient With ID {id} Not Found");

                if (!newPatientDTO.IsValid())
                    return BadRequest("Invalid Patient Data");

                if (await Patient.IsEmailUsed(patient.PersonID, newPatientDTO.Email))
                        return BadRequest("This Email Already Used.");

                    if (await Patient.IsPhoneNumberUsed(patient.PersonID, newPatientDTO.PhoneNumber))
                        return BadRequest("This Phone Number Already Used.");

                    patient.Name = newPatientDTO.Name;
                patient.Address = newPatientDTO.Address;
                patient.PhoneNumber = newPatientDTO.PhoneNumber;
                patient.Email = newPatientDTO.Email;


                if(await patient.Save())
                return Ok("Updated Successfully.");

                else
                   return BadRequest("Invalid Patient Data");

                }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An unexpected Error occured");
            }
        }

        [HttpDelete(Name = "DeletePatient")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletePatient(int id)
        {
            try
            {
                if (id < 1)
                    return BadRequest("Invalid Patient ID");

                Patient patient = await Patient.GetPatientByID(id);

                if (patient == null)
                    return NotFound($"Patient With ID {id} Not Found");

                await patient.Delete();

                return Ok("Deleted Successfully");

            }
            catch (SqlException ex) when (ex.Number == 547)
            {
                return BadRequest("This Patient cannot be deleted ,because has other related data");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An unexpected Error occured");
            }
        }
   }
}
