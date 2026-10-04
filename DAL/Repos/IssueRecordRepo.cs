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
    internal class IssueRecordRepo : IRepository<IssueRecord>
    {
        LMSContext db;
        public IssueRecordRepo(LMSContext db)
        {
            this.db = db;
        }
        public bool Create(IssueRecord b)
        {
            db.IssueRecords.Add(b);
            return db.SaveChanges() > 0;
        }
        public List<IssueRecord> GetAll()
        {
            return db.IssueRecords.ToList();
        }
        public IssueRecord GetById(int id)
        {
            return db.IssueRecords.Find(id);
        }
        public bool Update(IssueRecord b)
        {
            var exIssueRecord = db.IssueRecords.Find(b.IssueRecordId);
            if (exIssueRecord != null)
            {
                db.Entry(exIssueRecord).CurrentValues.SetValues(b);
                return db.SaveChanges() > 0;
            }
            return false;
        }
        public bool Delete(int id)
        {
            var exIssueRecord = db.IssueRecords.Find(id);
            if (exIssueRecord != null)
            {
                db.IssueRecords.Remove(exIssueRecord);
                return db.SaveChanges() > 0;
            }
            return false;
        }
    }
}
