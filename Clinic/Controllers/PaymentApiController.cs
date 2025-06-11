using Clinic.Business;
using Clinic.DataAccess.DTOs.Clinic.DataAccess.DTOs;
using Clinic.DataAccess.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.Api.Controllers
{
    [Route("api/PaymentApiController")]
    [ApiController]
    public class PaymentApiController : ControllerBase
    {

        [HttpGet("GetPaymentByID", Name = "GetPaymentByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetPaymentByID(int id)
        {

            try
            {
                if (id < 1) return BadRequest($"Not Accepted Payment ID : {id}");

                
                Payment payment = await Payment.GetPaymentByID(id);

                return payment == null
                ? NotFound($"Payment Not Found.")
                : Ok(payment.paymentDTO);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An unexpected Error occured");
            }
        }


        [HttpGet("GetPaymentHistoryByPatientID", Name = "GetPaymentHistoryByPatientID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetPaymentHistoryByPatientID(int id)
        {

            try
            {
                if (id < 1) return BadRequest($"Not Accepted Patient ID : {id}");

                List<PaymentHistoryDTO> paymentHistoryDTOs = await Payment.GetPaymentHistoryByPatientID(id);

                return  paymentHistoryDTOs.Count == 0
                ? NotFound($"Patient doesn't have any Payment History")
                : Ok(paymentHistoryDTOs);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An unexpected Error occured");
            }
        }




        [HttpGet("GetPaymentHistoryByAppointmentID", Name = "GetPaymentHistoryByAppointmentID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetPaymentHistoryByAppointmentID(int id)
        {

            try
            {
                if (id < 1) return BadRequest($"Not Accepted Appointment ID : {id}");

                if (await Appointment.GetAppointmentByAppointmentID(id) == null)
                    return BadRequest("This appointment not exist.");

                PaymentHistoryDTO paymentHistoryDTO = await Payment.GetPaymentHistoryByAppointmentID(id);

                return paymentHistoryDTO == null
                ? NotFound($"this Appointment is not exist.")
                : Ok(paymentHistoryDTO);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An unexpected Error occured");
            }
        }


        
            [HttpGet("GetPaymentHistoryByWithinDateRange", Name = "GetPaymentHistoryByWithinDateRange")]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            [ProducesResponseType(StatusCodes.Status500InternalServerError)]

            public async Task<IActionResult> GetPaymentHistoryByWithinDateRange(DateOnly start ,DateOnly end)
            {

                try
                {
                    if (start < end) return BadRequest($"Invalid Data.");

                    List<PaymentHistoryDTO> paymentHistoryDTOs 
                    = await Payment.GetPaymentHistoryByWithinDateRange(start,end);

                    return paymentHistoryDTOs.Count == 0
                    ? NotFound($"Patient doesn't have any Payment History")
                    : Ok(paymentHistoryDTOs);
                }
                catch
                {
                    return StatusCode(StatusCodes.Status500InternalServerError,
                        "An unexpected Error occured");
                }
            }



        [HttpPost(Name = "AddNewPaymentRecord")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddNewPaymentRecord(PaymentDTO paymentDTO)
        {
            try
            {
                if (!paymentDTO.IsValid())
                    return BadRequest("Invalid Payment Data");

                if (await Payment.IsAppointmentPaidFor(paymentDTO.AppointmentID))
                    return BadRequest("This Appointment Already Paid.");

                if (await Appointment.GetAppointmentByAppointmentID(paymentDTO.AppointmentID) == null)
                    return BadRequest("This appointment not exist.");


                Payment payment = new Payment(paymentDTO);

                if (!await payment.Save())
                    return BadRequest("Invalid Payment Data");

                paymentDTO.PaymentID = payment.PaymentID;

                return CreatedAtRoute("GetPrescriptionByID",
                    new { id = paymentDTO.PaymentID }, paymentDTO);

            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An unexpected Error occured");
            }
        }
    }
}
