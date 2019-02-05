using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Repositories.Abstract;

namespace SportsStore.Controllers.API
{
    [Route("api/products")]
    public class ProductAPIController : Controller
    {
        private readonly IWebServiceRepository _repository;
        public ProductAPIController(IWebServiceRepository repository)
        {
            _repository = repository;
        }
        [HttpGet("{id}")]
        public object GetProduct(long id)
        {
            return _repository.GetProduct(id) ?? NotFound();
        }
        [HttpGet]
        public object GetProducts(int skip, int take)
        {
            return _repository.GetProducts(skip, take) ?? NotFound();
        }
        [HttpPost]
        public long StoreProduct([FromBody] Product product)
        {
            return _repository.StoreProduct(product);
        }
        [HttpPut]
        public void UpdateProduct([FromBody] Product product)
        {
            _repository.UpdateProduct(product);
        }
        [HttpDelete("{id}")]
        public void DeleteProduct(long id)
        {
            _repository.DeleteProduct(id);
        }
    }
}