using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Models.Abstract;

namespace SportsStore.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IRepository<Category> _repository;
        public CategoryController(IRepository<Category> repository) => _repository = repository;
        public IActionResult Index() => View(_repository.GetAll);
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
    }
}