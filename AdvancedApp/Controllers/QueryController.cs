using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdvancedApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AdvancedApp.Controllers
{
    public class QueryController : Controller
    {
        private readonly AdvancedContext _context;
        public QueryController(AdvancedContext context)
        {
            _context = context;
        }
        public IActionResult ServerEval()
        {
            return View("Query", _context.Employees.Where(e => e.Salary > 150_000));
        }
        public IActionResult ClientEval()
        {
            return View("Query", _context.Employees.Where(e => IsHighEarner(e)));
        }
        private bool IsHighEarner(Employee e)
        {
            return e.Salary > 150_000;
        }
    }
}