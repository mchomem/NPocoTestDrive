namespace NPocoTestDrive.Domain.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task Create(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        Task<T> Details(T? entity);
        Task<T> DetailsSql(T? entity);
        Task<List<T>> Retreave(T? entity);
        Task<List<T>> RetreaveSql(T? entity);
    }
}
