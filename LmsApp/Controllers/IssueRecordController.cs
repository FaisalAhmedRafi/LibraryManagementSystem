using BLL.DTOs;
using BLL.Services;
using DAL.EF.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LmsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssueRecordController : ControllerBase
    {
        IssueRecordService service;
        public IssueRecordController(IssueRecordService service)
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
        public IActionResult Create(IssueRecordDTO b)
        {
            var res = service.Create(b);
            return Ok(res);
        }
        [HttpPut("update")]
        public IActionResult Update(IssueRecordDTO b)
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
        [HttpPost("issue")]
        public IActionResult IssueBook(int bookId, int memberId)
        {
            var res = service.IssueBook(bookId, memberId);
            if (!res) return BadRequest("Book not available");
            return Ok("Book issued successfully");
        }
        [HttpPost("return/{issueId}")]
        public IActionResult ReturnBook(int issueId)
        {
            var res = service.ReturnBook(issueId);
            if (!res) return BadRequest("Invalid return");
            return Ok("Book returned successfully");
        }
        [HttpGet("report/most-borrowed")]
        public IActionResult MostBorrowed()
        {
            return Ok(service.MostBorrowedBooks());
        }


    }
}
