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
    public class SuppliersController : Controller
    {
        private readonly IRepository<Supplier> _supplierRepository;
        private readonly IRepository<Product> _productRepository;

        public SuppliersController(IRepository<Supplier> supplierRepository, IRepository<Product> productRepository)
        {
            _supplierRepository = supplierRepository;
            _productRepository = productRepository;
        }

        public IActionResult Index()
        {
            ViewBag.SupplierEditId = TempData["SupplierEditId"];
            ViewBag.SupplierCreateId = TempData["SupplierCreateId"];
            ViewBag.SupplierRelationshipId = TempData["SupplierRelationshipId"];
            return View(_supplierRepository.GetList().Include("Products"));
        }
        public IActionResult Edit(long id)
        {
            TempData["SupplierEditId"] = id;
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult Update(Supplier supplier)
        {
            _supplierRepository.Update(supplier, null);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Create(long id)
        {
            TempData["SupplierCreateId"] = id;
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Change(long id)
        {
            TempData["SupplierRelationshipId"] = id;
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        //public IActionResult Change(Supplier supplier)//1. ve 2. yol için
        public IActionResult Change(long Id, Product[] products)
        {
            //1. Yol

            //IEnumerable<Product> changed = supplier.Products.Where(p => p.SupplierId != supplier.Id);
            //if (changed.Count() > 0)
            //{
            //    IEnumerable<Supplier> allSuppliers = _supplierRepository.GetList().Include("Products").ToArray();
            //    Supplier currentSupplier = allSuppliers.First(s => s.Id == supplier.Id);
            //    foreach (Product p in changed)
            //    {
            //        Supplier newSupplier = allSuppliers.First(s => s.Id == p.SupplierId);
            //        newSupplier.Products = newSupplier.Products
            //        .Append(currentSupplier.Products
            //        .First(op => op.Id == p.Id)).ToArray();
            //        _supplierRepository.Update(newSupplier, null);
            //    }
            //}


            //2. Yol: Entity framework MVC ile bind edilen nesneleri db den güncellemez. Yani izlemediği(track) etmediği nesneleri güncellemez. Bunun için AsNoTracking kullanılabilir :
            //            IEnumerable<Product> changed
            //= supplier.Products.Where(p => p.SupplierId != supplier.Id);
            //            IEnumerable<long> targetSupplierIds
            //            = changed.Select(p => p.SupplierId).Distinct();
            //            if (changed.Count() > 0)
            //            {
            //                IEnumerable<Supplier> targetSuppliers = _supplierRepository.GetList().Include("Products")
            //                .Where(s => targetSupplierIds.Contains(s.Id))
            //                .AsNoTracking().ToArray();
            //                foreach (Product p in changed)
            //                {
            //                    Supplier newSupplier
            //                    = targetSuppliers.First(s => s.Id == p.SupplierId);
            //                    newSupplier.Products = newSupplier.Products == null
            //                    ? new Product[] { p }
            //                    : newSupplier.Products.Append(p).ToArray();
            //                }
            //                _supplierRepository.UpdateRange(targetSuppliers);
            //            }

            //3. yol
            _productRepository.UpdateRange(products.Where(p => p.SupplierId != Id));
            return RedirectToAction(nameof(Index));
        }
    }
}