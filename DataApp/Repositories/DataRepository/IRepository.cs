using System.Linq;

namespace DataApp.Repositories.DataRepository
{
    public interface IRepository<T> where T : class
    {
        T Get(long id);
        IQueryable<T> GetAll();
        void Create(T entity);
        void Update(T entity,T original);
        void Delete(long id);
    }
}
