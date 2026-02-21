using System;
using QuantityMeasurementApp.Models;
using QuantityMeasurementApp.Services;

namespace QuantityMeasurementApp.Menu
{
    /// <summary>
    /// Provides menu-driven interaction supporting UC1, UC2, UC3, and UC4 comparisons.
    /// </summary>
    public class ConsoleMenu
    {
        // Service dependency
        private readonly QuantityMeasurementService _service;

        // Constructor initializes service
        public ConsoleMenu()
        {
            _service = new QuantityMeasurementService();
        }

        // Runs menu loop
        public void Run()
        {
            while (true)
            {
                try
                {
                    ShowMenu();

                    // Read user choice
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
                    // Handle all exceptions without crashing
                    Console.WriteLine($"Error: {ex.Message}");
                }

                Console.WriteLine("\nPress any key to return to menu...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        // Displays menu options
        private void ShowMenu()
        {
            Console.WriteLine("=== Quantity Measurement Menu ===");
            Console.WriteLine("1. Feet Equality (UC1)");
            Console.WriteLine("2. Inches Equality (UC2)");
            Console.WriteLine("3. Generic Length Comparison (UC3/UC4)");
            Console.WriteLine("4. Exit");
            Console.Write("Enter choice: ");
        }

        // Handles UC1 feet comparison
        private void HandleFeetEquality()
        {
            Console.Write("Enter first feet value: ");
            double v1 = double.Parse(Console.ReadLine());

            Console.Write("Enter second feet value: ");
            double v2 = double.Parse(Console.ReadLine());

            bool result = _service.ValidateFeetEquality(v1, v2);

            Console.WriteLine($"Equal ({result})");
        }

        // Handles UC2 inches comparison
        private void HandleInchesEquality()
        {
            Console.Write("Enter first inches value: ");
            double v1 = double.Parse(Console.ReadLine());

            Console.Write("Enter second inches value: ");
            double v2 = double.Parse(Console.ReadLine());

            bool result = _service.ValidateInchesEquality(v1, v2);

            Console.WriteLine($"Equal ({result})");
        }

        // Handles UC3/UC4 generic comparison
        private void HandleGenericComparison()
        {
            Console.Write("Enter first value: ");
            double v1 = double.Parse(Console.ReadLine());

            LengthUnit u1 = ReadUnit();

            Console.Write("Enter second value: ");
            double v2 = double.Parse(Console.ReadLine());

            LengthUnit u2 = ReadUnit();

            bool result = _service.ValidateLengthEquality(v1, u1, v2, u2);

            Console.WriteLine($"Equal ({result})");
        }

        // Reads unit selection from user
        private LengthUnit ReadUnit()
        {
            Console.WriteLine("Select Unit:");
            Console.WriteLine("1. Feet");
            Console.WriteLine("2. Inches");
            Console.WriteLine("3. Yards");
            Console.WriteLine("4. Centimeters");

            string choice = Console.ReadLine();

            return choice switch
            {
                "1" => LengthUnit.Feet,
                "2" => LengthUnit.Inches,
                "3" => LengthUnit.Yards,
                "4" => LengthUnit.Centimeters,
                _ => throw new ArgumentException("Invalid unit selection.")
            };
        }
    }
}