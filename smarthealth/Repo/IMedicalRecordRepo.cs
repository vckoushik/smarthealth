using smarthealth.Models.Dtos;

namespace smarthealth.Repo
{
    public interface IMedicalRecordRepo
    {
        public MedicalRecordDto GetMedicalRecordById(int id);
        public Boolean SaveMedicalRecord(MedicalRecordDto model);
        public List<MedicalRecordDto> GetAllMedicalRecords();
        public MedicalRecordDto DeleteMedicalRecordById(int id);
        public List<MedicalRecordDto> GetMedicalRecordByUserId(string UserId);


    }
}
