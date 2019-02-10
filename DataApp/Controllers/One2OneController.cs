using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataApp.Models;
using DataApp.Repositories.DataRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DataApp.Controllers
{
    public class One2OneController : Controller
    {
        private IRepository<ContactDetails> _cdRepository;
        private IRepository<Supplier> _supplierRepository;
        public One2OneController(IRepository<ContactDetails> cdRepository, IRepository<Supplier> supplierRepository)
        {
            _cdRepository = cdRepository;
            _supplierRepository = supplierRepository;
        }
        public IActionResult Index()
        {
            return View(_cdRepository.GetList().Include(cd => cd.Supplier));
        }
        public IActionResult Create() => View("ContactEditor");
        public IActionResult Edit(long id)
        {
            ViewBag.Suppliers = _supplierRepository.GetList().Include(s => s.Contact);
            return View("ContactEditor", _cdRepository.GetList().Include(cd => cd.Supplier).Where(cd => cd.Id == id).FirstOrDefault());
        }
        //[HttpPost]
        //public IActionResult Update(ContactDetails contactDetails)
        //{
        //    if (contactDetails.Id == 0)
        //        _cdRepository.Create(contactDetails);
        //    else
        //        _cdRepository.Update(contactDetails, null);
        //    return RedirectToAction(nameof(Index));
        //}
        [HttpPost]
        public IActionResult Update(ContactDetails contactDetails, long? targetSupplierId, long[] spares)
        {
            if (contactDetails.Id == 0)
                _cdRepository.Create(contactDetails);
            else
            {
                if (targetSupplierId.HasValue)
                {
                    if (spares.Contains(targetSupplierId.Value))
                    {
                        //contactDetails.SupplierId = targetSupplierId.Value;
                        contactDetails.Supplier = _supplierRepository.GetList().Where(s => s.Id == targetSupplierId).FirstOrDefault();
                    }
                }
                _cdRepository.Update(contactDetails, null);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}