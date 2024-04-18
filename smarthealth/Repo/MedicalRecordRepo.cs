using AutoMapper;
using smarthealth.Data;
using smarthealth.Models;
using smarthealth.Models.Dtos;

namespace smarthealth.Repo
{
    public class MedicalRecordRepo : IMedicalRecordRepo
    {
        private readonly AppDbContext _db;
        private IMapper _mapper;

        public MedicalRecordRepo(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public MedicalRecordDto DeleteMedicalRecordById(int id)
        {

            MedicalRecordDto medicalRecordDto = null;
            try
            {
                MedicalRecord medicalRecord = _db.MedicalRecords.First(m => m.Id == id);
                _db.MedicalRecords.Remove(medicalRecord);
                _db.SaveChanges();
                medicalRecordDto = _mapper.Map<MedicalRecordDto>(medicalRecord);

            }
            catch (Exception ex)
            {
                return medicalRecordDto;
            }
            return medicalRecordDto;
        }


        public List<MedicalRecordDto> GetAllMedicalRecords()
        {
            List<MedicalRecordDto> medicalRecordDtos = null;
            try
            {
                List<MedicalRecord> medicalRecords = _db.MedicalRecords.ToList();
                medicalRecordDtos = _mapper.Map<List<MedicalRecordDto>>(medicalRecords);

            }
            catch (Exception ex)
            {
                return medicalRecordDtos;
            }
            return medicalRecordDtos;
        }

        public MedicalRecordDto GetMedicalRecordById(int id)
        {
            MedicalRecordDto medicalRecordDto = null;
            try
            {
                MedicalRecord medicalRecord = _db.MedicalRecords.First(m => m.Id == id);
                medicalRecordDto = _mapper.Map<MedicalRecordDto>(medicalRecord);

            }
            catch (Exception ex)
            {
                return medicalRecordDto;
            }
            return medicalRecordDto;
        }
        public List<MedicalRecordDto> GetMedicalRecordByUserId(string UserId)
        {
            List<MedicalRecordDto> medicalRecordDtos = null;
            try
            {
                List<MedicalRecord> medicalRecords = _db.MedicalRecords.Where(m => m.UserId == UserId).ToList();
                medicalRecordDtos = _mapper.Map<List<MedicalRecordDto>>(medicalRecords);

            }
            catch (Exception ex)
            {
                return medicalRecordDtos;
            }
            return medicalRecordDtos;
        }

        public  Boolean SaveMedicalRecord(MedicalRecordDto model)
        {
            if (model.FileData == null || model.FileData.Length == 0)
            {
                return false;
            }

            try
            {
                var medicalRecord = new MedicalRecord
                {
                    Name = model.Name,
                    Description = model.Description,
                    CreatedDate = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc),
                    FileName = model.FileData.FileName,
                    UserId = model.UserId
                };

                using (var memoryStream = new MemoryStream())
                {
                     model.FileData.CopyToAsync(memoryStream);
                    medicalRecord.FileData = memoryStream.ToArray();
                }

                _db.MedicalRecords.Add(medicalRecord);
                 _db.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
