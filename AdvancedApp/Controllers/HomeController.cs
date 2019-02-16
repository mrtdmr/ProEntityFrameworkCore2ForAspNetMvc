using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AdvancedApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdvancedApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly AdvancedContext _context;
        private static Func<AdvancedContext, string, IEnumerable<Employee>> query
            = EF.CompileQuery((AdvancedContext context, string searchTerm)
                 => context.Employees
             .Where(e => EF.Functions.Like(e.FirstName, searchTerm)));
        public HomeController(AdvancedContext context)
        {
            _context = context;
        }
        //public IActionResult Index()
        //{
        //    return View(_context.Employees.AsNoTracking());
        //}

        //public IActionResult Index(string searchTerm)
        //{
        //    IQueryable<Employee> data = _context.Employees;
        //    if (!string.IsNullOrEmpty(searchTerm))
        //    {
        //        data = data.Where(e => EF.Functions.Like(e.FirstName, searchTerm));
        //    }
        //    return View(data);
        //}


        //public async Task<IActionResult> Index(string searchTerm)
        //{
        //    IQueryable<Employee> employees = _context.Employees;
        //    if (!string.IsNullOrEmpty(searchTerm))
        //    {
        //        employees = employees.Where(e => EF.Functions.Like(e.FirstName, searchTerm));
        //    }
        //    HttpClient client = new HttpClient();
        //    ViewBag.PageSize = (await client.GetAsync("http://apress.com")).Content.Headers.ContentLength;
        //    return View(await employees.ToListAsync());
        //}

        //public IActionResult Index(string searchTerm)
        //{
        //    return View(string.IsNullOrEmpty(searchTerm) ? _context.Employees : query(_context, searchTerm));
        //}

        //public IActionResult Index(decimal salary = 0)
        public IActionResult Index(string searchTerm)
        {
            //ViewBag.Secondaries = _context.Set<SecondaryIdentity>();
            //return View(_context.Employees.Include(e => e.OtherIdentity).OrderByDescending(e => EF.Property<DateTime>(e, "LastUpdated")));
            //IEnumerable<Employee> data = _context.Employees.Include(e => e.OtherIdentity).OrderByDescending(e => e.LastUpdated).ToArray();
            //ViewBag.Secondaries = data.Select(e => e.OtherIdentity);

            //Raw Sql ve EF birleşimi
            //IEnumerable<Employee> data = _context.Employees
            //    .FromSql($@"
            //    select 
            //        * 
            //    from 
            //        Employees 
            //    where 
            //        SoftDeleted = 0 and
            //        Salary>{salary}")
            //        .Include(e => e.OtherIdentity)
            //        .OrderByDescending(e => e.Salary)
            //        .OrderByDescending(e => e.LastUpdated).ToArray();
            //ViewBag.Secondaries = data.Select(e => e.OtherIdentity);


            //Stored procedure
            //IEnumerable<Employee> data = _context.Employees // Running stored procedure
            //    .FromSql($@"
            //    execute GetBySalary @SalaryFilter = {salary}")
            //    .IgnoreQueryFilters();


            //View
            //IEnumerable<Employee> data = _context.Employees
            //.FromSql($@"SELECT * from NotDeletedView
            //WHERE Salary > {salary}")
            //.Include(e => e.OtherIdentity)
            //.OrderByDescending(e => e.Salary)
            //.OrderByDescending(e => e.LastUpdated)
            //.IgnoreQueryFilters()
            //.ToArray();
            //ViewBag.Secondaries = data.Select(e => e.OtherIdentity);


            //Function
            //IEnumerable<Employee> data = _context.Employees
            //.FromSql($@"SELECT * from GetSalaryTable({salary})")
            //.Include(e => e.OtherIdentity)
            //.OrderByDescending(e => e.LastUpdated)
            //.IgnoreQueryFilters()
            //.ToArray();
            //ViewBag.Secondaries = data.Select(e => e.OtherIdentity);



            //IEnumerable<Employee> data = _context.Employees
            //.Include(e => e.OtherIdentity)
            //.OrderByDescending(e => e.LastUpdated)
            //.IgnoreQueryFilters()
            //.ToArray();
            //ViewBag.Secondaries = data.Select(e => e.OtherIdentity);

            IQueryable<Employee> query = _context.Employees.Include(e => e.OtherIdentity);
            if (!string.IsNullOrEmpty(searchTerm))
            {
                //query = query.Where(e => EF.Functions.Like($"{e.FirstName[0]}{e.FamilyName}", searchTerm));
                query = query.Where(e => EF.Functions.Like(e.GeneratedValue, searchTerm));
            }
            IEnumerable<Employee> data = query.ToArray();
            ViewBag.Secondaries = data.Select(e => e.OtherIdentity);
            return View(data);
        }

        //public IActionResult Edit(long id)
        //public IActionResult Edit(string SSN)
        //public IActionResult Edit(string SSN, string firstName, string familyName)
        //{
        //    //return View(id == default(long) ? new Employee() : _context.Employees.Include(e => e.OtherIdentity).First(e => e.Id == id));
        //    //return View(string.IsNullOrWhiteSpace(SSN) ? new Employee() : _context.Employees.Include(e => e.OtherIdentity).First(e => e.SSN == SSN));
        //    return View(string.IsNullOrWhiteSpace(SSN) ? new Employee() :
        //        _context
        //        .Employees.Include(e => e.OtherIdentity)
        //        .AsNoTracking()
        //        .First(e => e.SSN == SSN && e.FirstName == firstName && e.FamilyName == familyName));
        //}

        public IActionResult Edit(string SSN, string firstName, string familyName)
        {
            return View(string.IsNullOrWhiteSpace(SSN) ? new Employee() : _context.Employees.Include(e => e.OtherIdentity)
.First(e => e.SSN == SSN
&& e.FirstName == firstName
&& e.FamilyName == familyName));
        }
        //[HttpPost]
        //public IActionResult Update(Employee employee)
        //{
        //    //if (employee.Id == default(long))
        //    //if (_context.Employees.Count(e => e.SSN == employee.SSN) == 0)
        //    //if (_context.Employees.Count(e => e.SSN == employee.SSN && e.FirstName == employee.FirstName && e.FamilyName == employee.FamilyName) == 0)
        //    //if (_context.Employees.Find(employee.SSN, employee.FirstName, employee.FamilyName) == null) // Burada find metodundan dönen Employee ile MVC binder tarafından gelen employee nesneleri tracking moddadır. Bu yüzden update kısmında aynı isimde ve id de iki tracking nesensi olacağı için hata verecektir.
        //    //{
        //    //    _context.Add(employee);
        //    //}
        //    //else
        //    //{
        //    //    _context.Update(employee);
        //    //}

        //    //Employee existing = _context.Employees.Find(employee.SSN, employee.FirstName, employee.FamilyName);
        //    Employee existing = _context.Employees.AsTracking().First(e => e.SSN == employee.SSN && e.FirstName == employee.FirstName && e.FamilyName == employee.FamilyName);
        //    if (existing == null)
        //    {
        //        _context.Add(employee);
        //    }
        //    else
        //    {
        //        //_context.Entry(existing).State = EntityState.Detached;
        //        //_context.Update(employee);
        //        existing.Salary = employee.Salary;
        //    }
        //    _context.SaveChanges();
        //    return RedirectToAction(nameof(Index));
        //}


        //        [HttpPost]
        //        public IActionResult Update(Employee employee, decimal salary)
        //        {
        //            Employee existing = _context.Employees.Find(employee.SSN,
        //employee.FirstName, employee.FamilyName);
        //            if (existing == null)
        //            {
        //                //_context.Entry(employee).Property("LastUpdated").CurrentValue = DateTime.Now;
        //                _context.Add(employee);
        //            }
        //            else
        //            {
        //                _context.Entry(existing).Property("LastUpdated").CurrentValue = DateTime.Now;
        //                existing.Salary = salary;
        //            }
        //            _context.SaveChanges();
        //            return RedirectToAction(nameof(Index));
        //        }
        [HttpPost]
        public IActionResult Update(Employee employee)
        {
            if (_context.Employees.Count(e => e.SSN == employee.SSN
                   && e.FirstName == employee.FirstName
                   && e.FamilyName == employee.FamilyName) == 0)
            {
                _context.Add(employee);
            }
            else
            {
                Employee e = new Employee
                {
                    SSN = employee.SSN,
                    FirstName = employee.FirstName,
                    FamilyName = employee.FamilyName,
                    RowVersion = employee.RowVersion
                };
                _context.Employees.Attach(e);
                e.Salary = employee.Salary;
                e.LastUpdated = DateTime.Now;
            }
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult Delete(Employee employee)
        {
            //_context.Attach(employee);
            //employee.SoftDeleted = true;
            //            _context.Set<SecondaryIdentity>().FirstOrDefault(id =>
            //id.PrimarySSN == employee.SSN
            //&& id.PrimaryFirstName == employee.FirstName
            //&& id.PrimaryFamilyName == employee.FamilyName);

            //if (employee.OtherIdentity != null)
            //{
            //    _context.Set<SecondaryIdentity>().Remove(employee.OtherIdentity);
            //}
            //_context.Employees.Remove(employee);
            _context.Employees.Attach(employee);
            employee.SoftDeleted = true;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}