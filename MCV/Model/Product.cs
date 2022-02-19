using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DOTNETEXAMproduct.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Plase Fill ProductName")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Plase Fill ProductName")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public decimal Rate { get; set; }

        [Required(ErrorMessage = "Plase Fill ProductName")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Plase Fill ProductName")]
        public string CategoryName { get; set; }
    }
}