using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Models.Pages;
using SportsStore.Repositories.Abstract;

namespace SportsStore.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IRepository<Category> _repository;
        public CategoryController(IRepository<Category> repository) => _repository = repository;
        public IActionResult Index(QueryOption option) => View(_repository.GetAll(option));
        [HttpGet]
        public IActionResult Create() => View();
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                await _repository.Add(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id) => View(await _repository.GetById(id));
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                await _repository.Update(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id) => View(await _repository.GetById(id));
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Category category)
        {
            await _repository.Delete(category);
            return RedirectToAction(nameof(Index));
        }

        #region SinglePageCrud
        /*
        public async Task<IActionResult> Index(int? id)
        {
            CategoryViewModel categoryViewModel = new CategoryViewModel
            {
                TheCategories = _repository.GetAll,
                TheCategory = id != null ? await _repository.GetById(id) : new Category()
            };
            return View("Category", categoryViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveOrUpdate(CategoryViewModel category)
        {
            if (ModelState.IsValid)
            {
                if (category.TheCategory.Id != 0)
                {
                    await _repository.Update(category.TheCategory);
                }
                else
                {
                    await _repository.Add(category.TheCategory);
                }
                return RedirectToAction(nameof(Index), new { id = category.TheCategory.Id });
            }
            category.TheCategories = _repository.GetAll;
            return View("Category", category);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            CategoryViewModel categoryViewModel = new CategoryViewModel
            {
                TheCategories = _repository.GetAll,
                TheCategory = await _repository.GetById(id)
            };
            return View("Category", categoryViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var category =await _repository.GetById(id);
            await _repository.Delete(category);
            return RedirectToAction(nameof(Index));
        }
        */
        #endregion
    }
}