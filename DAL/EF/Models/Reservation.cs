using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EF.Models
{
    public class Reservation
    {
        public int ReservationId { get; set; }

        public int BookId { get; set; }
        public Book Book { get; set; }

        [ForeignKey("Member")]
        public int MemberId { get; set; }
        public virtual Member Member { get; set; }

        public DateTime ReservationDate { get; set; }
        public string Status { get; set; }
    }
}
