﻿using smarthealth.Models.Dtos;

namespace smarthealth.Repo
{
    public interface IAppointmentRepo
    {
        public List<AppointmentDto> GetAppointments();
        public List<AppointmentDto> ViewAppointments(int doctorId);
        public List<AppointmentDto> GetPatientAppointments(string userid);

        public AppointmentDto GetAppointmentDetails(int appointmentId);
        public AppointmentDto CreateAppointment(AppointmentDto appointmentDto);
        public bool CancelAppointment(int id);
        public bool ApproveAppointment(int id);
        public bool CompleteAppointment(int id);
        public bool DeleteAppointment(int id);
        public AppointmentDto UpdateAppointment(AppointmentDto appointmentDto);
    }
}
