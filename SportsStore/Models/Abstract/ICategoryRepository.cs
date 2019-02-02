using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models.Abstract
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> Categories { get; }
        Task AddCategory(Category category);
        Task UpdateCategory(Category category);
        Task DeleteCategory(Category category);
        Task<Category> GetCategory(int id);
    }
}
