namespace smarthealth.Models.Dtos
{
    public class DoctorDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DepartmentId { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        
        public DepartmentDto? DepartmentDto { get; set; }
    }
}
