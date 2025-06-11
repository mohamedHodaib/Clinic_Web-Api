using Clinic.DataAccess.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Clinic.Business;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Clinic.Api.Controllers
{

    


    [Route("api/AppointmentApiController")]
    [ApiController]

    public class AppointmentApiController : ControllerBase
    {
        [HttpGet("GetAppointmentByAppointmentID", Name = "GetAppointmentByAppointmentID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetAppointmentByAppointmentID(int id)
        {
               

            try
            {
                if (id < 0) return BadRequest($"Not Accepted ID : {id}");

                Appointment appointment = await Appointment.GetAppointmentByAppointmentID(id);

                return appointment == null
                ? NotFound($"Appointment With ID {id} Not Found")
                : Ok(appointment.appointmentDTO);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An unexpected Error occured");
            }
        }


        [HttpGet("GetAppointmentsByPatientID", Name = "GetAppointmentsByPatientID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetAppointmentsByPatientID(int id)
        {
            try
            {
                if (id < 0) return BadRequest($"Not Accepted ID : {id}");

                List<AppointmentReportDTO> appointmentReportDTOs = await Appointment.GetAppointmentsByPatientID(id);

                return  appointmentReportDTOs.Count == 0
                ? NotFound($"Patient With ID {id} Not Have any Appointments")
                : Ok(appointmentReportDTOs);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An unexpected Error occured");
            }
        }



        [HttpGet("GetAppointmentsByDoctorID", Name = "GetAppointmentsByDoctorID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAppointmentsByDoctorID(int id)
        {
            

            try
            {
                if (id < 0) return BadRequest($"Not Accepted ID : {id}");

                List<AppointmentReportDTO> appointmentReportDTOs = await Appointment.GetAppointmentsByDoctorID(id);

                if (appointmentReportDTOs == null)
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        "An unexpected Error occured");
                return appointmentReportDTOs.Count == 0
                ? NotFound($"Doctor With ID {id} Not Have any Appointments")
                : Ok(appointmentReportDTOs);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An unexpected Error occured");
            }
        }



        [HttpGet("GetAppointmentsByDate", Name = "GetAppointmentsByDate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAppointmentsByDate(DateOnly date)
        {
            try
            {
                if (date == default)
                    return BadRequest($"Not Accepted Date : {date}");

                List<AppointmentReportDTO> appointmentReportDTOs = await Appointment.GetAppointmentsByDate(date);

                return appointmentReportDTOs == null
                ? NotFound($"there is No Appointments at {date}")
                : Ok(appointmentReportDTOs);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An unexpected Error occured");
            }
        }



        [HttpGet("GetAppointmentsByStatus", Name = "GetAppointmentsByStatus")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAppointmentsByStatus(Appointment.AppointmentStatus status)
        {

            try
            {
                List<AppointmentReportDTO> appointmentReportDTOs = await Appointment.GetAppointmentsByStatus((byte)status);

                return appointmentReportDTOs.Count == 0
                ? NotFound($"There is no Appointments with Status {status.ToString()}")
                : Ok(appointmentReportDTOs);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An unexpected Error occured");
            }
        }



        [HttpGet("GetDoctorSchedulePerDay", Name = "GetDoctorSchedulePerDay")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDoctorSchedulePerDay(int id, DateOnly day)
        {
            try
            {
                if (id < 0) return BadRequest($"Not Accepted ID : {id}");
                if (day == default)
                    return BadRequest($"Not Accepted day : {day}");

                List<AppointmentReportDTO> appointmentReportDTOs =
                    await Appointment.GetDoctorSchedulePerDay(id, day);

                return appointmentReportDTOs.Count == 0
                ? NotFound($"Doctor With ID {id} Not Have any Appointments at {day}")
                : Ok(appointmentReportDTOs);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An unexpected Error occured");
            }
        }



        [HttpGet("GetDoctorSchedulePerToday", Name = "GetDoctorSchedulePerToday")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDoctorSchedulePerToday(int id)
            => await GetDoctorSchedulePerDay(id, DateOnly.FromDateTime( DateTime.Now));



        [HttpPost(Name = "AddNewAppointment")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddNewAppointment(AppointmentDTO newAppointmentDTO)
        {
            try
            {
                if (newAppointmentDTO == null
                    || newAppointmentDTO.AppointmentDateTime.Date < DateTime.Now.Date
                    || newAppointmentDTO.AppointmentDateTime.Hour <= DateTime.Now.Hour
                    || newAppointmentDTO.PatientID <= 0
                    || newAppointmentDTO.DoctorID <= 0)
                    return BadRequest("Invalid Appointment Data");


                if (await Appointment.IsReserved(newAppointmentDTO.AppointmentDateTime))
                    return BadRequest("Appointment Can't be scheduled because there is another appointment at the this time.");

                Appointment appointment = new Appointment(newAppointmentDTO);

                await appointment.Save();

                newAppointmentDTO.AppointmentID = appointment.AppointmentID;
                newAppointmentDTO.AppointmentStatus = appointment.Status;

                return CreatedAtRoute("GetAppointmentByAppointmentID", new { id = newAppointmentDTO.AppointmentID }, newAppointmentDTO);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An unexpected Error occured");
            }
        }

        [HttpPut(Name = "UpdateAppointment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAppointment(int id, DateTime newDateTime)
        {
            try
            {
                

                Appointment appointment = await Appointment.GetAppointmentByAppointmentID(id);

                if (appointment == null)
                    return NotFound($"Appointment With ID {id} Not Found");

                if (newDateTime <= DateTime.MinValue || newDateTime >= DateTime.MaxValue)
                    return BadRequest("Invalid Appointment DateTime");

                if (await Appointment.IsReserved(newDateTime))
                    return BadRequest("Appointment Can't be scheduled because there is another appointment at the this time.");

                appointment.AppointmentDateTime = newDateTime;
                appointment.Status = (byte)Appointment.AppointmentStatus.ReScheduled;

                if(await appointment.Save())
                  return Ok("Updated Successfully");

                else
                    return BadRequest("Invalid Appointment Data");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An unexpected Error occured");
            }
        }
    }
}
