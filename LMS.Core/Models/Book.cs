using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Core.Models
{
    public class Book
    {
        [Required(ErrorMessage =" Title Cant Be Empty ")]
        public string Title { get; set; }

        [Required(ErrorMessage = " Author Cant Be Empty ")]
        public string Author { get; set; }

        [Key]
        [Required(ErrorMessage = " ISBN Cant Be Empty ")]
        public string ISBN { get; set; }


        [Required(ErrorMessage = " ShelfLocation Cant Be Empty ")]
        public int ShelfLocation { get; set; }


        [Required(ErrorMessage = " AvailableQuantity Cant Be Empty ")]
        public int AvailableQuantity { get; set; }

    }
}
