using System;
using QuantityMeasurementApp.Models;
using QuantityMeasurementApp.Services;

namespace QuantityMeasurementApp.Menu
{
    /// <summary>
    /// Provides menu-driven interaction supporting UC1 through UC5.
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
                            HandleGenericEquality();
                            break;

                        case "4":
                            HandleConversion();
                            break;

                        case "5":
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
            Console.WriteLine("3. Generic Equality (UC3/UC4)");
            Console.WriteLine("4. Unit Conversion (UC5)");
            Console.WriteLine("5. Exit");
            Console.Write("Enter choice: ");
        }

        private void HandleFeetEquality()
        {
            double v1 = ReadValue("Enter first feet value: ");
            double v2 = ReadValue("Enter second feet value: ");

            bool result = _service.ValidateFeetEquality(v1, v2);

            Console.WriteLine($"Equal ({result})");
        }

        private void HandleInchesEquality()
        {
            double v1 = ReadValue("Enter first inches value: ");
            double v2 = ReadValue("Enter second inches value: ");

            bool result = _service.ValidateInchesEquality(v1, v2);

            Console.WriteLine($"Equal ({result})");
        }

        private void HandleGenericEquality()
        {
            double v1 = ReadValue("Enter first value: ");
            LengthUnit u1 = ReadUnit();

            double v2 = ReadValue("Enter second value: ");
            LengthUnit u2 = ReadUnit();

            bool result = _service.ValidateLengthEquality(v1, u1, v2, u2);

            Console.WriteLine($"Equal ({result})");
        }

        private void HandleConversion()
        {
            double value = ReadValue("Enter value to convert: ");
            LengthUnit from = ReadUnit();
            LengthUnit to = ReadUnit();

            double result = _service.Convert(value, from, to);

            Console.WriteLine($"Converted Value: {result}");
        }

        private double ReadValue(string message)
        {
            Console.Write(message);

            string input = Console.ReadLine();

            if (!double.TryParse(input, out double value) || !double.IsFinite(value))
                throw new ArgumentException("Invalid numeric value.");

            return value;
        }

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