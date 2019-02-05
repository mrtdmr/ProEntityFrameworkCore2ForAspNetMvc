using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public class Category
    {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "*")]
        [Display(Name = "Category Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "*")]
        [Display(Name = "Description")]
        public string Description { get; set; }
        public virtual IEnumerable<Product> Products { get; set; }
    }
}
