using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Core.Models
{
    public class Borrower
    {
        
        public int Id { get; private set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public int ReisteredData { get; set; }
    }
}
