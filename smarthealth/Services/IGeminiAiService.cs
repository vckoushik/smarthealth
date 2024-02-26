using smarthealth.Models.Dtos;

namespace smarthealth.Services
{
    public interface IGeminiAiService
    {
        Task<ResponseDto?> SearchAsync(SearchRequest searchRequest);
    }
}
