using smarthealth.Models.Dtos;

namespace smarthealth.Repo
{
    public interface IMedicalRecordRepo
    {
        public MedicalRecordDto GetMedicalRecordById(int id);
        public MedicalRecordDto GetMedicalRecordByPatientId(int id);
        public List<MedicalRecordDto> SearchMedicine(string query);
        public MedicalRecordDto CreateMedicalRecord(MedicineDto medicineDto);
        public MedicalRecordDto DeleteMedicalRecordById(int id);
    }
}
