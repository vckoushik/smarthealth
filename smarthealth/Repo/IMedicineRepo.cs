using smarthealth.Models.Dtos;

namespace smarthealth.Repo
{
    public interface IMedicineRepo
    {
        public List<MedicineDto> GetMedicines(int pageNumber,int pageSize,Boolean sort);
        public MedicineDto GetMedicineById(int id);
        public List<MedicineDto> SearchMedicine(string query);
        public int CreateMedicines(List<MedicineDto> medicineDtos);
        public MedicineDto CreateMedicine(MedicineDto medicineDto);
        public MedicineDto UpdateMedicine(int id, MedicineDto updatedMedicineDto);
        public MedicineDto DeleteMedicineById(int id);

    }
}
