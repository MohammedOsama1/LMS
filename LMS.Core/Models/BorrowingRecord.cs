using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Core.Models
{
    public class BorrowingRecord
    {
       [Key]
       public int Id { get; set; }

       [ForeignKey("BorrowerId")]
       public Borrower Borrower { get; set; }
       public int BorrowerId { get; set; }

        [ForeignKey("BookId")]
       public Book Book { get; set; }
       public int BookId { get; set; }
       public DateTime DateTime { get; } = DateTime.Now;

       
    }
}
