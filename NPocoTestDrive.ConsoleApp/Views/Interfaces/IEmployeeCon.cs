namespace NPocoTestDrive.ConsoleApp.Views.Interfaces
{
    public interface IEmployeeCon
    {
        public Task<Employee> GetEmployee();
        public Task GetEmployees();
        public Task IncludeEmployee();
        public Task UpdateEmployee();
        public Task DeleteEmployee();
    }
}
