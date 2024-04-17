using smarthealth.Models.Dtos;

namespace smarthealth.Repo
{
    public interface IDoctorsRepo
    {
       public List<DoctorDto> GetDoctors();
       public DoctorDto GetDoctorById(int id);
        public List<DoctorDto> SearchDoctor(string query);
        public int CreateDoctors(List<DoctorDto> doctorDtos);
        public DoctorDto CreateDoctor(DoctorDto doctorDto);
        public DoctorDto UpdateDoctor(int id,DoctorDto doctorDto);
        public DoctorDto DeleteDoctorById(int id);
    }
}
