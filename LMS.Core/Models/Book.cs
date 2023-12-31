﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LMS.Core.Models
{
    public class Book
    {
        [Key]
        public int Id { get; private set; }
        [Required(ErrorMessage =" Title Cant Be Empty ")]
        public string Title { get; set; }

        [Required(ErrorMessage = " Author Cant Be Empty ")]
        public string Author { get; set; }

        [Required(ErrorMessage = " ISBN Cant Be Empty ")]
        public string ISBN { get; set; }


        [Required(ErrorMessage = " ShelfLocation Cant Be Empty ")]
        public int ShelfLocation { get; set; }


        [Required(ErrorMessage = " AvailableQuantity Cant Be Empty ")]
        public int AvailableQuantity { get; set; }

    }
}
