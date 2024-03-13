namespace smarthealth.Models.Dtos
{
    public class DepartmentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<DoctorDto> Doctors { get; set; } = new List<DoctorDto>();
    }
}
