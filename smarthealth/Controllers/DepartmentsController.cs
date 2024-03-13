using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using smarthealth.Models.Dtos;
using smarthealth.Repo;

namespace smarthealth.Controllers
{
    [Route("api/department")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentRepo _departmentsRepo;
        private ResponseDto _response;
        public DepartmentsController(IDepartmentRepo departmentsRepo)
        {
            _departmentsRepo =departmentsRepo;
            _response = new ResponseDto();
        }
        [HttpGet]
        [Route("GetDepartments")]
        public ResponseDto GetDepartments()
        {
            try
            {
                List<DepartmentDto> departmentDtos = _departmentsRepo.GetDepartments();
                if (departmentDtos == null)
                {
                    throw new Exception("Departments Not Found");
                }
                else
                {
                    _response.IsSuccess = true;
                    _response.Result = departmentDtos;
                    _response.Message = "Found Department";
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
        public ResponseDto GetDepartmentById(int id)
        {
            try
            {
                DepartmentDto departmentDto = _departmentsRepo.GetDepartmentById(id);
                if (departmentDto == null)
                {
                    throw new Exception("Department Not Found");
                }
                else
                {
                    _response.IsSuccess = true;
                    _response.Result = departmentDto;
                    _response.Message = "Found Department";
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
        [Route("SearchDepartment")]
        public ResponseDto SearchDepartment(string query)
        {
            try
            {
                List<DepartmentDto> departmentDtos = _departmentsRepo.SearchDepartment(query);
                if (departmentDtos == null || departmentDtos.Count == 0)
                {
                    throw new Exception("Department Not Found");
                }
                else
                {
                    _response.IsSuccess = true;
                    _response.Result = departmentDtos;
                    _response.Message = "Found Departments";
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
        [Route("createDepartment")]
        public ResponseDto CreateDepartment([FromBody] DepartmentDto departmentDto)
        {
            try
            {
                departmentDto = _departmentsRepo.CreateDepartment(departmentDto);
                _response.IsSuccess = true;
                _response.Result = departmentDto;
                _response.Message = "Departments Added Successfully";
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
        public ResponseDto DeleteDepartment(int id)
        {
            try
            {
                DepartmentDto departmentDto = _departmentsRepo.DeleteDepartmentById(id);
                if (departmentDto == null)
                {
                    throw new Exception("Department Not found");
                }
                _response.IsSuccess = true;
                _response.Message = "Department Delete Successfully";
                _response.Result = departmentDto;
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
