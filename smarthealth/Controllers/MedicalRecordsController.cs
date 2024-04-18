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

        public MedicalRecordsController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        // GET: api/Download/5
        [HttpGet("{id}")]
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

        [HttpPost]
        public async Task<IActionResult> PostMedicalRecord([FromForm] MedicalRecordDto model)
        {
            if (model.FileData == null || model.FileData.Length == 0)
            {
                return BadRequest("File is required.");
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
                    await model.FileData.CopyToAsync(memoryStream);
                    medicalRecord.FileData = memoryStream.ToArray();
                }

                _db.MedicalRecords.Add(medicalRecord);
                await _db.SaveChangesAsync();

                return Ok("Medical record uploaded successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while uploading the medical record: {ex.Message}");
            }
        }

    }
}
