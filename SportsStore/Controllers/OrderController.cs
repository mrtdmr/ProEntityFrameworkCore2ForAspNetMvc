using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Repositories.Abstract;

namespace SportsStore.Controllers
{
    public class OrderController : Controller
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<Product> _productRepository;
        public OrderController(IRepository<Order> orderRepository, IRepository<Product> productRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }
        public IActionResult Index() => View(_orderRepository.GetAll("Lines", "Lines.Product"));
        public async Task<IActionResult> EditOrder(long id)
        {
            var products = _productRepository.GetAll("Category").ToList();
            Order order = id == 0 ? new Order() : await _orderRepository.GetById(o => o.Id == id, "Lines");
            IDictionary<long, OrderLine> linesMap = order.Lines?.ToDictionary(l => l.ProductId) ?? new Dictionary<long, OrderLine>();
            ViewBag.Lines = products.Select(p => linesMap.ContainsKey(p.Id) ? linesMap[p.Id] : new OrderLine
            {
                Product = p,
                ProductId = p.Id,
                Quantity = 0
            });
            return View(order);
        }
        [HttpPost]
        public async Task<IActionResult> AddOrUpdateOrder(Order order)
        {
            order.Lines = order.Lines.Where(l => l.Id > 0 || (l.Id == 0 && l.Quantity > 0)).ToArray();
            if (order.Id == 0)
            {
                await _orderRepository.Add(order);
            }
            else
            {
                await _orderRepository.Update(order);
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> DeleteOrder(Order order)
        {
            await _orderRepository.Delete(order);
            return RedirectToAction(nameof(Index));
        }
    }
}