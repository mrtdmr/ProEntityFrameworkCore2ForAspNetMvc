using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdvancedApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdvancedApp.Controllers
{
    public class DeleteController : Controller
    {
        private readonly AdvancedContext _context;
        public DeleteController(AdvancedContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Employees.Where(e => e.SoftDeleted)
            .Include(e => e.OtherIdentity).IgnoreQueryFilters());
        }
        [HttpPost]
        public IActionResult Restore(Employee employee)
        {
            _context.Employees.IgnoreQueryFilters()
            .First(e => e.SSN == employee.SSN
            && e.FirstName == employee.FirstName
            && e.FamilyName == employee.FamilyName).SoftDeleted = false;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        //[HttpPost]
        //public IActionResult Delete(Employee e)
        //{
        //    if (e.OtherIdentity != null)
        //    {
        //        _context.Remove(e.OtherIdentity);
        //    }
        //    _context.Employees.Remove(e);
        //    _context.SaveChanges();
        //    return RedirectToAction(nameof(Index));
        //}
        //[HttpPost]
        //public IActionResult DeleteAll()
        //{
        //    IEnumerable<Employee> data = _context.Employees
        //    .IgnoreQueryFilters()
        //    .Include(e => e.OtherIdentity)
        //    .Where(e => e.SoftDeleted).ToArray();
        //    _context.RemoveRange(data.Select(e => e.OtherIdentity));
        //    _context.RemoveRange(data);
        //    _context.SaveChanges();
        //    return RedirectToAction(nameof(Index));
        //}

        [HttpPost]
        public IActionResult DeleteAll()
        {
            _context.Database.ExecuteSqlCommand("EXECUTE PurgeSoftDelete");
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult RestoreAll()
        {
            _context.Database.ExecuteSqlCommand("EXECUTE RestoreSoftDelete");
            return RedirectToAction(nameof(Index));
        }
    }
}