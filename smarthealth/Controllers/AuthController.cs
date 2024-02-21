using smarthealth.Models.Dtos;
using smarthealth.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using smarthealth.Services;

namespace smarthealth.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ResponseDto _responseDto;

        public AuthController(IAuthService authService)
        {
            _authService= authService;
            _responseDto = new();
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto registrationRequestDto)
        {
            var errorMessage =await _authService.Register(registrationRequestDto);
            if (!errorMessage.IsNullOrEmpty())
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = errorMessage;
                return BadRequest(_responseDto);
            }

            return Ok(_responseDto);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            try {
                var loginResponse = await _authService.Login(loginRequestDto);
                if (loginResponse.UserDto == null)
                {
                    _responseDto.IsSuccess = false;
                    _responseDto.Message = "Invalid Credentials";
                    return BadRequest(_responseDto);
                }
                _responseDto.IsSuccess = true;
                _responseDto.Result = loginResponse;
                return Ok(_responseDto);
            }
            catch(Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message= "Invalid Credentials";
                return Ok(_responseDto);
            }
            
        }
        [HttpPost("assignrole")]
        public async Task<IActionResult> AssignRole([FromBody] RegistrationRequestDto registrationRequestDto)
        {
            var result = await _authService.AssignRole(registrationRequestDto.Email,registrationRequestDto.Role.ToUpper());
            if (!result)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "Error Encountered";
                return BadRequest(_responseDto);
            }
            _responseDto.IsSuccess = true;
            return Ok(_responseDto);
        }
    }
}
