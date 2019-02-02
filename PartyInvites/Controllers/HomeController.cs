using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PartyInvites.Models;

namespace PartyInvites.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataContext _context;
        public HomeController(DataContext context) => _context = context;
        public IActionResult Index() => View();
        public IActionResult Respond() => View();
        [HttpPost]
        public async Task<IActionResult> Respond(GuestResponse guestResponse)
        {
            _context.GuestResponses.Add(guestResponse);
            await _context.SaveChangesAsync();
            return View(nameof(Thanks), guestResponse);
        }
        public IActionResult Thanks(GuestResponse guestResponse) =>
            View(guestResponse);
        public IActionResult ResponseList(string searchString = "5059067970") =>
            View(_context.GuestResponses
                .Where(r => r.WillAttend == true && r.Phone == searchString)
                .OrderBy(r => r.Email));
    }
}