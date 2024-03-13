using AutoMapper;
using smarthealth.Data;
using smarthealth.Models.Dtos;
using smarthealth.Models;
using Microsoft.EntityFrameworkCore;

namespace smarthealth.Repo
{
    public class DepartmentRepo: IDepartmentRepo
    {
        private readonly AppDbContext _db;
        private IMapper _mapper;

        public DepartmentRepo(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public List<DepartmentDto> GetDepartments()
        {
            List<DepartmentDto> departmentDtos = null;
            try
            {
                
                List<Department> departments = _db.Departments.Include(d => d.Doctors).ToList();
                departmentDtos = _mapper.Map<List<DepartmentDto>>(departments);

            }
            catch (Exception ex)
            {
                return departmentDtos;
            }
            return departmentDtos;
        }


        public DepartmentDto GetDepartmentById(int id)
        {
            DepartmentDto departmentDto = null;
            try
            {
                Department department = _db.Departments.First(d => d.Id == id);
                departmentDto = _mapper.Map<DepartmentDto>(department);

            }
            catch (Exception ex)
            {
                return departmentDto;
            }
            return departmentDto;
        }
        public List<DepartmentDto> SearchDepartment(string query)
        {
            List<DepartmentDto> departmentDtos = null;
            query= query.ToLower();
            try
            {
                List<Department> departments = (from m in _db.Departments
                                        where m.Name.ToLower().Contains(query)
                                        select m).ToList();
                departmentDtos = _mapper.Map<List<DepartmentDto>>(departments);

            }
            catch (Exception ex)
            {
                return departmentDtos;
            }
            return departmentDtos;
        }

        public DepartmentDto CreateDepartment(DepartmentDto departmentDto)
        {

            try
            {
                Department department = _mapper.Map<Department>(departmentDto);
              
                _db.Departments.Add(department);
                _db.SaveChanges();
                departmentDto = _mapper.Map<DepartmentDto>(department);

            }
            catch (Exception ex)
            {
                return departmentDto;
            }
            return departmentDto;
        }
        public DepartmentDto DeleteDepartmentById(int id)
        {
            DepartmentDto departmentDto = null;
            try
            {
                Department department = _db.Departments.First(d => d.Id == id);
                _db.Departments.Remove(department);
                _db.SaveChanges();
                departmentDto = _mapper.Map<DepartmentDto>(department);

            }
            catch (Exception ex)
            {
                return departmentDto;
            }
            return departmentDto;
        }
    }
}

