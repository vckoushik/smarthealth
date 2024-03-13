using smarthealth.Models.Dtos;

namespace smarthealth.Repo
{
    public class AppointmentRepo : IAppointmentRepo
    {
        public List<AppointmentDto> ViewAppointments(string userid)
        {
            throw new NotImplementedException();
        }
        public AppointmentDto GetAppointmentDetails(int appointmentId)
        {
            throw new NotImplementedException();
        }
        public AppointmentDto CreateAppointment(AppointmentDto appointmentDto)
        {
            throw new NotImplementedException();
        }

        public AppointmentDto UpdateAppointment(AppointmentDto appointmentDto)
        {
            throw new NotImplementedException();
        }

        public bool CancelAppointment(int id)
        {
            throw new NotImplementedException();
        }

        public bool DeleteAppointment(int id)
        {
            throw new NotImplementedException();
        }
        
    }
}
