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
                
            });
            return mappingConfig;
        }
    }
}
