
using Newtonsoft.Json;
using smarthealth.Models.Dtos;
using System.Net;
using System.Text;
using static smarthealth.Utility.StaticDetail;

namespace smarthealth.Services
{
    public class BaseService: IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        
        public BaseService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        /*
         1. Create HttpClient
         2. Create HttpRequestMessage (to send request)
         3. Add Headers , Token, Content, API Type to HttpRequestMessage
         4. httpClient.SendAsync() to send Http Request
         5. Capture the Response
         */
        public async Task<GeminiResponseDto?> SendAsync(RequestDto requestDTO,bool withBearer=true)
        {
            try
            {
            HttpClient httpClient = _httpClientFactory.CreateClient("GeminiAPI");
            HttpRequestMessage message = new();
                message.Headers.Add("Accept", "application/json");
                message.RequestUri = new Uri(requestDTO.Url);
            //Serialize the data
            if (requestDTO != null)
            {
                message.Content = new StringContent(JsonConvert.SerializeObject(requestDTO.Data), Encoding.UTF8, "application/json");
            }

            HttpResponseMessage? apiResponse = null;

            switch (requestDTO.ApiType)
            {
                case ApiType.POST:
                    message.Method = HttpMethod.Post;
                    break;
                case ApiType.PUT:
                    message.Method = HttpMethod.Put;
                    break;
                case ApiType.DELETE:
                    message.Method = HttpMethod.Delete;
                    break;
                default:
                    message.Method = HttpMethod.Get;
                    break;
            }

            apiResponse = await httpClient.SendAsync(message);
           
                switch (apiResponse.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        return new() { IsSuccess = false, Message = "Not Found" };
                    case HttpStatusCode.Forbidden:
                        return new() { IsSuccess = false, Message = "Forbidden" };
                    case HttpStatusCode.Unauthorized:
                        return new() { IsSuccess = false, Message = "UnAuthorized" };
                    case HttpStatusCode.InternalServerError:
                        return new() { IsSuccess = false, Message = "Internal Server Error" };
                    default:
                        var apiContent = await apiResponse.Content.ReadAsStringAsync();
                        var apiResponseDto = JsonConvert.DeserializeObject<GeminiResponseDto>(apiContent);
                        return apiResponseDto;
                }
            }
            catch (Exception e)
            {
                var dto = new GeminiResponseDto()
                {
                    Message = e.Message,
                    IsSuccess = false
                };
                return dto;
            }
        }
    }
}
