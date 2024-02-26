namespace smarthealth.Models.Dtos
{
    public class Candidate
    {
        public List<Content> contents { get; set; }  = new List<Content>(); 
        public Content content { get; set; }
    }
}
