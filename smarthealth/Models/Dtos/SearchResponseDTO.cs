namespace smarthealth.Models.Dtos
{
    public class SearchResponseDTO
    {
        public string Disease { get; set; }
        public List<string> Treatments { get; set; }
        public List<string> HomeRemedies { get; set; }
        public string DoctorDepartment { get; set; }
        public List<string> Medications { get; set; }
        public List<string> PreventionMeasures { get; set; }
    }
}
