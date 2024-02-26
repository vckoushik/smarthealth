using AutoMapper;
using Azure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using smarthealth.Data;
using smarthealth.Models;
using smarthealth.Models.Dtos;
using System.Collections.Generic;
using static Azure.Core.HttpHeader;

namespace smarthealth.Repo
{
    public class MedicineRepo : IMedicineRepo
    {
        private readonly AppDbContext _db;
        private IMapper _mapper;

        public MedicineRepo(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public List<MedicineDto> GetMedicines(int pageNumber, int pageSize,Boolean sort)
        {
            List<MedicineDto> medicineDtos = null;
            try
            {
                int itemsToSkip = (pageNumber - 1) * pageSize;

                List<Medicine> medicines = _db.Medicines.Skip(itemsToSkip).Take(pageSize).ToList();
                medicineDtos = _mapper.Map<List<MedicineDto>>(medicines);

            } 
            catch(Exception ex)
            {
                return medicineDtos;
            }
            return medicineDtos;
        }
        public MedicineDto GetMedicineById(int id)
        {
            MedicineDto medicineDto = null;
            try
            {
                Medicine medicine = _db.Medicines.First(m=>m.Id == id);
                medicineDto = _mapper.Map<MedicineDto>(medicine);

            }
            catch (Exception ex)
            {
                return medicineDto;
            }
            return medicineDto;
        }
        public List<MedicineDto> SearchMedicine(string query)
        {
            List<MedicineDto> medicineDtos = null;
            try
            {
                List<Medicine> medicines = (from m in _db.Medicines
                                    where m.Name.Contains(query)
                                    select m).ToList();
                medicineDtos = _mapper.Map<List<MedicineDto>>(medicines);

            }
            catch (Exception ex)
            {
                return medicineDtos;
            }
            return medicineDtos;
        }
        public MedicineDto CreateMedicine(MedicineDto medicineDto)
        {
            
            try
            {
                Medicine medicine = _mapper.Map<Medicine>(medicineDto);
                
                _db.Medicines.Add(medicine);
                _db.SaveChanges();
                medicineDto = _mapper.Map<MedicineDto>(medicine);

            }
            catch (Exception ex)
            {
                return medicineDto;
            }
            return medicineDto;
        }

        public int CreateMedicines(List<MedicineDto> medicineDtos) 
        {
            try
            {
                foreach(MedicineDto medicineDto in medicineDtos)
                {
                    Medicine medicine= _mapper.Map<Medicine>(medicineDto);
                    _db.Medicines.Add(medicine);
                    _db.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                return -1;
            }
            return 1;
        }

        public MedicineDto DeleteMedicineById(int id)
        {
            MedicineDto medicineDto = null;
            try
            {
                Medicine medicine = _db.Medicines.First(m => m.Id == id);
                _db.Medicines.Remove(medicine);
                _db.SaveChanges();
                medicineDto = _mapper.Map<MedicineDto>(medicine);

            }
            catch (Exception ex)
            {
                return medicineDto;
            }
            return medicineDto;
        }

    }
}
