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
    public class BookService
    {
        DataAccessFactory factory;
        public BookService(DataAccessFactory factory)
        {
            this.factory = factory;
        }
        public List<BookDTO> GetAll()
        {
            var data = factory.BookData().GetAll();
            var mapper = MapperConfig.GetMapper();
            return mapper.Map<List<BookDTO>>(data);
        }
        public BookDTO Get(int id)
        {
            var data = factory.BookData().GetById(id);
            var mapper = MapperConfig.GetMapper();
            return mapper.Map<BookDTO>(data);
        }
        public bool Create(BookDTO b)
        {
            var mapper = MapperConfig.GetMapper();
            var book = mapper.Map<Book>(b);
            return factory.BookData().Create(book);
        }
        public bool Update(BookDTO b)
        {
            var mapper = MapperConfig.GetMapper();
            var book = mapper.Map<Book>(b);
            return factory.BookData().Update(book);
        }
        public bool Delete(int id)
        {
            return factory.BookData().Delete(id);
        }
    }
}
