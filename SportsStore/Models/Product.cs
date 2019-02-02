using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public class Product
    {
        [Required]
        public long Id { get; set; }
        [Required]
        [Display(Name="Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Category")]
        public string Category { get; set; }
        [Required]
        [Display(Name = "Purchase Price")]
        public decimal PurchasePrice { get; set; }
        [Required]
        [Display(Name = "Retail Price")]
        public decimal RetailPrice { get; set; }
    }
}
