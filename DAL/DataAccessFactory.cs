using DAL.EF;
using DAL.EF.Models;
using DAL.Interfaces;
using DAL.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DataAccessFactory
    {
        LMSContext db;
        public DataAccessFactory(LMSContext db)
        {
            this.db = db;
        }
        public IRepository<Book> BookData()
        {
            return new BookRepo(db);
        }
        public IRepository<Fine> FineData()
        {
            return new FineRepo(db);
        }
        public IRepository<Member> MemberData()
        {
            return new MemberRepo(db);
        }
        public IRepository<IssueRecord> IssueRecordData()
        {
            return new IssueRecordRepo(db);
        }
        public IRepository<Reservation> ReservationData()
        {
            return new ReservationRepo(db);
        }
    }
}
