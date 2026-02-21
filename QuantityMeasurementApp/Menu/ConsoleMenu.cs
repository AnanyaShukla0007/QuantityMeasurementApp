using System;
using QuantityMeasurementApp.Services;

namespace QuantityMeasurementApp.Menu
{
    public class ConsoleMenu
    {
        private readonly QuantityMeasurementService _service;

        public ConsoleMenu()
        {
            _service = new QuantityMeasurementService();
        }

        public void Run()
        {
            while (true)
            {
                try
                {
                    ShowMenu();

                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            HandleFeetEquality();
                            break;

                        case "2":
                            Exit();
                            return;

                        default:
                            Console.WriteLine("Invalid choice.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }

                Console.WriteLine();
                Console.WriteLine("Press any key to return to menu...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        private void ShowMenu()
        {
            Console.WriteLine("=== Quantity Measurement Menu ===");
            Console.WriteLine("1. Check Feet Equality");
            Console.WriteLine("2. Exit");
            Console.Write("Enter choice: ");
        }

        private void HandleFeetEquality()
        {
            Console.Write("Enter first value: ");
            string input1 = Console.ReadLine();

            Console.Write("Enter second value: ");
            string input2 = Console.ReadLine();

            if (!double.TryParse(input1, out double value1) ||
                !double.TryParse(input2, out double value2))
            {
                throw new ArgumentException("Invalid numeric input.");
            }

            bool result = _service.ValidateFeetEquality(value1, value2);

            Console.WriteLine($"Equal ({result})");
        }

        private void Exit()
        {
            Console.WriteLine("Thank you.");
        }
    }
}