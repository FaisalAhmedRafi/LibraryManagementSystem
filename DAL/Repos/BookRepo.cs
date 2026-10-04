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
    internal class BookRepo : IRepository<Book>
    {
        LMSContext db;
        public BookRepo(LMSContext db)
        {
            this.db = db;
        }
        public bool Create (Book b)
        {
            db.Books.Add(b);
            return db.SaveChanges()>0;
        }
        public List<Book> GetAll()
        {
            return db.Books.ToList();
        }
        public Book GetById(int id)
        {
            return db.Books.Find(id);
        }
        public bool Update(Book b)
        {
            var exBook = db.Books.Find(b.BookId);
            if (exBook != null)
            {
                db.Entry(exBook).CurrentValues.SetValues(b);
                return db.SaveChanges() > 0;
            }
            return false;
        }
        public bool Delete(int id)
        {
            var exBook = db.Books.Find(id);
            if (exBook != null)
            {
                db.Books.Remove(exBook);
                return db.SaveChanges() > 0;
            }
            return false;
        }

    }
}
