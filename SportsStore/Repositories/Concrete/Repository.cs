using Microsoft.EntityFrameworkCore;
using SportsStore.Models;
using SportsStore.Models.Pages;
using SportsStore.Repositories.Abstract;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SportsStore.Repositories.Concrete
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DataContext _dataContext;
        private readonly DbSet<T> _dbSet;
        public Repository(DataContext dataContext)
        {
            _dataContext = dataContext;
            _dbSet = dataContext.Set<T>();
        }
        public IQueryable<T> GetAll() => _dbSet;
        public IQueryable<T> GetAll(params object[] includeParams)
        {
            var query = _dbSet.AsQueryable();
            foreach (var item in includeParams)
            {
                query = query.Include(item.ToString());
            }
            return query.AsQueryable();
        }
        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate, params object[] includeParams)
        {
            var query = _dbSet.AsQueryable();
            foreach (var item in includeParams)
            {
                query = query.Include(item.ToString());
            }
            return query.Where(predicate).AsQueryable();
        }
        public PagedList<T> GetAll(QueryOption option, params object[] includeParams)
        {
            var query = _dbSet.AsQueryable();
            foreach (var item in includeParams)
            {
                query = query.Include(item.ToString());
            }
            return new PagedList<T>(query, option);
        }
        public async Task<T> GetById(long id)
        {
            try
            {
                return await _dbSet.FindAsync(id);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<T> GetById(int id)
        {
            try
            {
                return await _dbSet.FindAsync(id);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<T> GetById(Expression<Func<T, bool>> predicate, params object[] includeParams)
        {
            var query = _dbSet.AsQueryable();
            foreach (var item in includeParams)
            {
                query = query.Include(item.ToString());
            }
            return await query.Where(predicate).FirstOrDefaultAsync();
        }
        public async Task Add(T entity)
        {
            try
            {
                _dataContext.Add(entity);
                await _dataContext.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task Update(T entity)
        {
            try
            {
                var entry = _dataContext.Entry(entity);
                entry.OriginalValues.SetValues(await entry.GetDatabaseValuesAsync());
                foreach (var item in entry.OriginalValues.Properties)
                {
                    var original = entry.OriginalValues[item.Name];
                    var current = entry.CurrentValues[item.Name];
                    if (original != null && !original.Equals(current))
                    {
                        entry.Property(item.Name).IsModified = true;
                    }
                }
                await _dataContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task Delete(T entity)
        {
            try
            {
                _dbSet.Remove(entity);
                await _dataContext.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
