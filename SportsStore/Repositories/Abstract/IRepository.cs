﻿using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SportsStore.Repositories.Abstract
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        IQueryable<T> GetAll(params object[] includeParams);
        IQueryable<T> GetAll(Expression<Func<T, bool>> predicate, params object[] includeParams);
        Task<T> GetById(long id);
        Task<T> GetById(int id);
        Task<T> GetById(Expression<Func<T, bool>> predicate, params object[] includeParams);
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);
    }
}
