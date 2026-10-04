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
    public class IssueRecordService
    {
        DataAccessFactory factory;
        public IssueRecordService(DataAccessFactory factory)
        {
            this.factory = factory;
        }
        public List<IssueRecordDTO> GetAll()
        {
            var data = factory.IssueRecordData().GetAll();
            var mapper = MapperConfig.GetMapper();
            return mapper.Map<List<IssueRecordDTO>>(data);
        }
        public IssueRecordDTO Get(int id)
        {
            var data = factory.IssueRecordData().GetById(id);
            var mapper = MapperConfig.GetMapper();
            return mapper.Map<IssueRecordDTO>(data);
        }
        public bool Create(IssueRecordDTO b)
        {
            var mapper = MapperConfig.GetMapper();
            var IssueRecord = mapper.Map<IssueRecord>(b);
            return factory.IssueRecordData().Create(IssueRecord);
        }
        public bool Update(IssueRecordDTO b)
        {
            var mapper = MapperConfig.GetMapper();
            var IssueRecord = mapper.Map<IssueRecord>(b);
            return factory.IssueRecordData().Update(IssueRecord);
        }
        public bool Delete(int id)
        {
            return factory.IssueRecordData().Delete(id);
        }
        public bool IssueBook(int bookId, int memberId)
        {
            var book = factory.BookData().GetById(bookId);
            if (book == null || book.AvailableCopies <= 0)
                return false;

            book.AvailableCopies--;
            factory.BookData().Update(book);

            var issue = new IssueRecord
            {
                BookId = bookId,
                MemberId = memberId,
                IssueDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(14),
                Status = "Issued"
            };

            return factory.IssueRecordData().Create(issue);
        }
        public bool ReturnBook(int issueId)
        {
            var issue = factory.IssueRecordData().GetById(issueId);
            if (issue == null || issue.Status == "Returned")
                return false;

            issue.ReturnDate = DateTime.Now;

            int lateDays = (issue.ReturnDate.Value - issue.DueDate).Days;

            if (lateDays > 0)
            {
                var fine = new Fine
                {
                    IssueRecordId = issue.IssueRecordId,
                    DaysLate = lateDays,
                    Amount = lateDays * 10,
                    IsPaid = false
                };

                factory.FineData().Create(fine);
                issue.Status = "Overdue";
            }
            else
            {
                issue.Status = "Returned";
            }

            factory.IssueRecordData().Update(issue);

            var book = factory.BookData().GetById(issue.BookId);
            book.AvailableCopies++;
            factory.BookData().Update(book);

            return true;
        }
        public List<object> MostBorrowedBooks()
        {
            var issues = factory.IssueRecordData().GetAll();

            var report = issues
                .GroupBy(i => i.BookId)
                .Select(g => new
                {
                    BookId = g.Key,
                    TimesBorrowed = g.Count()
                })
                .OrderByDescending(x => x.TimesBorrowed)
                .ToList<object>();

            return report;
        }


    }
}
