using DataApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DataApp.Repositories.DataRepository
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetList();
        void Create(T entity);
        void Update(T entity, T original);
        void UpdateRange(IEnumerable<T> entities);
        void Delete(long id);
    }
}
