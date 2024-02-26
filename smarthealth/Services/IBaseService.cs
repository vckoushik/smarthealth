using smarthealth.Models.Dtos;

namespace smarthealth.Services
{
    public interface IBaseService
    {
        Task<GeminiResponseDto?> SendAsync(RequestDto requestDTO, bool withBearer = true);
      
    }
}
