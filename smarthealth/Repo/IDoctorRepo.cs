using smarthealth.Models.Dtos;

namespace smarthealth.Repo
{
    public interface IDoctorRepo
    {
        public List<DoctorDto> GetDoctors(int pageNumber, int pageSize, Boolean sort);
        public DoctorDto GetDoctorById(int id);
        public List<DoctorDto> SearchDoctor(string query);
        public int CreateDoctors(List<DoctorDto> doctorDtos);
        public DoctorDto CreateDoctor(DoctorDto doctorDto);

        public DoctorDto DeleteDoctorById(int id);
    }
}
