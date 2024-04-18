using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using smarthealth.Data;
using smarthealth.Models;
using smarthealth.Models.Dtos;
using smarthealth.Repo;

namespace smarthealth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalRecordsController : ControllerBase
    {
        private readonly AppDbContext _db;
        private IMapper _mapper;
        IMedicalRecordRepo _medicalRecordRepo;
        public MedicalRecordsController(AppDbContext db, IMapper mapper, IMedicalRecordRepo medicalRecordRepo)
        {
            _db = db;
            _medicalRecordRepo = medicalRecordRepo;
        }
        // GET: api/Download/5
        [HttpGet("Download/{id}")]
        public async Task<ActionResult<MedicalRecord>> DownloadFile(int id)
        {
            var medicalRecord = await _db.MedicalRecords.FindAsync(id);

            if (medicalRecord == null)
            {
                return NotFound();
            }

            var stream = new MemoryStream(medicalRecord.FileData);

            return File(stream, "application/octet-stream", medicalRecord.FileName);
        }

        [HttpGet("GetRecords")]
        public ActionResult<List<MedicalRecordDto>> GetAllMedicalRecords()
        {
            List<MedicalRecordDto> medicalRecordDtos = _medicalRecordRepo.GetAllMedicalRecords();

            if (medicalRecordDtos == null)
            {
                return NotFound();
            }

            return Ok(medicalRecordDtos);
        }


        [HttpGet("GetRecord/{id}")]
        public  ActionResult<MedicalRecordDto> MedicalRecordDetails(int id)
        {
            var medicalRecordDto = _medicalRecordRepo.GetMedicalRecordById(id);

            if (medicalRecordDto == null)
            {
                return NotFound();
            }


            return Ok(medicalRecordDto);
        }

        [HttpGet("GetRecordByUserId/{userid}")]
        public ActionResult<List<MedicalRecordDto>> GetRecordByUserId(string userid)
        {
            var medicalRecordDtos = _medicalRecordRepo.GetMedicalRecordByUserId(userid);

            if (medicalRecordDtos == null)
            {
                return NotFound();
            }


            return Ok(medicalRecordDtos);
        }
        [HttpDelete("DeleteRecord/{id}")]
        public  ActionResult<MedicalRecordDto> DeleteMedicalRecord(int id)
        {
            var medicalRecordDto = _medicalRecordRepo.DeleteMedicalRecordById(id);

            if (medicalRecordDto == null)
            {
                return NotFound();
            }
            return Ok(medicalRecordDto);
        }


        [HttpPost]
        public  IActionResult PostMedicalRecord([FromForm] MedicalRecordDto model)
        {
            if (model.FileData == null || model.FileData.Length == 0)
            {
                return BadRequest("File is required.");
            }

            try
            {
                Boolean result = _medicalRecordRepo.SaveMedicalRecord(model);
                if(result)
                    return Ok("Medical record uploaded successfully.");
                else
                {
                   return StatusCode(500, $"An error occurred while uploading the medical record");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while uploading the medical record: {ex.Message}");
            }
        }

    }
}
