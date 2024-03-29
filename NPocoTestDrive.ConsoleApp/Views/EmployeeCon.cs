﻿namespace NPocoTestDrive.ConsoleApp.Views
{
    public class EmployeeCon : IEmployeeCon
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeCon(IEmployeeService employeeService)
            => _employeeService = employeeService;

        public async Task GetEmployees()
        {
            Console.Clear();
            Console.WriteLine("Getting data, wait ...");
            List<EmployeeDto> employees = await _employeeService.Retreave(null);

            var line = 120;
            var maxPagging = AppSettings.MaxPagging;
            var row = 0;
            var currentPage = 0;
            double totalPages = Math.Ceiling(Convert.ToDouble(employees.Count) / Convert.ToDouble(maxPagging));

            if (employees.Count == 0)
            {
                Console.WriteLine("Data not found");
                return;
            }

            foreach (EmployeeDto employee in employees)
            {
                if (row == 0)
                {
                    Console.Write("".PadRight(line, '-'));
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine();
                    Console.Write("ID".PadRight(10, ' '));
                    Console.Write("Name".PadRight(30, ' '));
                    Console.Write("Doc number".PadRight(20, ' '));
                    Console.Write("Active".PadRight(10, ' '));
                    Console.Write("Created in".PadRight(25, ' '));
                    Console.Write("Updated in".PadRight(25, ' '));
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write("".PadRight(line, '-'));
                    Console.WriteLine();
                }

                Console.Write(employee.Id.ToString().PadRight(10, ' '));
                Console.Write(employee.Name.PadRight(30, ' '));
                Console.Write(employee.DocumentNumber.PadRight(20, ' '));

                if (employee.Active.Value)
                    ConsoleMessage.Show("Yes".PadRight(10, ' '), ConsoleMessage.TypeMessage.OK, false, false);
                else
                    ConsoleMessage.Show("No".PadRight(10, ' '), ConsoleMessage.TypeMessage.NOK, false, false);

                Console.Write(employee.CreatedIn.ToString("dd/MM/yyyy HH:mm:ss").PadRight(25, ' '));
                Console.Write(employee.UpdatedIn?.ToString("dd/MM/yyyy HH:mm:ss").PadRight(25, ' '));
                Console.WriteLine();

                row++;

                if (maxPagging == row || currentPage == totalPages || employees.Last().Id == employee.Id)
                {
                    row = 0;
                    currentPage++;
                    Console.Write("".PadRight(line, '-'));
                    Console.WriteLine($"\nShowing {currentPage} of {totalPages} pages for {employees.Count}");
                    Console.WriteLine("\nPress any button to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        public async Task<EmployeeDto> GetEmployee()
        {
            int employeeID = 0;

            while (true)
            {
                Console.Write("Enter the employee ID: ");

                if (!int.TryParse(Console.ReadLine(), out employeeID))
                {
                    ConsoleMessage.Show("Enter an integer number for the ID.", ConsoleMessage.TypeMessage.WARNING);
                    continue;
                }

                break;
            }

            var employee = await _employeeService.Details(new EmployeeDto() { Id = employeeID });

            if (employee != null)
            {
                Console.Clear();
                Console.WriteLine("Data Emplooyee\n");
                Console.WriteLine($"Name: {employee.Name}");
                Console.WriteLine($"Document Number: {employee.DocumentNumber}");
                Console.WriteLine($"Registration status: {(employee.Active.Value ? "Active" : "Deacrive")}");
            }
            else
            {
                Console.WriteLine("Not found");
            }

            Console.ReadKey();

            return employee;
        }

        public async Task IncludeEmployee()
        {
            EmployeeDto employee = new EmployeeDto();

            while (true)
            {
                Console.Clear();
                Console.Write("Name: ");
                var name = Console.ReadLine();

                if (string.IsNullOrEmpty(name))
                {
                    ConsoleMessage.Show("Enter the employee name.", ConsoleMessage.TypeMessage.WARNING);
                    continue;
                }

                employee.Name = name;
                break;
            }

            while (true)
            {
                Console.Clear();
                Console.Write("Doc number: ");
                var documentNumber = Console.ReadLine();

                if (string.IsNullOrEmpty(documentNumber))
                {
                    ConsoleMessage.Show("Enter the employee document number.", ConsoleMessage.TypeMessage.WARNING);
                    continue;
                }

                if (documentNumber.Length < 11 || documentNumber.Length > 11)
                {
                    ConsoleMessage.Show("The document number must contain 11 characters.", ConsoleMessage.TypeMessage.WARNING);
                    continue;
                }

                employee.DocumentNumber = this.FormatDocumentNumber(documentNumber);
                break;
            }

            employee.Active = true;
            employee.CreatedIn = DateTime.Now;

            await _employeeService.Create(employee);
            ConsoleMessage.Show("Registered employee.", ConsoleMessage.TypeMessage.SUCCESS);
            Console.ReadLine();
        }

        public async Task UpdateEmployee()
        {
            EmployeeDto employee = await this.GetEmployee();

            if (employee == null)
            {
                ConsoleMessage.Show("Register not found.", ConsoleMessage.TypeMessage.WARNING);
                return;
            }

            this.GetEmployeeDetails(employee);

            while (true)
            {
                Console.Clear();
                Console.Write("Name: ");
                var name = Console.ReadLine();

                if (string.IsNullOrEmpty(name))
                {
                    ConsoleMessage.Show("Enter a name for the employee!", ConsoleMessage.TypeMessage.WARNING);
                    continue;
                }

                employee.Name = name;
                break;
            }

            while (true)
            {
                Console.Clear();
                Console.Write("Doc number: ");
                var documentNumber = Console.ReadLine();

                if (string.IsNullOrEmpty(documentNumber))
                {
                    ConsoleMessage.Show("Enter the employee's document number!", ConsoleMessage.TypeMessage.WARNING);
                    continue;
                }

                if (documentNumber.Length < 11 || documentNumber.Length > 11)
                {
                    ConsoleMessage.Show("The document number must contain 11 characters.", ConsoleMessage.TypeMessage.WARNING);
                    continue;
                }

                employee.DocumentNumber = this.FormatDocumentNumber(documentNumber);
                break;
            }

            var active = false;

            while (true)
            {
                Console.Clear();
                Console.Write("Active[y/n]? ");
                var op = Console.ReadLine()?.ToLower();

                if (op.Equals("y"))
                {
                    active = true;
                    break;
                }
                else if (op.Equals("n"))
                {
                    active = false;
                    break;
                }
                else
                {
                    ConsoleMessage.Show("Select \"y\" or \"n\" to enable or disable the employee.", ConsoleMessage.TypeMessage.WARNING);
                    continue;
                }
            }

            employee.Active = active;
            employee.UpdatedIn = DateTime.Now;
            await _employeeService.Update(employee);
            ConsoleMessage.Show("Updated data.", ConsoleMessage.TypeMessage.SUCCESS);
        }

        public async Task DeleteEmployee()
        {
            EmployeeDto employee = await this.GetEmployee();

            if (employee == null)
            {
                ConsoleMessage.Show("Employee not found.", ConsoleMessage.TypeMessage.WARNING);
                return;
            }

            this.GetEmployeeDetails(employee);

            var noDelete = false;

            while (true)
            {
                Console.Write("Do you really want to delete the collaborator record ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(employee.Name);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write(" with document number ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(employee.DocumentNumber);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write(" [y/n] ? ");
                var op = Console.ReadLine()?.ToLower();

                if (op!.Equals("y"))
                {
                    break;
                }
                else if (op.Equals("n"))
                {
                    noDelete = true;
                    break;
                }
                else
                {
                    ConsoleMessage.Show("Enter \"y\" or \"n\".", ConsoleMessage.TypeMessage.WARNING);
                    continue;
                }
            }

            if (noDelete)
            {
                ConsoleMessage.Show("Operation canceled", ConsoleMessage.TypeMessage.WARNING);
                return;
            }

            await _employeeService.Delete(employee);
            ConsoleMessage.Show("Record deleted", ConsoleMessage.TypeMessage.SUCCESS);
        }

        private void GetEmployeeDetails(EmployeeDto employee)
        {
            Console.WriteLine();
            Console.WriteLine("Name: {0}", employee.Name);
            Console.WriteLine("Doc number: {0}", employee.DocumentNumber);
            Console.WriteLine("Active: {0}", employee.Active.HasValue ? "Yes" : "No");
            Console.WriteLine();
            Console.ReadKey();
        }

        private string FormatDocumentNumber(string documentNumber)
        {
            string aux = string.Empty;

            for (int i = 0; i < documentNumber.Length; i++)
            {
                if (i == 2 || i == 5)
                    aux = aux + documentNumber[i] + ".";
                else if (i == 8)
                    aux = aux + documentNumber[i] + "-";
                else
                    aux = aux + documentNumber[i];
            }

            return aux;
        }
    }
}
