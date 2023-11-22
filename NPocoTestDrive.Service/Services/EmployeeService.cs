namespace NPocoTestDrive.Service.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
            => _employeeRepository = employeeRepository;

        public Task Create(Employee entity)
            => _employeeRepository.Create(entity);

        public Task Delete(Employee entity)
            => _employeeRepository.Delete(entity);

        public Task<Employee> Details(Employee? entity = null)
            => _employeeRepository.Details(entity);

        public Task<List<Employee>> Retreave(Employee? entity = null)
            => _employeeRepository.Retreave(entity);

        public Task Update(Employee entity)
            => _employeeRepository.Update(entity);
    }
}
