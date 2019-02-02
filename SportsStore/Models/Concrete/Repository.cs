using Microsoft.EntityFrameworkCore;
using SportsStore.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models.Concrete
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
        public IQueryable<T> GetAll => _dbSet;
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
