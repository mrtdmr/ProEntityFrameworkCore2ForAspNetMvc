using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models.Abstract
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll { get; }
        Task<T> GetById(long id);
        Task<T> GetById(int id);
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);
    }
}
