using DAL.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class FineDTO
    {
        public int FineId { get; set; }

        public int IssueRecordId { get; set; }
        public IssueRecord IssueRecord { get; set; }

        public int DaysLate { get; set; }
        public decimal Amount { get; set; }

        public bool IsPaid { get; set; }
    }
}
