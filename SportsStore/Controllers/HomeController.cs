using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SportsStore.Models;
using SportsStore.Models.Pages;
using SportsStore.Repositories.Abstract;

namespace SportsStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Category> _categoryRepository;
        public HomeController(IRepository<Product> pRepository, IRepository<Category> cRepository)
        {
            _productRepository = pRepository;
            _categoryRepository = cRepository;
        }
        public IActionResult Index(QueryOption option)
        {
            //Console.Clear();
            var pro = _productRepository.GetAll(option, "Category");
            return View(pro);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = _categoryRepository.GetAll().ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                await _productRepository.Add(product);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = _categoryRepository.GetAll().ToList();
            return View(product);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            ViewBag.Categories = _categoryRepository.GetAll();
            return View(await _productRepository.GetById(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                await _productRepository.Update(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(long id)
        {
            return View(await _productRepository.GetById(p => p.Id == id, "Category"));
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Product product)
        {
            if (product.Id != 0)
            {
                await _productRepository.Delete(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }
    }
}