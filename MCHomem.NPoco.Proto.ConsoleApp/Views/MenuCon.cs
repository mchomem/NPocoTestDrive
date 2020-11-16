using System;

namespace MCHomem.NPoco.Proto.ConsoleApp.Views
{
    public class MenuCon
    {
        public void ShowMenu()
        {
            while (true)
            {
                Boolean exit = false;
                EmployeeCon employeeCon = new EmployeeCon();

                Console.Clear();
                Console.WriteLine("Selecione uma operação:\n");
                Console.WriteLine("1. Consultar colaborador;");
                Console.WriteLine("2. Incluir novo colaborador;");
                Console.WriteLine("3. Atualizar colaborador;");
                Console.WriteLine("4. Excluir colaborador;");
                Console.WriteLine("0. Finalizar.\n");
                Console.Write("Operação: ");
                String op = Console.ReadLine();

                switch (op)
                {
                    case "1":
                        employeeCon.GetEmployees();
                        break;

                    case "2":
                        employeeCon.IncludeEmployee();
                        break;

                    case "3":
                        employeeCon.UpdateEmployee();
                        break;

                    case "4":
                        employeeCon.DeleteEmployee();
                        break;

                    case "0":
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Selecione uma operação válida!");
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
            Console.WriteLine("Pressione qualquer Tecla para encerrar.");
            Environment.Exit(0);
        }
    }
}
