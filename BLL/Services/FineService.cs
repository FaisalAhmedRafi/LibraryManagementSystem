using BLL.DTOs;
using DAL;
using DAL.EF.Models;
using DAL.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class FineService
    {
        DataAccessFactory factory;
        public FineService(DataAccessFactory factory)
        {
            this.factory = factory;
        }
        public List<FineDTO> GetAll()
        {
            var data = factory.FineData().GetAll();
            var mapper = MapperConfig.GetMapper();
            return mapper.Map<List<FineDTO>>(data);
        }
        public FineDTO Get(int id)
        {
            var data = factory.FineData().GetById(id);
            var mapper = MapperConfig.GetMapper();
            return mapper.Map<FineDTO>(data);
        }
        public bool Create(FineDTO b)
        {
            var mapper = MapperConfig.GetMapper();
            var Fine = mapper.Map<Fine>(b);
            return factory.FineData().Create(Fine);
        }
        public bool Update(FineDTO b)
        {
            var mapper = MapperConfig.GetMapper();
            var Fine = mapper.Map<Fine>(b);
            return factory.FineData().Update(Fine);
        }
        public bool Delete(int id)
        {
            return factory.FineData().Delete(id);
        }
        public bool PayFine(int fineId)
        {
            var fine = factory.FineData().GetById(fineId);
            if (fine == null || fine.IsPaid)
                return false;

            fine.IsPaid = true;
            return factory.FineData().Update(fine);
        }
        public List<FineDTO> GetUnpaidFinesByMember(int memberId)
        {
            var fines = factory.FineData().GetAll()
                .Where(f => f.IssueRecord.MemberId == memberId && !f.IsPaid)
                .ToList();

            var mapper = MapperConfig.GetMapper();
            return mapper.Map<List<FineDTO>>(fines);
        }


    }
}
