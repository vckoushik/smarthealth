using smarthealth.Models;
using smarthealth.Models.Dtos;
using smarthealth.Data;
using Microsoft.AspNetCore.Identity;
using smarthealth.Service;

namespace smarthealth.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(AppDbContext appDbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IJwtTokenGenerator jwtTokenGenerator = null)
        {
            _appDbContext = appDbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }
        /*
           1. Get the User from DB for given username
           2. Check if role exists else add it
           3. Create a new role for user
       */
        public async Task<bool> AssignRole(string email, string roleName)
        {
            var user = _appDbContext.ApplicationUsers.FirstOrDefault(u => u.UserName.ToLower() == email.ToLower());
            if (user != null)
            {
                if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                {
                    _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
                }
                await _userManager.AddToRoleAsync(user,roleName);
                return true;
            }
            return false;
        }

        /*
        1. Get the User from DB for given username
        2. Check Password 
        3. If user found generate JWT token
        */
        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var user = _appDbContext.ApplicationUsers.FirstOrDefault(u => u.UserName.ToLower() == loginRequestDto.UserName.ToLower());
            bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);
            var role = await _userManager.GetRolesAsync(user);
            if (user == null || isValid == false)
            {
                return new LoginResponseDto() { UserDto = null, Token = "" };

            }
            UserDto userDto = new()
            {
                Email = user.Email,
                ID = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Role = role.First(),
            };
            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtTokenGenerator.GenerateToken(user,roles);
            LoginResponseDto loginResponseDto = new()
            {
                UserDto = userDto,
                Token = token
            };

            return loginResponseDto;

        }

        /*
         1. Create Application User
         2. Create UserManager User (ApplicationUser , Password)
         */
        public async Task<string> Register(RegistrationRequestDto registrationRequestDto)
        {
            ApplicationUser user = new()
            {
                UserName = registrationRequestDto.Email,
                Email = registrationRequestDto.Email,
                PhoneNumber = registrationRequestDto.PhoneNumber,
                FirstName = registrationRequestDto.FirstName,
                LastName = registrationRequestDto.LastName,
                NormalizedEmail = registrationRequestDto.Email.ToUpper()
            };
            var roleName = registrationRequestDto.Role;
            try
            {
                var result  = await _userManager.CreateAsync(user, registrationRequestDto.Password);
                if (result.Succeeded)
                {
                    var CreatedUser = _appDbContext.ApplicationUsers.First(u => u.UserName == registrationRequestDto.Email);
                    UserDto userDto = new()
                    {
                        Email = CreatedUser.Email,
                        FirstName = CreatedUser.FirstName,
                        LastName = CreatedUser.LastName,
                        ID = CreatedUser.Id,
                        PhoneNumber = CreatedUser.PhoneNumber

                    };
                    if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                    {
                        _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
                    }
                    await _userManager.AddToRoleAsync(user, roleName);
                    return "";

                }
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                }
            }
            catch(Exception ex)
            {

            }
            return "Error Occured";

        }
    }
}
