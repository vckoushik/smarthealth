using AutoMapper;
using smarthealth.Data;
using smarthealth.Models;
using smarthealth.Models.Dtos;
using System.Numerics;

namespace smarthealth.Repo
{
    public class DoctorRepo : IDoctorRepo
    {
        private readonly AppDbContext _db;
        private IMapper _mapper;

        public DoctorRepo(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
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

        public List<DoctorDto> GetDoctors(int pageNumber, int pageSize, bool sort)
        {
            List<DoctorDto> doctorDtos = null;
            try
            {
                int itemsToSkip = (pageNumber - 1) * pageSize;

                List<Doctor> doctors = _db.Doctors.Skip(itemsToSkip).Take(pageSize).ToList();
                doctorDtos = _mapper.Map<List<DoctorDto>>(doctors);

            }
            catch (Exception ex)
            {
                return doctorDtos;
            }
            return doctorDtos;
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
    }
}
