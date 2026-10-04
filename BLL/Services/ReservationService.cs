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
    public class ReservationService
    {
        DataAccessFactory factory;
        public ReservationService(DataAccessFactory factory)
        {
            this.factory = factory;
        }
        public List<ReservationDTO> GetAll()
        {
            var data = factory.ReservationData().GetAll();
            var mapper = MapperConfig.GetMapper();
            return mapper.Map<List<ReservationDTO>>(data);
        }
        public ReservationDTO Get(int id)
        {
            var data = factory.ReservationData().GetById(id);
            var mapper = MapperConfig.GetMapper();
            return mapper.Map<ReservationDTO>(data);
        }
        public bool Create(ReservationDTO b)
        {
            var mapper = MapperConfig.GetMapper();
            var Reservation = mapper.Map<Reservation>(b);
            return factory.ReservationData().Create(Reservation);
        }
        public bool Update(ReservationDTO b)
        {
            var mapper = MapperConfig.GetMapper();
            var Reservation = mapper.Map<Reservation>(b);
            return factory.ReservationData().Update(Reservation);
        }
        public bool Delete(int id)
        {
            return factory.ReservationData().Delete(id);
        }
        public bool ReserveBook(int bookId, int memberId)
        {
            var book = factory.BookData().GetById(bookId);

            if (book.AvailableCopies > 0)
                return false;

            var reservation = new Reservation
            {
                BookId = bookId,
                MemberId = memberId,
                ReservationDate = DateTime.Now,
                Status = "Pending"
            };

            return factory.ReservationData().Create(reservation);
        }

    }
}
