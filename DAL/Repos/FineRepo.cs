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
    internal class FineRepo : IRepository<Fine>
    {
        LMSContext db;
        public FineRepo(LMSContext db)
        {
            this.db = db;
        }
        public bool Create (Fine b)
        {
            db.Fines.Add(b);
            return db.SaveChanges()>0;
        }
        public List<Fine> GetAll()
        {
            return db.Fines.ToList();
        }
        public Fine GetById(int id)
        {
            return db.Fines.Find(id);
        }
        public bool Update(Fine b)
        {
            var exFine = db.Fines.Find(b.FineId);
            if (exFine != null)
            {
                db.Entry(exFine).CurrentValues.SetValues(b);
                return db.SaveChanges() > 0;
            }
            return false;
        }
        public bool Delete(int id)
        {
            var exFine = db.Fines.Find(id);
            if (exFine != null)
            {
                db.Fines.Remove(exFine);
                return db.SaveChanges() > 0;
            }
            return false;
        }
    }
}
