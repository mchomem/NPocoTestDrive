namespace NPocoTestDrive.Domain.Services
{
    public interface IEmployeeService
    {
        public Task Create(Employee entity);

        public Task Delete(Employee entity);

        public Task<Employee> Details(Employee? entity = null);

        public Task<List<Employee>> Retreave(Employee? entity = null);

        public Task Update(Employee entity);
    }
}
