using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using smarthealth.Models.Dtos;
using smarthealth.Repo;

namespace smarthealth.Controllers
{
    [Route("api/doctor")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorsRepo _doctorsRepo;
        private ResponseDto _response;
        public DoctorsController(IDoctorsRepo doctorsRepo)
        {
            _doctorsRepo = doctorsRepo;
            _response = new ResponseDto();
        }


        [HttpGet]
        [Route("Getdoctors")]
        public ResponseDto Getdoctors()
        {
            try
            {
                List<DoctorDto> doctorDtos = _doctorsRepo.GetDoctors();
                if (doctorDtos == null)
                {
                    throw new Exception("Doctors Not Found");
                }
                else
                {
                    _response.IsSuccess = true;
                    _response.Result = doctorDtos;
                    _response.Message = "Found doctor";
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;

            }
            return _response;
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public ResponseDto GetdoctorById(int id)
        {
            try
            {
                DoctorDto doctorDto = _doctorsRepo.GetDoctorById(id);
                if (doctorDto == null)
                {
                    throw new Exception("Doctor Not Found");
                }
                else
                {
                    _response.IsSuccess = true;
                    _response.Result = doctorDto;
                    _response.Message = "Found doctor";
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;

            }
            return _response;
        }

        [HttpGet]
        [Route("Searchdoctor")]
        public ResponseDto SearchDoctor(string query)
        {
            try
            {
                List<DoctorDto> doctorDtos = _doctorsRepo.SearchDoctor(query);
                if (doctorDtos == null)
                {
                    throw new Exception("Doctor Not Found");
                }
                else
                {
                    _response.IsSuccess = true;
                    _response.Result = doctorDtos;
                    _response.Message = "Found doctors";
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;

            }
            return _response;
        }

        [HttpPost]
        [Route("createdoctors")]
        public ResponseDto Createdoctors([FromBody] List<DoctorDto> doctorDtos)
        {
            try
            {
                _doctorsRepo.CreateDoctors(doctorDtos);
                _response.IsSuccess = true;
                _response.Message = "doctors Added Successfully";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpPost]
        [Route("createdoctor")]
        public ResponseDto Createdoctor([FromBody] DoctorDto doctorDto)
        {
            try
            {
                doctorDto = _doctorsRepo.CreateDoctor(doctorDto);
                _response.IsSuccess = true;
                _response.Result = doctorDto;
                _response.Message = "Doctors Added Successfully";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpDelete]
        [Route("{id:int}")]
        public ResponseDto Deletedoctor(int id)
        {
            try
            {
                DoctorDto doctorDto = _doctorsRepo.DeleteDoctorById(id);
                if (doctorDto == null)
                {
                    throw new Exception("Doctor Not found");
                }
                _response.IsSuccess = true;
                _response.Message = "Doctor Delete Successfully";
                _response.Result = doctorDto;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


    }
}
