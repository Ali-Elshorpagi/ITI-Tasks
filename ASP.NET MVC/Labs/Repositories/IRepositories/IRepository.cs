namespace Task.Repositories.IRepositories
{
    public interface IRepository<T> where T : class
    {
        public List<T> GetAll();
        public T? GetById(int id);
        public void Add(T entity);
        public void Update(T entity);
        public void Delete(int id);
        public void Save();
    }
}
