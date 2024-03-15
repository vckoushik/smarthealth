using smarthealth.Models.Dtos;

namespace smarthealth.Repo
{
    public interface IAppointmentRepo
    {
        public List<AppointmentDto> ViewAppointments(int doctorId);
        public AppointmentDto GetAppointmentDetails(int appointmentId);
        public AppointmentDto CreateAppointment(AppointmentDto appointmentDto);
        public bool CancelAppointment(int id);
        public bool DeleteAppointment(int id);
        public AppointmentDto UpdateAppointment(AppointmentDto appointmentDto);
    }
}
