using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models.Abstract
{
    public interface IProductRepository
    {
        IEnumerable<Product> Products { get; }
        Task AddProduct(Product product);
        Task UpdateProduct(Product product);
        Task DeleteProduct(Product product);
        Task<Product> GetProduct(long id);
    }
}
