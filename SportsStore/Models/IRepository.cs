using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public interface IRepository
    {
        IEnumerable<Product> Products { get; }
        Task AddProduct(Product product);
        Task<Product> GetProduct(long id);
        Task UpdateProduct(Product product);
        Task DeleteProduct(Product product);
    }
}
