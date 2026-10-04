using BLL.DTOs;
using BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LmsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FineController : ControllerBase
    {
        FineService service;
        public FineController(FineService service)
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
        public IActionResult Create(FineDTO b)
        {
            var res = service.Create(b);
            return Ok(res);
        }
        [HttpPut("update")]
        public IActionResult Update(FineDTO b)
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
        [HttpPost("pay/{id}")]
        public IActionResult Pay(int id)
        {
            var res = service.PayFine(id);
            if (!res) return BadRequest("Fine already paid or not found");
            return Ok("Fine paid successfully");
        }
        [HttpGet("unpaid/member/{memberId}")]
        public IActionResult UnpaidFines(int memberId)
        {
            return Ok(service.GetUnpaidFinesByMember(memberId));
        }


    }
}
