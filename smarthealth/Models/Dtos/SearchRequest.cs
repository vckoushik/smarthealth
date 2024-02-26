namespace smarthealth.Models.Dtos
{
    public class SearchRequest
    {
        public string query { get; set; }
        public int age { get; set; }
        public string gender { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public Boolean isSmoker { get; set; }
        public Boolean isAlocholic { get; set; }
        public string serverity { get; set; }   
        public decimal height { get; set; }
        public decimal weight { get; set; }

    }
}
