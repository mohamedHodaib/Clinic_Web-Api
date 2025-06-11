using Clinic.Business;
using Clinic.DataAccess.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Clinic.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorApiController : ControllerBase
    {
        [HttpGet("GetDoctorByID", Name = "GetDoctorByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetDoctorByID(int id)
        {

            try
            {
                if (id < 1) return BadRequest($"Not Accepted ID : {id}");

                Doctor doctor = await Doctor.GetDoctorByID(id);

                return doctor == null
                ? NotFound($"Doctor With ID {id} Not Found")
                : Ok(doctor.doctorInfoDTO);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An unexpected Error occured");
            }
        }


        [HttpGet("GetDoctorByPhone", Name = "GetDoctorByPhone")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDoctorByPhone(string phoneNumber)
        {
            try
            {
                if (!PersonDTO.IsValidPhone(phoneNumber))
                    return BadRequest($"Not Accepted phone number : {phoneNumber}");

                Doctor doctor = await Doctor.GetDoctorInfoByPhone(phoneNumber);

                return doctor == null
                ? NotFound($"Doctor With phone Number {phoneNumber} Not Found")
                : Ok(doctor.doctorInfoDTO);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An unexpected Error occured");
            }
        }



        [HttpGet("GetDoctorByEmail", Name = "GetDoctorByEmail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetDoctorByEmail(string email)
        {
            try
            {
                if (!PersonDTO.IsValidEmail(email))
                    return BadRequest($"Not Accepted Email : {email}");

                Doctor doctor = await Doctor.GetDoctorInfoByEmail(email);

                return doctor == null
                ? NotFound($"Doctor With Email {email} Not Found")
                : Ok(doctor.doctorInfoDTO);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An unexpected Error occured");
            }
        }



        [HttpGet("GetAllDoctors", Name = "GetAllDoctors")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllDoctors()
        {
            try
            {

                List<DoctorInfoDTO> doctorInfoDTOs = await Doctor.GetAllDoctors();

                return doctorInfoDTOs.Count == 0
                ? NotFound($"there is No Doctors")
                : Ok(doctorInfoDTOs);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An unexpected Error occured");
            }
        }



        [HttpGet("GetDoctorsByGender", Name = "GetDoctorsByGender")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDoctorsByGender(bool gender)
        {

            try
            {
                List<DoctorInfoDTO> doctorInfoDTOs = await Doctor.GetDoctorsByGender(gender);

                return doctorInfoDTOs.Count == 0
                ? NotFound($"There is no Doctors with Gender {(gender ? "Male" : "Female")}")
                : Ok(doctorInfoDTOs);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An unexpected Error occured");
            }
        }



        [HttpGet("GetDoctorsByAddress", Name = "GetDoctorsByAddress")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDoctorsByAddress(string address)
        {

            try
            {
                if (string.IsNullOrEmpty(address))
                    return BadRequest($"Not Accepted address: {address}");

                List<DoctorInfoDTO> doctorInfoDTOs = await Doctor.GetDoctorsByAddress(address);

                return doctorInfoDTOs.Count == 0
                ? NotFound($"There is no Doctor has address starts with {address}")
                : Ok(doctorInfoDTOs);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An unexpected Error occured");
            }
        }


        [HttpGet("GetDoctorsByName", Name = "GetDoctorsByName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDoctorsByName(string name)
        {

            try
            {
                if (string.IsNullOrEmpty(name))
                    return BadRequest($"Not Accepted Name: {name}");

                List<DoctorInfoDTO> doctorInfoDTOs = await Doctor.GetDoctorsByName(name);

                return doctorInfoDTOs.Count == 0
                ? NotFound($"There is no Doctors with name starts with {name}")
                : Ok(doctorInfoDTOs);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An unexpected Error occured");
            }
        }



        [HttpGet("GetDoctorsByDateOfBirth", Name = "GetDoctorsByDateOfBirth")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDoctorsByDateOfBirth(DateOnly dateOfBirth)
        {

            try
            {
                if (dateOfBirth == default)
                    return BadRequest($"Not Accepted DateOfBirth: {dateOfBirth},must be dd/mm/yyyy");

                List<DoctorInfoDTO> doctorInfoDTOs = await Doctor.GetDoctorsByDateOfBirth(dateOfBirth);

                return doctorInfoDTOs.Count == 0
                ? NotFound($"There is no Doctors with date of birth {dateOfBirth}")
                : Ok(doctorInfoDTOs);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An unexpected Error occured");
            }
        }


        [HttpGet("GetDoctorsBySpecialization", Name = "GetDoctorsBySpecialization")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDoctorsBySpecialization(string specialization)
        {

            try
            {
                if (string.IsNullOrEmpty(specialization))
                    return BadRequest($"Not Accepted Specialization: {specialization}");

                List<DoctorInfoDTO> doctorInfoDTOs = await Doctor.GetDoctorsBySpecialization(specialization);

                return doctorInfoDTOs.Count == 0
                ? NotFound($"There is no Doctors with Specialization {specialization}")
                : Ok(doctorInfoDTOs);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An unexpected Error occured");
            }
        }



        [HttpPost(Name = "AddDoctor")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddDoctor(DoctorInfoDTO newDoctorDTO)
        {
            try
            {
                if (!newDoctorDTO.IsValid())
                    return BadRequest("Invalid Doctor Data");

                if(!newDoctorDTO.IsValidDateOfBirth())
                    return BadRequest("your age is not conveniant");

                if (!newDoctorDTO.IsValidSpecialization())
                    return BadRequest("Specialization is not accepted,Enter a valid one.");

                if (await Doctor.IsEmailUsed(newDoctorDTO.Email))
                    return BadRequest("This Email Already Used.");

                if (await Doctor.IsPhoneNumberUsed(newDoctorDTO.PhoneNumber))
                    return BadRequest("This Phone Number Already Used.");


                Doctor doctor = new Doctor(newDoctorDTO);

                if(!await doctor.Save())
                    return BadRequest("Invalid Doctor Data");

                newDoctorDTO.DoctorID = doctor.DoctorID;
                newDoctorDTO.PersonID = doctor.PersonID;

                return CreatedAtAction("GetDoctorByID", new { id = newDoctorDTO.DoctorID }, newDoctorDTO);

            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An unexpected Error occured");
            }
        }


        [HttpPut(Name = "UpdateDoctor")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateDoctor(int id, DoctorInfoDTO newDoctorDTO)
        {
            try
            {

                Doctor doctor = await Doctor.GetDoctorByID(id);


                if (doctor == null)
                    return NotFound($"doctor With ID {id} Not Found");



                if (await Doctor.IsEmailUsed(doctor.PersonID, newDoctorDTO.Email))
                    return BadRequest("This Email Already Used.");

                if (await Doctor.IsPhoneNumberUsed(doctor.PersonID, newDoctorDTO.PhoneNumber))
                    return BadRequest("This Phone Number Already Used.");

                doctor.Name = newDoctorDTO.Name;
                doctor.Address = newDoctorDTO.Address;
                doctor.PhoneNumber = newDoctorDTO.PhoneNumber;
                doctor.Email = newDoctorDTO.Email;


                if (await doctor.Save())
                    return Ok("Updated Successfully.");

                else
                    return BadRequest("Invalid Doctor Data");

            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An unexpected Error occured");
            }
        }

        [HttpDelete(Name = "DeleteDoctor")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            try
            {
                if (id < 1)
                    return BadRequest("Invalid Doctor ID");

                Doctor doctor = await Doctor.GetDoctorByID(id);

                if (doctor == null)
                    return NotFound($"Doctor With ID {id} Not Found");

                await doctor.Delete();

                return Ok("Deleted Successfully");

            }
            catch (SqlException ex) when (ex.Number == 547)
            {
                return BadRequest("This Doctor cannot be deleted ,because has other related data");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An unexpected Error occured");
            }
        }
    }
}
