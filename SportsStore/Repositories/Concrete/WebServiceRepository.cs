using Microsoft.EntityFrameworkCore;
using SportsStore.Models;
using SportsStore.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Repositories.Concrete
{
    public class WebServiceRepository : IWebServiceRepository
    {
        private readonly DataContext _dataContext;
        public WebServiceRepository(DataContext context)
        {
            _dataContext = context;
        }
        public object GetProduct(long id)
        {
            //return _dataContext.Products.FirstOrDefault(p => p.Id == id);
            //return _dataContext.Products
            //    .Select(p => new { p.Id, p.Name, p.Description, p.PurchasePrice, p.RetailPrice })
            //    .FirstOrDefault(p => p.Id == id);
            //return _dataContext.Products.Include("Category").FirstOrDefault(p => p.Id == id); //There is an endless loop
            return _dataContext.Products
                .Include(p => p.Category)
                .Select(p => new
                {
                    p.Id,
                    p.Name,
                    p.Description,
                    p.RetailPrice,
                    p.PurchasePrice,
                    p.CategoryId,
                    p.Category
                })
                .FirstOrDefault(p => p.Id == id);
        }

        public object GetProducts(int skip, int take)
        {
            return _dataContext.Products
                .Include(p => p.Category)
                .OrderBy(p => p.Id)
                .Skip(skip)
                .Take(take)
                .Select(p => new
                {
                    p.Id,
                    p.Name,
                    p.Description,
                    p.RetailPrice,
                    p.PurchasePrice,
                    p.CategoryId,
                    p.Category
                });
        }

        public long StoreProduct(Product product)
        {
            _dataContext.Products.Add(product);
            _dataContext.SaveChanges();
            return product.Id;
        }
        public void UpdateProduct(Product product)
        {
            _dataContext.Products.Update(product);
            _dataContext.SaveChanges();
        }
        public void DeleteProduct(long id)
        {
            _dataContext.Products.Remove(new Product { Id = id });
            _dataContext.SaveChanges();
        }
    }
}
