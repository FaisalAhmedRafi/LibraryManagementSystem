using DAL.EF;
using DAL.EF.Models;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    internal class MemberRepo : IRepository<Member>
    {
        LMSContext db;
        public MemberRepo(LMSContext db)
        {
            this.db = db;
        }
        public bool Create(Member b)
        {
            db.Members.Add(b);
            return db.SaveChanges() > 0;
        }
        public List<Member> GetAll()
        {
            return db.Members.ToList();
        }
        public Member GetById(int id)
        {
            return db.Members.Find(id);
        }
        public bool Update(Member b)
        {
            var exMember = db.Members.Find(b.MemberId);
            if (exMember != null)
            {
                db.Entry(exMember).CurrentValues.SetValues(b);
                return db.SaveChanges() > 0;
            }
            return false;
        }
        public bool Delete(int id)
        {
            var exMember = db.Members.Find(id);
            if (exMember != null)
            {
                db.Members.Remove(exMember);
                return db.SaveChanges() > 0;
            }
            return false;
        }
    }
}
