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
        private ResponseDto _response;

        public MedicalRecordsController(AppDbContext db, IMapper mapper, IMedicalRecordRepo medicalRecordRepo)
        {
            _db = db;
            _medicalRecordRepo = medicalRecordRepo;
            _response = new ResponseDto();
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
        public ActionResult<ResponseDto> GetAllMedicalRecords()
        {
            try
            {
                List<MedicalRecordDto> medicalRecordDtos = _medicalRecordRepo.GetAllMedicalRecords();

                if (medicalRecordDtos == null)
                {
                    return NotFound();
                }
                else
                {
                    _response.IsSuccess = true;
                    _response.Result = medicalRecordDtos;
                    _response.Message = "Found Medical Record";
                }

            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            

            return Ok(_response);
        }


        [HttpGet("GetRecord/{id}")]
        public  ActionResult<ResponseDto> MedicalRecordDetails(int id)
        {
            try
            {
                var medicalRecordDto = _medicalRecordRepo.GetMedicalRecordById(id);

                if (medicalRecordDto == null)
                {
                    return NotFound();
                }
                else
                {
                    _response.IsSuccess = true;
                    _response.Result = medicalRecordDto;
                    _response.Message = "Found Medical Record";

                }
            }
            catch(Exception ex) {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }


            return Ok(_response);
        }

        [HttpGet("GetRecordByUserId/{userid}")]
        public ActionResult<ResponseDto> GetRecordByUserId(string userid)
        {
            try
            {
                var medicalRecordDtos = _medicalRecordRepo.GetMedicalRecordByUserId(userid);

                if (medicalRecordDtos == null)
                {
                    return NotFound();
                }
                else
                {
                    _response.IsSuccess = true;
                    _response.Result = medicalRecordDtos;
                    _response.Message = "Found doctor";
                }
            }
            catch(Exception  ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return Ok(_response);
        }
        [HttpDelete("DeleteRecord/{id}")]
        public  ActionResult<ResponseDto> DeleteMedicalRecord(int id)
        {
            try
            {
                var medicalRecordDto = _medicalRecordRepo.DeleteMedicalRecordById(id);

                if (medicalRecordDto == null)
                {
                    return NotFound();
                }
                else
                {
                   _response.IsSuccess = true;
                    _response.Message = "Medical Record Delete Successfully";
                    _response.Result = medicalRecordDto;
                }
            }
            catch  (Exception ex)
            {
                _response.IsSuccess = false;
                   _response.Message = ex.Message;
            }
            
            return Ok(_response);
        }


        [HttpPost]
        public async Task<IActionResult> PostMedicalRecord([FromForm] MedicalRecordDto model)
        {
            if (model.FileData == null || model.FileData.Length == 0)
            {
                return BadRequest("File is required.");
            }

            try
            {
                bool result = await _medicalRecordRepo.SaveMedicalRecord(model);
                if (result)
                {
                    return Ok("Medical record uploaded successfully.");
                }
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
