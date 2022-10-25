using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Services.IRepository;

namespace Services
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly UniversityDBContext _DbContext;
        private readonly DbSet<T> _DbSet;

        public GenericRepository(UniversityDBContext context)
        {
            _DbContext = context;
            _DbSet = _DbContext.Set<T>();
        }

        public void Add(T entity)
        {
            _DbSet.Add(entity);
            _DbContext.SaveChanges();
        }

        public void Delete(T entity)
        {
            _DbSet.Remove(entity);
            _DbContext.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            return _DbSet.ToList();
        }

        public T GetById(int id)
        {
            return _DbSet.Find(id);
        }

        public void Update(T entity)
        {
            _DbSet.Attach(entity);
            _DbContext.Entry(entity).State = EntityState.Modified;
            _DbContext.SaveChanges();
        }
    }
}
