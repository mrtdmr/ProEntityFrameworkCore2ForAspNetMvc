using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Models.Abstract;

namespace SportsStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductRepository _repository;
        public HomeController(IProductRepository repository) => _repository = repository;
        public IActionResult Index()
        {
            //Console.Clear();
            return View(_repository.Products);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                await _repository.AddProduct(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            return View(await _repository.GetProduct(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                await _repository.UpdateProduct(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(long id)
        {
            return View(await _repository.GetProduct(id));
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Product product)
        {
            if (product.Id != 0)
            {
                await _repository.DeleteProduct(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }
    }
}