using NPocoTestDrive.ConsoleApp.Views.Interfaces;

namespace NPocoTestDrive.ConsoleApp.Views
{
    public class MenuCon : IMenuCon
    {
        private readonly IEmployeeCon _employeeCon;

        public MenuCon(IEmployeeCon employeeCon)
            => _employeeCon = employeeCon;

        public async Task ShowMenu()
        {
            Console.Title = "NPoco Test drive CRUD";

            while (true)
            {
                var exit = false;

                Console.Clear();
                Console.WriteLine("Select an operation:\n");
                Console.WriteLine("1. Query an employee;");
                Console.WriteLine("2. Query all employees;");
                Console.WriteLine("3. Add employee;");
                Console.WriteLine("4. Update employee;");
                Console.WriteLine("5. Delete employee;");
                Console.WriteLine("0. Finish.\n");
                Console.Write("Operation: ");
                var op = Console.ReadLine();

                switch (op)
                {
                    case "1":
                        await _employeeCon.GetEmployee();
                        break;

                    case "2":
                        await _employeeCon.GetEmployees();
                        break;

                    case "3":
                        await _employeeCon.IncludeEmployee();
                        break;

                    case "4":
                        await _employeeCon.UpdateEmployee();
                        break;

                    case "5":
                        await _employeeCon.DeleteEmployee();
                        break;

                    case "0":
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Select a valid operation!");
                        Console.ReadKey();
                        break;
                }

                if (exit)
                    this.Exit();
            }
        }

        private void Exit()
        {
            Console.WriteLine("Press any button to finish.");
            Environment.Exit(0);
        }
    }
}
