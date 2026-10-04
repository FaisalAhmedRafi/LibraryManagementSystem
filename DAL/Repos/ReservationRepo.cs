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
    internal class ReservationRepo : IRepository<Reservation>
    {
        LMSContext db;
        public ReservationRepo(LMSContext db)
        {
            this.db = db;
        }
        public bool Create(Reservation b)
        {
            db.Reservations.Add(b);
            return db.SaveChanges() > 0;
        }
        public List<Reservation> GetAll()
        {
            return db.Reservations.ToList();
        }
        public Reservation GetById(int id)
        {
            return db.Reservations.Find(id);
        }
        public bool Update(Reservation b)
        {
            var exReservation = db.Reservations.Find(b.ReservationId);
            if (exReservation != null)
            {
                db.Entry(exReservation).CurrentValues.SetValues(b);
                return db.SaveChanges() > 0;
            }
            return false;
        }
        public bool Delete(int id)
        {
            var exReservation = db.Reservations.Find(id);
            if (exReservation != null)
            {
                db.Reservations.Remove(exReservation);
                return db.SaveChanges() > 0;
            }
            return false;
        }
    }
}
