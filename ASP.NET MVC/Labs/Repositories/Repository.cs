using Microsoft.EntityFrameworkCore;
using Task.Context;
using Task.Repositories.IRepositories;

namespace Task.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _dbSet;
        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public virtual List<T> GetAll() => _dbSet.ToList();
        public virtual T? GetById(int id) => _dbSet.Find(id);
        public virtual void Add(T entity) => _dbSet.Add(entity);
        public virtual void Update(T entity) => _dbSet.Update(entity);
        public virtual void Delete(int id)
        {
            T? existing = _dbSet.Find(id);
            if (existing is not null)
                _dbSet.Remove(existing);
        }
        public virtual void Save() => _context.SaveChanges();
    }
}
