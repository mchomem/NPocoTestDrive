namespace NPocoTestDrive.Data.Repositories
{
    interface IRepository<T>
    {
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        T Details(T entity);
        List<T> Retreave(T entity);
    }
}
