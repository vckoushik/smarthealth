using static smarthealth.Utility.StaticDetail;

namespace smarthealth.Models.Dtos
{
    public class CreateAppointmentDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Status Status { get; set; }
        public int UserId { get; set; }
        public int DoctorId { get; set; }
    }
}
