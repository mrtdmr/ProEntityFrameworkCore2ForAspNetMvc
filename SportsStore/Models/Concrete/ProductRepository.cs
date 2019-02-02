using SportsStore.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models.Concrete
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _dataContext;
        public ProductRepository(DataContext dataContext) => _dataContext = dataContext;
        public IEnumerable<Product> Products => _dataContext.Products.ToArray();//DbSet<T> yerine direk array döndürdük, Viewdaki Model.Count sorgusunu önlemek için.
        public async Task AddProduct(Product product)
        {
            try
            {
                _dataContext.Add(product);
                await _dataContext.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<Product> GetProduct(long id) => await _dataContext.Products.FindAsync(id);

        public async Task UpdateProduct(Product product)
        {
            try
            {
                Product p = await GetProduct(product.Id);
                p.Name = product.Name;
                p.Category = product.Category;
                p.PurchasePrice = product.PurchasePrice;
                p.RetailPrice = product.RetailPrice;
                //_dataContext.Products.Update(product);
                await _dataContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteProduct(Product product)
        {
            try
            {
                _dataContext.Products.Remove(product);
                await _dataContext.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
