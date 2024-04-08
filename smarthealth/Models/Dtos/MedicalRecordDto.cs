namespace smarthealth.Models.Dtos
{
    public class MedicalRecordDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public string URL { get; set; }
        public int UserId { get; set; }
    }
}
