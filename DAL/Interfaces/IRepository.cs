using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IRepository<T> where T: class
    {
        List<T> GetAll();
        T GetById(int id);
        bool Create(T obj);
        bool Update(T obj);
        bool Delete(int id);
    }
}
