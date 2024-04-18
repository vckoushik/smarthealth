namespace smarthealth.Models.Dtos
{
    public class MedicalRecordDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public IFormFile FileData { get; set; }
        public string FileName { get; set; }
        public string UserId { get; set; }
    }
}
