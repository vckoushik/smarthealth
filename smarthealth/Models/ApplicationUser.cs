using Microsoft.AspNetCore.Identity;

namespace smarthealth.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
