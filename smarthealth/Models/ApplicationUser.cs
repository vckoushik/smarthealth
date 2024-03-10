using Microsoft.AspNetCore.Identity;

namespace smarthealth.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<MedicalRecord> MedicalRecords { get; set; }
        public List<Appointment> Appointments { get; set; }
    }
}
