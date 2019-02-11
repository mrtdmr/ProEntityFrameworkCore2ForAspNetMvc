using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExistingDb.Models.Scaffold;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExistingDb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ScaffoldContext _context;
        public HomeController(ScaffoldContext context) => _context = context;
        public IActionResult Index()
        {
            return View(_context.Shoes
                .Include(s => s.Color)
                .Include(s => s.SalesCampaigns)
                .Include(s => s.ShoeCategoryJunction)
                    .ThenInclude(j => j.Category)
                .Include(s => s.Fitting));
        }
    }
}