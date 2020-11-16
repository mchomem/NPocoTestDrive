using MCHomem.NPoco.Proto.Models.Entities;
using MCHomem.NPoco.Proto.Models.Repositories;
using System;
using System.Collections.Generic;

namespace MCHomem.NPoco.Proto.ConsoleApp.Views
{
    public class EmployeeCon
    {
        public void GetEmployees()
        {
            Console.Clear();
            EmployeeRepository repository = new EmployeeRepository();
            List<Employee> employees = repository.Retreave(null);

            Int32 line = 120;
            Int32 pagging = 30;
            Int32 index = 0;
            Int32 currentPage = 0;
            Double pages = Math.Ceiling(Convert.ToDouble(employees.Count) / Convert.ToDouble(pagging));

            foreach (Employee employee in employees)
            {
                if (index == 0)
                {
                    Console.Write("".PadRight(line, '-'));
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine();
                    Console.Write("ID".PadRight(10, ' '));
                    Console.Write("Nome".PadRight(30, ' '));
                    Console.Write("CPF".PadRight(20, ' '));
                    Console.Write("Ativo".PadRight(10, ' '));
                    Console.Write("Criado em".PadRight(25, ' '));
                    Console.Write("Atualizado em".PadRight(25, ' '));
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write("".PadRight(line, '-'));
                    Console.WriteLine();
                }

                Console.Write(employee.EmployeeID.ToString().PadRight(10, ' '));
                Console.Write(employee.Name.PadRight(30, ' '));
                Console.Write(employee.DocumentNumber.PadRight(20, ' '));

                if (employee.Active.Value)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("Sim".PadRight(10, ' '));
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Não".PadRight(10, ' '));
                    Console.ForegroundColor = ConsoleColor.Gray;
                }

                Console.Write(employee.CreatedIn.ToString("dd/MM/yyyy HH:mm:ss").PadRight(25, ' '));
                Console.Write(employee.UpdatedIn?.ToString("dd/MM/yyyy HH:mm:ss").PadRight(25, ' '));
                Console.WriteLine();

                index++;

                if (pagging == index || currentPage == pages)
                {
                    currentPage++;
                    index = 0;
                    Console.Write("".PadRight(line, '-'));
                    Console.WriteLine($"\nTotal de registros {employees.Count}.\n\nExibindo {currentPage} de {pages} páginas.");
                    Console.WriteLine("\nPressione uma tecla para continuar...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
            Console.ReadKey();
        }

        private Employee GetEmployee()
        {
            EmployeeRepository repository = new EmployeeRepository();
            Int32 employeeID = 0;

            while (true)
            {
                Console.Write("Informe o ID do colaborador: ");
                if (!Int32.TryParse(Console.ReadLine(), out employeeID))
                {
                    Console.WriteLine("Informe um número inteiro para o ID.");
                    Console.ReadKey();
                    continue;
                }
                break;
            }

            return repository.Details(new Employee() { EmployeeID = employeeID });
        }

        public void IncludeEmployee()
        {
            EmployeeRepository repository = new EmployeeRepository();

            Employee employee = new Employee();

            while (true)
            {
                Console.Clear();
                Console.Write("Nome: ");
                String name = Console.ReadLine();
                if (String.IsNullOrEmpty(name))
                {
                    Console.WriteLine("Informe o nome do colaborador.");
                    continue;
                }
                employee.Name = name;
                break;
            }

            while (true)
            {
                Console.Clear();
                Console.Write("CPF: ");
                String documentNumber = Console.ReadLine();
                if (String.IsNullOrEmpty(documentNumber))
                {
                    Console.WriteLine("Informe o CPF do colaborador.");
                    continue;
                }

                if (documentNumber.Length < 11 || documentNumber.Length > 11)
                {
                    Console.WriteLine("O CPF deve conter 11 caraceres.");
                    continue;
                }

                String aux = String.Empty;
                for (int i = 0; i < documentNumber.Length; i++)
                {
                    if (i == 2 || i == 5)
                        aux = aux + documentNumber[i] + ".";
                    else if (i == 8)
                        aux = aux + documentNumber[i] + "-";
                    else
                        aux = aux + documentNumber[i];
                }

                employee.DocumentNumber = aux;
                break;
            }

            employee.Active = true;
            employee.CreatedIn = DateTime.Now;

            repository.Create(employee);
            Console.WriteLine("Colaborador cadastrado.");
            Console.ReadLine();
        }

        public void UpdateEmployee()
        {
            Employee employee = this.GetEmployee();

            if (employee == null)
            {
                Console.WriteLine("Registro não encontrado.");
                Console.ReadKey();
                return;
            }

            EmployeeRepository repository = new EmployeeRepository();

            while (true)
            {
                Console.Clear();
                Console.Write("Nome: ");
                String name = Console.ReadLine();
                if (String.IsNullOrEmpty(name))
                {
                    Console.WriteLine("Informe um nome para o colaborador!");
                    continue;
                }

                employee.Name = name;
                break;
            }

            while (true)
            {
                Console.Clear();
                Console.Write("CPF: ");
                String documentNumber = Console.ReadLine();
                if (String.IsNullOrEmpty(documentNumber))
                {
                    Console.WriteLine("Informe o CPF do colaborador!");
                    continue;
                }

                employee.DocumentNumber = documentNumber;
                break;
            }

            Boolean active;

            while (true)
            {
                Console.Clear();
                Console.Write("Ativo [s/n]? ");
                String op = Console.ReadLine().ToLower();

                if (op.Equals("s"))
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
                    Console.WriteLine("Selecione \"s\" ou \"n\" para ativar ou desativar o colaborador.");
                    Console.ReadLine();
                    continue;
                }
            }

            employee.Active = active;
            employee.UpdatedIn = DateTime.Now;
            repository.Update(employee);
            Console.WriteLine("Dados atualizados.");
            Console.ReadKey();
        }

        public void DeleteEmployee()
        {
            Employee employee = this.GetEmployee();

            if (employee == null)
            {
                Console.WriteLine("Colaborador não localizado");
                Console.ReadKey();
                return;
            }

            Boolean noDelete = false;

            while (true)
            {
                Console.Write("Deseja mesmo excluir o registro do colaborador ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(employee.Name);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write(" com CPF ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(employee.DocumentNumber);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write(" [s/n] ? ");
                String op = Console.ReadLine().ToLower();

                if (op.Equals("s"))
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
                    Console.WriteLine("Informe \"s\" ou \"n\"");
                    Console.ReadKey();
                    continue;
                }
            }

            if (noDelete)
            {
                Console.WriteLine("Operação cancelada");
                Console.ReadKey();
                return;
            }

            EmployeeRepository repository = new EmployeeRepository();
            repository.Delete(employee);
            Console.WriteLine("Registro excluído.");
            Console.ReadKey();
        }
    }
}
