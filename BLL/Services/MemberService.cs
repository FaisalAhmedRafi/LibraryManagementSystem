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
    public class MemberService
    {
        DataAccessFactory factory;
        public MemberService(DataAccessFactory factory)
        {
            this.factory = factory;
        }
        public List<MemberDTO> GetAll()
        {
            var data = factory.MemberData().GetAll();
            var mapper = MapperConfig.GetMapper();
            return mapper.Map<List<MemberDTO>>(data);
        }
        public MemberDTO Get(int id)
        {
            var data = factory.MemberData().GetById(id);
            var mapper = MapperConfig.GetMapper();
            return mapper.Map<MemberDTO>(data);
        }
        public bool Create(MemberDTO b)
        {
            var mapper = MapperConfig.GetMapper();
            var Member = mapper.Map<Member>(b);
            return factory.MemberData().Create(Member);
        }
        public bool Update(MemberDTO b)
        {
            var mapper = MapperConfig.GetMapper();
            var Member = mapper.Map<Member>(b);
            return factory.MemberData().Update(Member);
        }
        public bool Delete(int id)
        {
            return factory.MemberData().Delete(id);
        }
    }
}
