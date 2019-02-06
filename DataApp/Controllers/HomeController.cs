using System.Linq;
using DataApp.Models;
using DataApp.Repositories.DataRepository;
using Microsoft.AspNetCore.Mvc;

namespace DataApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository<Product> _productRepository;
        public HomeController(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }
        public IActionResult Index(string category = null, decimal? price = null)
        {
            //var products = _productRepository.GetAll().Where(p => p.Price > 25).ToArray();//IEnumarable<T> önce tüm datayı çeker, daha sonra MVC tarafında filtreler. Performans olarak zararlı. IQueryable<T> ise sorguyu DB düzeyinde çeker. IQueryable<T> interface sinin olabilecek dezavantajı ise farkında olmadan birden fazla sorgu çekilmesi riskidir. Örneğin alttaki products.Count() bilgisi için EF tarafından DB ye fazladan 1 sorgu daha çekilir. Bunu önlemek için de dönen veriyi veri setine dönüştürmeye zorlanır. Bunu da ToArray() veya ToList() metodlarıyla yapabiliriz.
            //ViewBag.ProductCount = products.Count();
            var products = _productRepository.GetAll();
            if (category != null)
            {
                products = products.Where(p => p.Category == category);
            }
            if (price != null)
            {
                products = products.Where(p => p.Price >= price);
            }
            ViewBag.category = category;
            ViewBag.price = price;
            return View(products);
        }
        public IActionResult Create()
        {
            ViewBag.CreateMode = true;
            return View("Editor", new Product(0));
        }
        [HttpPost]
        public IActionResult Create(Product product)
        {
            _productRepository.Create(product);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Edit(long id)
        {
            ViewBag.CreateMode = false;
            return View("Editor", _productRepository.Get(id));
        }
        [HttpPost]
        public IActionResult Edit(Product product,Product original)
        {
            _productRepository.Update(product,original);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult Delete(long id)
        {
            _productRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}