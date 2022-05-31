namespace NPocoTestDrive.ConsoleApp.Views
{
    public class MenuCon
    {
        public async Task ShowMenu()
        {
            while (true)
            {
                bool exit = false;
                EmployeeCon employeeCon = new EmployeeCon();

                Console.Clear();
                Console.WriteLine("Select an operation:\n");
                Console.WriteLine("1. Query an employee;");
                Console.WriteLine("2. Add employee;");
                Console.WriteLine("3. Update employee;");
                Console.WriteLine("4. Delete employee;");
                Console.WriteLine("0. Finish.\n");
                Console.Write("Operation: ");
                string? op = Console.ReadLine();

                switch (op)
                {
                    case "1":
                        await employeeCon.GetEmployees();
                        break;

                    case "2":
                        await employeeCon.IncludeEmployee();
                        break;

                    case "3":
                        await employeeCon.UpdateEmployee();
                        break;

                    case "4":
                        await employeeCon.DeleteEmployee();
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
                {
                    this.Exit();
                }
            }
        }

        private void Exit()
        {
            Console.WriteLine("Press any button to finish.");
            Environment.Exit(0);
        }
    }
}
