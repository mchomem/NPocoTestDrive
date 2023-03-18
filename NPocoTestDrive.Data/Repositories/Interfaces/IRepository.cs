namespace NPocoTestDrive.Data.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task Create(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        Task<T> Details(T entity);
        Task<List<T>> Retreave(T entity);
    }
}
