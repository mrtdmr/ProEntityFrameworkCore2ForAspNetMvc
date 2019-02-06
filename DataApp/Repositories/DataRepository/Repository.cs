using DataApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace DataApp.Repositories.DataRepository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly EFDatabaseContext _context;
        private readonly DbSet<T> _dbSet;
        public Repository(EFDatabaseContext ctx)
        {
            _context = ctx;
            _dbSet = _context.Set<T>();
        }

        public void Create(T entity)
        {
            //Console.WriteLine("Create Entity: " + JsonConvert.SerializeObject((T)Activator.CreateInstance(typeof(T))));
            Console.WriteLine("Create Entity: " + JsonConvert.SerializeObject(entity));
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(long id)
        {
            Console.WriteLine("Delete Entity: " + id);
            //_dbSet.Remove(Get(id)); // Burada DB'den fazladan 1 sorgu çekmekten kurtulduk.
            _dbSet.Remove((T)Activator.CreateInstance(typeof(T), id));
            _context.SaveChanges();
        }

        public T Get(long id)
        {
            //Console.WriteLine("Entity: " + id);
            //return (T)Activator.CreateInstance(typeof(T));
            return _dbSet.Find(id);
        }

        public IQueryable<T> GetAll()
        {
            Console.WriteLine("Get all entities");
            return _dbSet;
        }

        public void Update(T entity, T originalEntity)
        {
            Console.WriteLine("Update Entity : " + JsonConvert.SerializeObject((T)Activator.CreateInstance(typeof(T))));
            //_dbSet.Update(entity);
            //_context.SaveChanges(); 
            //EntityEntry<T> entry = null;
            //if (originalEntity == null)
            //{
            //    entry = _context.Entry(entity); // Entry metodu change detection olarak çalışır. 
            //}
            //else
            //{
            //    entry = _context.Attach(originalEntity);
            //}
            var entry = _context.Entry(entity); // Entry metodu change detection olarak çalışır. 
            entry.OriginalValues.SetValues(entry.GetDatabaseValues());
            foreach (var item in entry.OriginalValues.Properties)
            {
                var original = entry.OriginalValues[item.Name];
                var current = entry.CurrentValues[item.Name];
                if (original != null && !original.Equals(current))
                {
                    entry.Property(item.Name).IsModified = true;
                }
            }
            _context.SaveChanges();
        }
    }
}
