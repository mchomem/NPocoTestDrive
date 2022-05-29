using MCHomem.NPoco.Proto.ConsoleApp.Views;
using System;

namespace MCHomem.NPoco.Proto.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "NPoco Test drive CRUD";
            new MenuCon().ShowMenu();
        }
    }
}
