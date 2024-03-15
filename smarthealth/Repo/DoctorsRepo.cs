using AutoMapper;
using smarthealth.Data;
using smarthealth.Models.Dtos;
using smarthealth.Models;
using Microsoft.EntityFrameworkCore;

namespace smarthealth.Repo
{
    public class DoctorsRepo:IDoctorsRepo
    {
        private readonly AppDbContext _db;
        private IMapper _mapper;

        public DoctorsRepo(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public List<DoctorDto> GetDoctors()
        {
            List<DoctorDto> doctorDtos = new List<DoctorDto>();
            try
            {
                List<Doctor> doctors = _db.Doctors.Include(d => d.Department).ToList();

                foreach(Doctor doctor in doctors){
                    DoctorDto doctorDto = new DoctorDto();
                    doctorDto = _mapper.Map<DoctorDto>(doctor);
                    doctorDto.DepartmentDto = new DepartmentDto();
                    doctorDto.DepartmentDto.Id = doctor.Department.Id;
                    doctorDto.DepartmentDto.Name = doctor.Department.Name;
                    doctorDtos.Add(doctorDto);
                }

            }
            catch (Exception ex)
            {
                return doctorDtos;
            }
            return doctorDtos;
        }

        public DoctorDto GetDoctorById(int id)
        {
            DoctorDto doctorDto = new DoctorDto();
            try
            {
                Doctor doctor = _db.Doctors.Include(d=>d.Department).First(d => d.Id == id);
                doctorDto = _mapper.Map<DoctorDto>(doctor);
                doctorDto.DepartmentDto = new DepartmentDto();
                doctorDto.DepartmentDto.Id = doctor.Department.Id;
                doctorDto.DepartmentDto.Name = doctor.Department.Name;

            }
            catch (Exception ex)
            {
                return doctorDto;
            }
            return doctorDto;
        }
        public List<DoctorDto> SearchDoctor(string query)
        {
            List<DoctorDto> doctorDtos = new List<DoctorDto>();
            try
            {
                List<Doctor> doctors = _db.Doctors.Include(d=>d.Department).Where(doc=>doc.Name.ToLower().Contains(query.ToLower()) || doc.Department.Name.ToLower().Contains(query.ToLower())).ToList();

                foreach (Doctor doctor in doctors)
                {
                    DoctorDto doctorDto = new DoctorDto();
                    doctorDto = _mapper.Map<DoctorDto>(doctor);
                    doctorDto.DepartmentDto = new DepartmentDto();
                    doctorDto.DepartmentDto.Id = doctor.Department.Id;
                    doctorDto.DepartmentDto.Name = doctor.Department.Name;
                    doctorDtos.Add(doctorDto);
                }


            }
            catch (Exception ex)
            {
                return doctorDtos;
            }
            return doctorDtos;
        }
        public int CreateDoctors(List<DoctorDto> doctorDtos)
        {
            try
            {
                foreach (DoctorDto doctorDto in doctorDtos)
                {
                    Doctor doctor = _mapper.Map<Doctor>(doctorDto);
                    _db.Doctors.Add(doctor);
                    _db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return -1;
            }
            return 1;
        }

        public DoctorDto CreateDoctor(DoctorDto doctorDto)
        {

            try
            {
                Doctor doctor = _mapper.Map<Doctor>(doctorDto);

                _db.Doctors.Add(doctor);
                _db.SaveChanges();
                doctorDto = _mapper.Map<DoctorDto>(doctor);

            }
            catch (Exception ex)
            {
                return doctorDto;
            }
            return doctorDto;
        }
        public DoctorDto DeleteDoctorById(int id)
        {
            DoctorDto doctorDto = null;
            try
            {
                Doctor doctor = _db.Doctors.First(d => d.Id == id);
                _db.Doctors.Remove(doctor);
                _db.SaveChanges();
                doctorDto = _mapper.Map<DoctorDto>(doctor);

            }
            catch (Exception ex)
            {
                return doctorDto;
            }
            return doctorDto;
        }
    }
}
