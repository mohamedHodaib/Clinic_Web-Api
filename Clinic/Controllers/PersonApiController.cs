using Clinic.Business;
using Clinic.DataAccess.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Net;

namespace Clinic.Api.Controllers
{
    [Route("api/PersonApiController")]
    [ApiController]
    public class PersonApiController : ControllerBase
    {
        [HttpGet("GetPersonByID", Name = "GetPersonByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetPersonByID(int id)
        {

            try
            {
                if (id < 0) return BadRequest($"Not Accepted ID : {id}");

                Person person = await Person.GetPersonByID(id);

                return person == null
                ? NotFound($"Person With ID {id} Not Found")
                : Ok(person.personDTO);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An unexpected Error occured");
            }
        }


        [HttpGet("GetPersonByPhone", Name = "GetPersonByPhone")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPersonByPhone(string phoneNumber)
        {
            try
            {
                if (!PersonDTO.IsValidPhone(phoneNumber)) 
                    return BadRequest($"Not Accepted phone number : {phoneNumber}");

                Person person = await Person.GetPersonInfoByPhone(phoneNumber);

                return person == null
                ? NotFound($"Person With phone Number {phoneNumber} Not Found")
                : Ok(person.personDTO);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An unexpected Error occured");
            }
        }



        [HttpGet("GetPersonByEmail", Name = "GetPersonByEmail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetPersonByEmail(string email)
        {
            try
            {
                if (!PersonDTO.IsValidEmail(email)) 
                    return BadRequest($"Not Accepted Email : {email}");

                Person person = await Person.GetPersonInfoByEmail(email);

                return person == null
                ? NotFound($"Person With Email {email} Not Found")
                : Ok(person.personDTO);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An unexpected Error occured");
            }
        }



        [HttpGet("GetAllPersons", Name = "GetAllPersons")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllPersons()
        {
            try
            {

                List<PersonDTO> personDTOs = await Person.GetAllPersons();

                return personDTOs.Count == 0
                ? NotFound($"there is No Persons")
                : Ok(personDTOs);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An unexpected Error occured");
            }
        }



        [HttpGet("GetPersonsByGender", Name = "GetPersonsByGender")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPersonsByGender(bool gender)
        {

            try
            {
                List<PersonDTO> personDTOs = await Person.GetPersonsByGender(gender);

                return personDTOs.Count == 0
                ? NotFound($"There is no Persons with Gender {(gender ? "Male" : "Female")}")
                : Ok(personDTOs);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An unexpected Error occured");
            }
        }



        [HttpGet("GetPersonsByAddress", Name = "GetPersonsByAddress")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPersonsByAddress(string address)
        {

            try
            {
                if(string.IsNullOrEmpty(address))
                    return BadRequest($"Not Accepted Address: {address}");  

                List<PersonDTO> personDTOs = await Person.GetPersonsByAddress(address);

                return personDTOs.Count == 0
                ? NotFound($"There is no Person has address starts with {address}")
                : Ok(personDTOs);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An unexpected Error occured");
            }
        }


        [HttpGet("GetPersonsByName", Name = "GetPersonsByName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPersonsByName(string name)
        {

            try
            {
                if (string.IsNullOrEmpty(name))
                    return BadRequest($"Not Accepted Name: {name}");

                List<PersonDTO> personDTOs = await Person.GetPersonsByName(name);

                return personDTOs.Count == 0
                ? NotFound($"There is no Persons with name starts with {name}")
                : Ok(personDTOs);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An unexpected Error occured");
            }
        }



        [HttpGet("GetPersonsByDateOfBirth", Name = "GetPersonsByDateOfBirth")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetPersonsByDateOfBirth(DateOnly dateOfBirth)
        {

            try
            {
                if (dateOfBirth == default)
                    return BadRequest($"Not Accepted DateOfBirth: {dateOfBirth},must be dd/mm/yyyy");
                List<PersonDTO> personDTOs = await Person.GetPersonsByDateOfBirth(dateOfBirth);

                return personDTOs.Count == 0
                ? NotFound($"There is no Persons with date of birth {dateOfBirth}")
                : Ok(personDTOs);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An unexpected Error occured");
            }
        }


        [HttpPost(Name = "AddPerson")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddPerson(PersonDTO newPersonDTO)
        {
            try
            {
                if (!newPersonDTO.IsValid())
                    return BadRequest("Invalid Person Data");


                if(await Person.IsEmailUsed(newPersonDTO.Email))
                    return BadRequest("This Email Already Used.");

                if (await Person.IsPhoneNumberUsed(newPersonDTO.PhoneNumber))
                    return BadRequest("This Phone Number Already Used.");


                Person person = new Person(newPersonDTO);

                person.Save();

                newPersonDTO.PersonID = person.PersonID;

                return CreatedAtAction("GetPersonByID", new { id = newPersonDTO.PersonID }, newPersonDTO);

            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An unexpected Error occured");
            }
        }


        [HttpPut(Name = "UpdatePerson")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdatePerson(int id, PersonDTO newPersonDTO)
        {
            try
            {
                

                Person person = await Person.GetPersonByID(id);

               

                if (person == null)
                    return NotFound($"Person With ID {id} Not Found");

                if (!newPersonDTO.IsValid())
                    return BadRequest("Invalid Person Data");

                if (await Person.IsEmailUsed(id, newPersonDTO.Email))
                    return BadRequest("This Email Already Used.");

                if (await Person.IsPhoneNumberUsed(id, newPersonDTO.PhoneNumber))
                    return BadRequest("This Phone Number Already Used.");

                person.Name = newPersonDTO.Name;
                person.Address = newPersonDTO.Address;
                person.PhoneNumber = newPersonDTO.PhoneNumber;
                person.Email = newPersonDTO.Email;


                if(await person.Save())
                  return Ok("Updated Successfully.");

                else
                    return BadRequest("Invalid Person Data");

            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An unexpected Error occured");
            }
        }

        [HttpDelete(Name = "DeletePerson")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeletePerson(int id)
        {
            try
            {
                if (id < 1)
                    return BadRequest("Invalid Person ID");

                Person person = await Person.GetPersonByID(id);

                if (person == null)
                    return NotFound($"Person With ID {id} Not Found");

                await person.Delete();

                return Ok("Deleted Successfully");

            }
            catch (SqlException ex) when (ex.Number == 547)
            {
                return BadRequest("This Person cannot be deleted ,because has other related data");
            }
            catch (Exception ex) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An unexpected Error occured");
            }
        }
    }
}
