using AutoMapper;
using Microsoft.EntityFrameworkCore;
using smarthealth.Data;
using smarthealth.Models;
using smarthealth.Models.Dtos;

namespace smarthealth.Repo
{
    
    public class AppointmentRepo : IAppointmentRepo
    {
        private readonly AppDbContext _db;
        private IMapper _mapper;

        public AppointmentRepo(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public List<AppointmentDto> ViewAppointments(int doctorId)
        {
            List<AppointmentDto> appointmentDtos = null;
            try
            {
                List<Appointment> appointments = _db.Appointments.Where(a => a.DoctorId == doctorId).ToList();

                appointmentDtos = _mapper.Map<List<AppointmentDto>>(appointments);

            }
            catch (Exception ex)
            {
                return appointmentDtos;
            }
            return appointmentDtos;
        }
        public List<AppointmentDto> GetPatientAppointments(string userid)
        {
            List<AppointmentDto> appointmentDtos = null;
            try
            {
                List<Appointment> appointments = _db.Appointments.Where(a => a.UserId == userid).ToList();

                appointmentDtos = _mapper.Map<List<AppointmentDto>>(appointments);

            }
            catch (Exception ex)
            {
                return appointmentDtos;
            }
            return appointmentDtos;
        }
        public AppointmentDto GetAppointmentDetails(int appointmentId)
        {
            AppointmentDto appointmentDto = null;
            try
            {
                Appointment appointment = _db.Appointments.FirstOrDefault(a => a.Id == appointmentId);
            
                appointmentDto = _mapper.Map<AppointmentDto>(appointment);
                

            }
            catch (Exception ex)
            {
                return appointmentDto;
            }
            return appointmentDto;
        }
        public AppointmentDto CreateAppointment(AppointmentDto appointmentDto)
        {
            try
            {
                Appointment appointment = _mapper.Map<Appointment>(appointmentDto);
                appointment.StartTime = DateTime.SpecifyKind(appointment.StartTime, DateTimeKind.Utc);
                appointment.EndTime = DateTime.SpecifyKind(appointment.EndTime, DateTimeKind.Utc);
                _db.Appointments.Add(appointment);
                _db.SaveChanges();
                appointmentDto = _mapper.Map<AppointmentDto>(appointment);

            }
            catch (Exception ex)
            {
                return appointmentDto;
            }
            return appointmentDto;
        }

        public AppointmentDto UpdateAppointment(AppointmentDto appointmentDto)
        {
            try
            {
                // Retrieve the existing appointment from the database based on its ID
                Appointment existingAppointment = _db.Appointments.FirstOrDefault(a => a.Id == appointmentDto.Id);

                // Check if the appointment exists
                if (existingAppointment == null)
                {
                    throw new Exception("Appointment not found.");
                }

                // Update the properties of the existing appointment with values from the DTO
                existingAppointment.DoctorId = appointmentDto.DoctorId;
                existingAppointment.UserId = appointmentDto.UserId;
                existingAppointment.StartTime = appointmentDto.StartTime;
                existingAppointment.EndTime = appointmentDto.EndTime;
                existingAppointment.Title = appointmentDto.Title;
                existingAppointment.Description = appointmentDto.Description;
                existingAppointment.Status = appointmentDto.Status;
                

                // Save the changes to the database
                _db.SaveChanges();

                // Map the updated appointment back to the DTO and return it
                return _mapper.Map<AppointmentDto>(existingAppointment);
            }
            catch (Exception ex)
            {
                // Handle exceptions and return the original appointment DTO
                return appointmentDto;
            }
        }

        public bool CancelAppointment(int id)
        {
            try
            {
                // Retrieve the existing appointment from the database based on its ID
                Appointment existingAppointment = _db.Appointments.FirstOrDefault(a => a.Id == id);

                // Check if the appointment exists
                if (existingAppointment == null)
                {
                    throw new Exception("Appointment not found.");
                }

                existingAppointment.Status = Utility.StaticDetail.Status.Cancelled;
                // Save the changes to the database
                _db.SaveChanges();

                // Map the updated appointment back to the DTO and return it
                return true;
            }
            catch (Exception ex)
            {
                // Handle exceptions and return the original appointment DTO
                return false;
            }
        }

        public bool DeleteAppointment(int id)
        {
            AppointmentDto appointmentDto = null;
            try
            {
                Appointment appointment = _db.Appointments.First(d => d.Id == id);
                _db.Appointments.Remove(appointment);
                _db.SaveChanges();
                appointmentDto = _mapper.Map<AppointmentDto>(appointment);

            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        
    }
}
