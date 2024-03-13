using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace smarthealth.Controllers
{
    [Route("api/appointment")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        [HttpGet]
        [Route("time-slots")]
        public IActionResult GetTimeSlots(DateTime selectedDate)
        {
            // Ensure the selected date falls within Monday to Friday
            if (selectedDate.DayOfWeek < DayOfWeek.Monday || selectedDate.DayOfWeek > DayOfWeek.Friday)
            {
                return BadRequest("Selected date must be between Monday and Friday.");
            }
            if (selectedDate.Date < DateTime.Today)
            {
                return BadRequest("Selected date must be today or a future date.");
            }

            // Create a list to store the available time slots
            List<DateTime> timeSlots = new List<DateTime>();

            // Add time slots from 9 am to 9 pm with 1-hour intervals
            DateTime startTime = selectedDate.Date.AddHours(9);
            DateTime endTime = selectedDate.Date.AddHours(21); // 9 pm

            while (startTime < endTime)
            {
                timeSlots.Add(startTime);
                startTime = startTime.AddHours(1);
            }

            return Ok(timeSlots);
        }
    }
}
