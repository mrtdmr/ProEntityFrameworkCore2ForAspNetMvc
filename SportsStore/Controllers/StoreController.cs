using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Models.Pages;
using SportsStore.Repositories.Abstract;

namespace SportsStore.Controllers
{
    public class StoreController : Controller
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Category> _categoryRepository;
        public StoreController(IRepository<Product> productRepository, IRepository<Category> categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }
        public IActionResult Index([FromQuery(Name = "options")] QueryOption productOption, QueryOption categoryOption, int category)
        {
            ViewBag.Categories = _categoryRepository.GetAll(categoryOption);
            ViewBag.SelectedCategory = category;
            return View(_productRepository.GetAll(p => p.CategoryId == category, productOption, "Category"));
        }
    }
}