using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using smarthealth.Data;
using smarthealth.Models.Dtos;
using smarthealth.Repo;

namespace smarthealth.Controllers
{
    [Route("api/medicines")]
    [ApiController]
    public class MedicinesController : ControllerBase
    {
        private readonly IMedicineRepo _medicineRepo;
        private ResponseDto _response;
        public MedicinesController(IMedicineRepo medicineRepo)
        {
            _medicineRepo= medicineRepo;
            _response = new ResponseDto();
        }

        [HttpGet]
        [Route("GetMedicines")]
        public ResponseDto GetMedicines(int pageNumber,int pageSize= 25,Boolean sort = false)
        {
            try
            {
                List<MedicineDto> medicineDtos = _medicineRepo.GetMedicines(pageNumber,pageSize,sort);
                if (medicineDtos == null)
                {
                    throw new Exception("Medicinces Not Found");
                }
                else
                {
                    _response.IsSuccess = true;
                    _response.Result = medicineDtos;
                    _response.Message = "Found Medicine";
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;

            }
            return _response;
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public ResponseDto GetMedicineById(int id)
        {
            try
            {
                MedicineDto medicineDto = _medicineRepo.GetMedicineById(id);
                if(medicineDto== null)
                {
                    throw new Exception("Medicince Not Found");
                }
                else
                {
                    _response.IsSuccess = true;
                    _response.Result = medicineDto;
                    _response.Message = "Found Medicine";
                }
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
               
            }
            return _response;
        }

        [HttpGet]
        [Route("SearchMedicine")]
        public ResponseDto GetMedicineById(string query)
        {
            try
            {
                List<MedicineDto> medicineDtos = _medicineRepo.SearchMedicine(query);
                if (medicineDtos == null)
                {
                    throw new Exception("Medicince Not Found");
                }
                else
                {
                    _response.IsSuccess = true;
                    _response.Result = medicineDtos;
                    _response.Message = "Found Medicines";
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;

            }
            return _response;
        }

        [HttpPost]
        [Route("createmedicines")]
        public ResponseDto CreateMedicines([FromBody] List<MedicineDto> medicineDtos)
        {
            try
            {
                _medicineRepo.CreateMedicines(medicineDtos);
                _response.IsSuccess = true;
                _response.Message = "Medicines Added Successfully";
            }
            catch(Exception ex) {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            
            return _response;
        }

        [HttpPost]
        [Route("createmedicine")]
        public ResponseDto CreateMedicine([FromBody] MedicineDto medicineDto)
        {
            try
            {
                medicineDto= _medicineRepo.CreateMedicine(medicineDto);
                _response.IsSuccess = true;
                _response.Result = medicineDto;
                _response.Message = "Medicines Added Successfully";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpDelete]
        [Route("{id:int}")]
        public ResponseDto DeleteMedicine(int id)
        {
            try
            {
                MedicineDto medicineDto = _medicineRepo.DeleteMedicineById(id);
                if(medicineDto == null)
                {
                    throw new Exception("Medicine Not found");
                }
                _response.IsSuccess = true;
                _response.Message = "Medicine Delete Successfully";
                _response.Result = medicineDto;
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

    }
}
