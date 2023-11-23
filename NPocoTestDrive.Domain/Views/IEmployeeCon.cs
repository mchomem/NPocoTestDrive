namespace NPocoTestDrive.Domain.Views.Interfaces
{
    public interface IEmployeeCon
    {
        public Task<EmployeeDto> GetEmployee();
        public Task GetEmployees();
        public Task IncludeEmployee();
        public Task UpdateEmployee();
        public Task DeleteEmployee();
    }
}
