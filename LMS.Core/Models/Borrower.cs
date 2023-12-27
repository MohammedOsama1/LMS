using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Core.Models
{
    public class Borrower
    {
        [Key]
        public int Id { get; private set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public DateTime ReisteredData { get;} = DateTime.Now;
    }
}
