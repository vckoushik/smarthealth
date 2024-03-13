using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using smarthealth.Models.Dtos;
using smarthealth.Repo;
using smarthealth.Services;
using smarthealth.Utility;
using System.Text;

namespace smarthealth.Controllers
{
    [Route("api/search")]
    [ApiController]
    public class SearchController : ControllerBase
    {

        private IGeminiAiService _geminiAiService;
        private ResponseDto _response;
        public SearchController(IGeminiAiService geminiAiService)
        {
            _geminiAiService = geminiAiService;
            _response = new ResponseDto();
        }

        [HttpPost]
        public async Task<ResponseDto> Search([FromBody] SearchRequest searchRequest)
        {
            _response = await _geminiAiService.SearchAsync(searchRequest);
            return _response;
        }

    }
}
