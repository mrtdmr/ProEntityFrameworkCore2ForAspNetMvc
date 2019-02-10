using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DataApp.Models;
using DataApp.Repositories.DataRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DataApp.Controllers
{
    public class RelatedDataController : Controller
    {
        private readonly IRepository<Supplier> _supplierRepository;
        private readonly IRepository<ContactDetails> _detailRepository;
        private readonly IRepository<ContactLocation> _locationRepository;
        private readonly EFDatabaseContext _context;
        public RelatedDataController(
            IRepository<Supplier> supplierRepository,
            IRepository<ContactDetails> detailRepository,
            IRepository<ContactLocation> locationRepository,
            EFDatabaseContext context)
        {
            _supplierRepository = supplierRepository;
            _detailRepository = detailRepository;
            _locationRepository = locationRepository;
            _context = context;
        }
        public IActionResult Index(){
            return View(_supplierRepository.GetList().Include(s => s.Products));//Include metodunda ilişkili veriyi filtreleyemiyoruz. O yüzden fixing up ya da explicit query denen metodları uygulayabiliriz.
        }
        public IActionResult Contacts() => View(_detailRepository.GetList());
        public IActionResult Locations() => View(_locationRepository.GetList());
    }
}