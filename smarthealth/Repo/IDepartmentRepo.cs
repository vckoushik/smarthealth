using smarthealth.Models;
using smarthealth.Models.Dtos;

namespace smarthealth.Repo
{
    public interface IDepartmentRepo
    {
        public List<DepartmentDto> GetDepartments();
        public DepartmentDto GetDepartmentById(int id);
        public List<DepartmentDto> SearchDepartment(string query);
        public DepartmentDto CreateDepartment(DepartmentDto department);
        public DepartmentDto DeleteDepartmentById(int id);
    }
}
