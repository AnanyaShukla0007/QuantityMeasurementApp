using System;
using QuantityMeasurementApp.Models;
using QuantityMeasurementApp.Services;

namespace QuantityMeasurementApp.Menu
{
    /// <summary>
    /// Handles menu-driven interaction for length comparison.
    /// </summary>
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
                            HandleInchesEquality();
                            break;

                        case "3":
                            HandleGenericComparison();
                            break;

                        case "4":
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

                Console.WriteLine("\nPress any key to return to menu...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        private void ShowMenu()
        {
            Console.WriteLine("=== Quantity Measurement Menu ===");
            Console.WriteLine("1. Feet Equality (UC1)");
            Console.WriteLine("2. Inches Equality (UC2)");
            Console.WriteLine("3. Generic Length Comparison (UC3)");
            Console.WriteLine("4. Exit");
            Console.Write("Enter choice: ");
        }

        private void HandleFeetEquality()
        {
            Console.Write("Enter first feet value: ");
            double v1 = double.Parse(Console.ReadLine());

            Console.Write("Enter second feet value: ");
            double v2 = double.Parse(Console.ReadLine());

            bool result = _service.ValidateFeetEquality(v1, v2);

            Console.WriteLine($"Equal ({result})");
        }

        private void HandleInchesEquality()
        {
            Console.Write("Enter first inches value: ");
            double v1 = double.Parse(Console.ReadLine());

            Console.Write("Enter second inches value: ");
            double v2 = double.Parse(Console.ReadLine());

            bool result = _service.ValidateInchesEquality(v1, v2);

            Console.WriteLine($"Equal ({result})");
        }

        private void HandleGenericComparison()
        {
            Console.Write("Enter first value: ");
            double v1 = double.Parse(Console.ReadLine());

            Console.Write("Select unit (1=Feet, 2=Inches): ");
            LengthUnit u1 = Console.ReadLine() == "1" ? LengthUnit.Feet : LengthUnit.Inches;

            Console.Write("Enter second value: ");
            double v2 = double.Parse(Console.ReadLine());

            Console.Write("Select unit (1=Feet, 2=Inches): ");
            LengthUnit u2 = Console.ReadLine() == "1" ? LengthUnit.Feet : LengthUnit.Inches;

            bool result = _service.ValidateLengthEquality(v1, u1, v2, u2);

            Console.WriteLine($"Equal ({result})");
        }
    }
}