using MCHomem.NPoco.Proto.ConsoleApp.Views.UserControl;
using MCHomem.NPoco.Proto.Models;
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
            Console.WriteLine("Consultando, aguarde ...");
            EmployeeRepository repository = new EmployeeRepository();
            List<Employee> employees = repository.Retreave(null);
            Console.Clear();

            Int32 line = 120;
            Int32 maxPagging = AppSettings.MaxPagging;
            Int32 index = 0;
            Int32 currentPage = 0;
            Double totalPages = Math.Ceiling(Convert.ToDouble(employees.Count) / Convert.ToDouble(maxPagging));

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
                    ConsoleMessage.Show("Sim".PadRight(10, ' '), ConsoleMessage.TypeMessage.OK, false, false);
                }
                else
                {
                    ConsoleMessage.Show("Não".PadRight(10, ' '), ConsoleMessage.TypeMessage.NOK, false, false);
                }

                Console.Write(employee.CreatedIn.ToString("dd/MM/yyyy HH:mm:ss").PadRight(25, ' '));
                Console.Write(employee.UpdatedIn?.ToString("dd/MM/yyyy HH:mm:ss").PadRight(25, ' '));
                Console.WriteLine();

                index++;

                if (maxPagging == index || currentPage == totalPages)
                {
                    index = 0;
                    currentPage++;
                    Console.Write("".PadRight(line, '-'));
                    Console.WriteLine($"\nTotal de registros {employees.Count}.\n\nExibindo {currentPage} de {totalPages} páginas.");
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
                    ConsoleMessage.Show("Informe um número inteiro para o ID.", ConsoleMessage.TypeMessage.WARNING);
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
                    ConsoleMessage.Show("Informe o nome do colaborador.", ConsoleMessage.TypeMessage.WARNING);
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
                    ConsoleMessage.Show("Informe o CPF do colaborador.", ConsoleMessage.TypeMessage.WARNING);
                    continue;
                }

                if (documentNumber.Length < 11 || documentNumber.Length > 11)
                {
                    ConsoleMessage.Show("O CPF deve conter 11 caraceres.", ConsoleMessage.TypeMessage.WARNING);
                    continue;
                }                

                employee.DocumentNumber = this.FormatDocumentNumber(documentNumber);
                break;
            }

            employee.Active = true;
            employee.CreatedIn = DateTime.Now;

            repository.Create(employee);
            ConsoleMessage.Show("Colaborador cadastrado.", ConsoleMessage.TypeMessage.SUCCESS);
            Console.ReadLine();
        }

        public void UpdateEmployee()
        {
            Employee employee = this.GetEmployee();

            if (employee == null)
            {
                ConsoleMessage.Show("Registro não encontrado.", ConsoleMessage.TypeMessage.WARNING);
                return;
            }

            this.GetEmployeeDetails(employee);

            EmployeeRepository repository = new EmployeeRepository();

            while (true)
            {
                Console.Clear();
                Console.Write("Nome: ");
                String name = Console.ReadLine();
                if (String.IsNullOrEmpty(name))
                {
                    ConsoleMessage.Show("Informe um nome para o colaborador!", ConsoleMessage.TypeMessage.WARNING);
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
                    ConsoleMessage.Show("Informe o CPF do colaborador!", ConsoleMessage.TypeMessage.WARNING);
                    continue;
                }

                if (documentNumber.Length < 11 || documentNumber.Length > 11)
                {
                    ConsoleMessage.Show("O CPF deve conter 11 caraceres.", ConsoleMessage.TypeMessage.WARNING);
                    continue;
                }
                
                employee.DocumentNumber = this.FormatDocumentNumber(documentNumber);
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
                    ConsoleMessage.Show("Selecione \"s\" ou \"n\" para ativar ou desativar o colaborador.", ConsoleMessage.TypeMessage.WARNING);
                    continue;
                }
            }

            employee.Active = active;
            employee.UpdatedIn = DateTime.Now;
            repository.Update(employee);
            ConsoleMessage.Show("Dados atualizados.", ConsoleMessage.TypeMessage.SUCCESS);
        }

        public void DeleteEmployee()
        {
            Employee employee = this.GetEmployee();

            if (employee == null)
            {
                ConsoleMessage.Show("Colaborador não localizado.", ConsoleMessage.TypeMessage.WARNING);
                return;
            }

            this.GetEmployeeDetails(employee);

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
                    ConsoleMessage.Show("Informe \"s\" ou \"n\".", ConsoleMessage.TypeMessage.WARNING);
                    continue;
                }
            }

            if (noDelete)
            {
                ConsoleMessage.Show("Operação cancelada", ConsoleMessage.TypeMessage.WARNING);
                return;
            }

            EmployeeRepository repository = new EmployeeRepository();
            repository.Delete(employee);
            ConsoleMessage.Show("Registro excluído", ConsoleMessage.TypeMessage.SUCCESS);
        }

        private void GetEmployeeDetails(Employee employee)
        {
            Console.WriteLine();
            Console.WriteLine("Nome: {0}", employee.Name);
            Console.WriteLine("CPF: {0}", employee.DocumentNumber);
            Console.WriteLine("Ativo: {0}", employee.Active.HasValue ? "Sim" : "Não");
            Console.WriteLine();
            Console.ReadKey();
        }

        private String FormatDocumentNumber(String documentNumber)
        {
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

            return aux;
        }
    }
}
