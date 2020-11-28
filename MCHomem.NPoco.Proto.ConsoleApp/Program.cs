using MCHomem.NPoco.Proto.ConsoleApp.Views;
using MCHomem.NPoco.Proto.Models.Repositories;
using System;

namespace MCHomem.NPoco.Proto.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "NPoco CRUD";
            new TestAppContext().Setup();
            new MenuCon().ShowMenu();
        }
    }
}
