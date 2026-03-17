using System;
namespace QuantityMeasurementApp.Controller.Helpers
{
    public static class ConsoleHelper
    {
        public static void Header(string title)
        {
            Console.WriteLine("=======================================");
            Console.WriteLine($" {title}");
            Console.WriteLine("=======================================");
        }

        public static void Pause()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }
}