using AutoMapper;
using BLL.DTOs;
using DAL.EF.Models;
namespace BLL
{
    public class MapperConfig
    {
        static MapperConfiguration cfg  = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Book, BookDTO>().ReverseMap();
            cfg.CreateMap<Fine, FineDTO>().ReverseMap();
            cfg.CreateMap<IssueRecord, IssueRecordDTO>().ReverseMap();
            cfg.CreateMap<Member, MemberDTO>().ReverseMap();
            cfg.CreateMap<Reservation, ReservationDTO>().ReverseMap();
        });
        public static Mapper GetMapper()
        {
            return new Mapper(cfg);
        }
    }
}
