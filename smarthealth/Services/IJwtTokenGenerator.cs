using smarthealth.Models;

namespace smarthealth.Service
{
    public interface IJwtTokenGenerator
    {
        public string GenerateToken(ApplicationUser applicationUser, IEnumerable<string> roles);
    }
}
