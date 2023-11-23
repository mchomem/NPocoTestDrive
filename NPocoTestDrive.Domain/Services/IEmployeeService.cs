namespace NPocoTestDrive.Domain.Services
{
    public interface IEmployeeService
    {
        public Task Create(EmployeeDto entity);

        public Task Delete(EmployeeDto entity);

        public Task<EmployeeDto> Details(EmployeeDto? entity = null);

        public Task<List<EmployeeDto>> Retreave(EmployeeDto? entity = null);

        public Task Update(EmployeeDto entity);
    }
}
