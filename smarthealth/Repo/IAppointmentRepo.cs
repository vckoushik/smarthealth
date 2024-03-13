using smarthealth.Models.Dtos;

namespace smarthealth.Repo
{
    public interface IAppointmentRepo
    {
        public List<AppointmentDto> ViewAppointments(string userid);
        public AppointmentDto GetAppointmentDetails(int appointmentId);
        public AppointmentDto CreateAppointment(AppointmentDto appointmentDto);
        public Boolean CancelAppointment(int id);
        public Boolean DeleteAppointment(int id);
        public AppointmentDto UpdateAppointment(AppointmentDto appointmentDto);
    }
}
