namespace smarthealth.Models.Dtos
{
    public class GeminiResponseDto
    {
        public List<Candidate> candidates { get; set; } = new List<Candidate>();
        public object promptFeedback { get; set; }
        public Boolean IsSuccess;
        public string Message;
    }
}
