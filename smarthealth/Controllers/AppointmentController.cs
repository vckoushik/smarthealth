using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using smarthealth.Models.Dtos;
using smarthealth.Repo;

namespace smarthealth.Controllers
{

    [Route("api/appointment")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentRepo _appointmentRepo;
        private ResponseDto _response;
        public AppointmentController(IAppointmentRepo appointmentRepo)
        {
            _appointmentRepo = appointmentRepo;
            _response = new ResponseDto();
        }

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
        [HttpGet]
        [Route("GetAppointments/")]
        public ResponseDto GetAppointments()
        {
            try
            {
                List<AppointmentDto> AppointmentDtos = _appointmentRepo.GetAppointments();
                if (AppointmentDtos == null)
                {
                    throw new Exception("Appointments Not Found");
                }
                else
                {
                    _response.IsSuccess = true;
                    _response.Result = AppointmentDtos;
                    _response.Message = "Found Appointment";
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;

            }
            return _response;
        }

        [HttpGet]
        [Route("GetAppointments/{doctorId}")]
        public ResponseDto GetAppointments(int doctorId)
        {
            try
            {
                List<AppointmentDto> AppointmentDtos = _appointmentRepo.ViewAppointments(doctorId);
                if (AppointmentDtos == null)
                {
                    throw new Exception("Appointments Not Found");
                }
                else
                {
                    _response.IsSuccess = true;
                    _response.Result = AppointmentDtos;
                    _response.Message = "Found Appointment";
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;

            }
            return _response;
        }

        [HttpGet]
        [Route("GetPaitentAppointments/{userid}")]
        public ResponseDto GetPatientAppointments(string userid)
        {
            try
            {
                List<AppointmentDto> AppointmentDtos = _appointmentRepo.GetPatientAppointments(userid);
                if (AppointmentDtos == null)
                {
                    throw new Exception("Appointments Not Found");
                }
                else
                {
                    _response.IsSuccess = true;
                    _response.Result = AppointmentDtos;
                    _response.Message = "Found Appointment";
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;

            }
            return _response;
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public ResponseDto GetAppointmentById(int id)
        {
            try
            {
                AppointmentDto AppointmentDto = _appointmentRepo.GetAppointmentDetails(id);
                if (AppointmentDto == null)
                {
                    throw new Exception("Appointment Not Found");
                }
                else
                {
                    _response.IsSuccess = true;
                    _response.Result = AppointmentDto;
                    _response.Message = "Found Appointment";
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;

            }
            return _response;
        }

        [HttpPost]
        [Route("CreateAppointment")]
        public ResponseDto CreateAppointment([FromBody] AppointmentDto AppointmentDto)
        {
            try
            {
                AppointmentDto = _appointmentRepo.CreateAppointment(AppointmentDto);
                _response.IsSuccess = true;
                _response.Result = AppointmentDto;
                _response.Message = "Appointments Added Successfully";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpPut]
        [Route("UpdateAppointment")]
        public ResponseDto UpdateAppointment([FromBody] AppointmentDto AppointmentDto)
        {
            try
            {
                AppointmentDto = _appointmentRepo.UpdateAppointment(AppointmentDto);
                _response.IsSuccess = true;
                _response.Result = AppointmentDto;
                _response.Message = "Appointments Added Successfully";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }
        [HttpPut]
        [Route("cancelappointment/{id:int}")]
        public ResponseDto CancelAppointment(int id)
        {
            try
            {
                bool result = _appointmentRepo.CancelAppointment(id);
                if (result == false)
                {
                    throw new Exception("Appointment Not found");
                }
                _response.IsSuccess = true;
                _response.Message = "Appointment Cancelled Successfully";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPut]
        [Route("approveappointment/{id:int}")]
        public ResponseDto ApproveAppointment(int id)
        {
            try
            {
                bool result = _appointmentRepo.ApproveAppointment(id);
                if (result == false)
                {
                    throw new Exception("Appointment Not found");
                }
                _response.IsSuccess = true;
                _response.Message = "Appointment Approved Successfully";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPut]
        [Route("completeappointment/{id:int}")]
        public ResponseDto CompleteAppointnment(int id)
        {
            try
            {
                bool result = _appointmentRepo.CompleteAppointment(id);
                if (result == false)
                {
                    throw new Exception("Appointment Not found");
                }
                _response.IsSuccess = true;
                _response.Message = "Appointment Completed Successfully";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        [HttpDelete]
        [Route("{id:int}")]
        public ResponseDto DeleteAppointment(int id)
        {
            try
            {
                bool result = _appointmentRepo.DeleteAppointment(id);
                if (result == false)
                {
                    throw new Exception("Appointment Not found");
                }
                _response.IsSuccess = true;
                _response.Message = "Appointment Deleted Successfully";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

    }
}
