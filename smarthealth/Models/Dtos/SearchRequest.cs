namespace smarthealth.Models.Dtos
{
    public class SearchRequest
    {
        public string query { get; set; }
        public string age { get; set; }
        public string gender { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string isSmoker { get; set; }
        public string isAlcholic { get; set; }
        public string severity { get; set; }   
        public string height { get; set; }
        public string weight { get; set; }

    }
}
