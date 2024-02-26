using Newtonsoft.Json;
using smarthealth.Models.Dtos;
using smarthealth.Utility;
using System;
using System.Text;

namespace smarthealth.Services
{
    public class GeminiAiService:IGeminiAiService
    {
        private readonly IBaseService _baseService;

        public GeminiAiService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public async Task<ResponseDto?> SearchAsync(SearchRequest searchRequest)
        {
            GeminiRequestDto geminiRequestDto = new GeminiRequestDto();
            
            Part p = new Part();
            p.text = StaticDetail.Query;
            var searchReq = new StringContent(JsonConvert.SerializeObject(searchRequest), Encoding.UTF8, "application/json");
            p.text += await searchReq.ReadAsStringAsync();
            Content content = new Content();
            content.parts.Add(p);
            geminiRequestDto.contents.Add(content);
            RequestDto requestDTO = new RequestDto()
            {
                ApiType = StaticDetail.ApiType.POST,
                Data = geminiRequestDto,
                Url = StaticDetail.GeminiAPIBase
            };
            GeminiResponseDto geminiResponseDto =  await _baseService.SendAsync(requestDTO);
            ResponseDto responseDto = new ResponseDto();
            responseDto.IsSuccess = true;
            responseDto.Message = "Results";
            try
            {
                SearchResponseDTO searchResponseDTO = JsonConvert.DeserializeObject<SearchResponseDTO>(geminiResponseDto.candidates[0].content.parts[0].text);
                
              
                responseDto.Result = searchResponseDTO;

            }
            catch(Exception ex)
            {
                responseDto.IsSuccess = false;
                responseDto.Message = "Search with new query";
            }
           
      
            return responseDto;
        }
    }
}
