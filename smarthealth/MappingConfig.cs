using AutoMapper;
using smarthealth.Models;
using smarthealth.Models.Dtos;

namespace smarthealth
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<MedicineDto, Medicine>();
                config.CreateMap<Medicine, MedicineDto>();
                config.CreateMap<DoctorDto, Doctor>();
                config.CreateMap<Doctor, DoctorDto>();
                config.CreateMap<Department, DepartmentDto>();
                config.CreateMap<DepartmentDto, Department>();
                config.CreateMap<Appointment, AppointmentDto>();
                config.CreateMap<AppointmentDto, Appointment>();

            });
            return mappingConfig;
        }
    }
}
