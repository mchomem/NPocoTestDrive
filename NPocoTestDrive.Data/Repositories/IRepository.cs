namespace NPocoTestDrive.Data.Repositories
{
    interface IRepository<T>
    {
        Task Create(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        Task<T> Details(T entity);
        Task<List<T>> Retreave(T entity);
    }
}
