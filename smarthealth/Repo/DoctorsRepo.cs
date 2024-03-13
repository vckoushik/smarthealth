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
            List<DoctorDto> doctorDtos = null;
            try
            {
                List<Doctor> doctors = _db.Doctors.Include(d => d.Department).ToList();
                
                doctorDtos = _mapper.Map<List<DoctorDto>>(doctors);

            }
            catch (Exception ex)
            {
                return doctorDtos;
            }
            return doctorDtos;
        }

        public DoctorDto GetDoctorById(int id)
        {
            DoctorDto doctorDto = null;
            try
            {
                Doctor doctor = _db.Doctors.First(d => d.Id == id);
                doctorDto = _mapper.Map<DoctorDto>(doctor);

            }
            catch (Exception ex)
            {
                return doctorDto;
            }
            return doctorDto;
        }
        public List<DoctorDto> SearchDoctor(string query)
        {
            List<DoctorDto> doctorDtos = null;
            try
            {
                List<Doctor> doctors = (from m in _db.Doctors
                                        where m.Name.Contains(query)
                                        select m).ToList();
                doctorDtos = _mapper.Map<List<DoctorDto>>(doctors);

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
