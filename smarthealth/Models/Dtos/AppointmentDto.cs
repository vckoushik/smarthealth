using System.ComponentModel.DataAnnotations;

namespace smarthealth.Models.Dtos
{
    public class AppointmentDto
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public int UserId { get; set; }
        public ApplicationUser? User { get; set; }
        public int DoctorId { get; set; }
        public Doctor? Doctor { get; set; }

    }
}
