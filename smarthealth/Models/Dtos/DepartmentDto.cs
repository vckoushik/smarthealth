﻿namespace smarthealth.Models.Dtos
{
    public class DepartmentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImgUrl { get; set; }
        public List<DoctorDto> Doctors { get; set; } = new List<DoctorDto>();
    }
}
