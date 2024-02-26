
using static smarthealth.Utility.StaticDetail;

namespace smarthealth.Models.Dtos
{
    public class RequestDto
    {
        public ApiType ApiType { get; set; } = ApiType.GET;
        public String Url { get; set; }
        public object Data { get; set; }
        public String AccessToken { get; set; }
        public ContentType ContentType { get; set; } = ContentType.Json;
    }
}
