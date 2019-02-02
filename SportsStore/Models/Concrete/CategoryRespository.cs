using SportsStore.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models.Concrete
{
    public class CategoryRespository : ICategoryRepository
    {
        private readonly ICategoryRepository _repository;
        public CategoryRespository(ICategoryRepository repository) => _repository = repository;
        public IEnumerable<Category> Categories => _repository.Categories.ToArray();

        public async Task AddCategory(Category category)
        {
            try
            {
                _repository.Categories.Append(category);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task DeleteCategory(Category category)
        {
            throw new NotImplementedException();
        }

        public Task<Category> GetCategory(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCategory(Category category)
        {
            throw new NotImplementedException();
        }
    }
}
