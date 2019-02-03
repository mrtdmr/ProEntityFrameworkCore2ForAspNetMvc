using SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.ViewModels
{
    public class CategoryViewModel
    {
        public Category TheCategory { get; set; }
        public IEnumerable<Category> TheCategories { get; set; }
    }
}
