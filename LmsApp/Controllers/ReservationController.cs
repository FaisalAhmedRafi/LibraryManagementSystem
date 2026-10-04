using BLL.DTOs;
using BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LmsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        ReservationService service;
        public ReservationController(ReservationService service)
        {
            this.service = service;
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var data = service.GetAll();
            return Ok(data);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var data = service.Get(id);
            return Ok(data);
        }
        [HttpPost("create")]
        public IActionResult Create(ReservationDTO b)
        {
            var res = service.Create(b);
            return Ok(res);
        }
        [HttpPut("update")]
        public IActionResult Update(ReservationDTO b)
        {
            var res = service.Update(b);
            return Ok(res);
        }
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var res = service.Delete(id);
            return Ok(res);
        }
        [HttpPost("reserve")]
        public IActionResult Reserve(int bookId, int memberId)
        {
            var res = service.ReserveBook(bookId, memberId);
            if (!res) return BadRequest("Book is available, no need to reserve");
            return Ok("Book reserved successfully");
        }

    }
}
