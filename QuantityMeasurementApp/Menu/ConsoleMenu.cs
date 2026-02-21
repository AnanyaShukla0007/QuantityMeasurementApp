using System;
using QuantityMeasurementApp.Models;
using QuantityMeasurementApp.Services;

namespace QuantityMeasurementApp.Menu
{
    /// <summary>
    /// Hierarchical console menu supporting UC1–UC7 functionality.
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
                Console.Clear();

                Console.WriteLine("=== MAIN MENU ===");
                Console.WriteLine("1. Equality (UC1–UC4)");
                Console.WriteLine("2. Conversion (UC5)");
                Console.WriteLine("3. Addition - Default Unit (UC6)");
                Console.WriteLine("4. Addition - Explicit Target Unit (UC7)");
                Console.WriteLine("5. Exit");

                string? choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            ShowEqualityMenu();
                            break;
                        case "2":
                            HandleConversion();
                            break;
                        case "3":
                            HandleAdditionDefault();
                            break;
                        case "4":
                            HandleAdditionExplicit();
                            break;
                        case "5":
                            return;
                        default:
                            Console.WriteLine("Invalid choice.");
                            Pause();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    Pause();
                }
            }
        }

        // ================= EQUALITY =================

        private void ShowEqualityMenu()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("=== EQUALITY MENU ===");
                Console.WriteLine("1. Feet Equality (UC1)");
                Console.WriteLine("2. Inches Equality (UC2)");
                Console.WriteLine("3. Generic Equality (UC3/UC4)");
                Console.WriteLine("4. Back");

                string? choice = Console.ReadLine();

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
                        return;
                    default:
                        Console.WriteLine("Invalid choice.");
                        Pause();
                        break;
                }
            }
        }

        private void HandleFeetEquality()
        {
            double v1 = ReadValue("Enter first feet value: ");
            double v2 = ReadValue("Enter second feet value: ");

            bool result = _service.ValidateFeetEquality(v1, v2);

            Console.WriteLine($"Equal: {result}");
            Pause();
        }

        private void HandleInchesEquality()
        {
            double v1 = ReadValue("Enter first inches value: ");
            double v2 = ReadValue("Enter second inches value: ");

            bool result = _service.ValidateInchesEquality(v1, v2);

            Console.WriteLine($"Equal: {result}");
            Pause();
        }

        private void HandleGenericEquality()
        {
            double v1 = ReadValue("Enter first value: ");
            LengthUnit u1 = ReadUnit();

            double v2 = ReadValue("Enter second value: ");
            LengthUnit u2 = ReadUnit();

            bool result = _service.ValidateLengthEquality(v1, u1, v2, u2);

            Console.WriteLine($"Equal: {result}");
            Pause();
        }

        // ================= CONVERSION (UC5) =================

        private void HandleConversion()
        {
            double value = ReadValue("Enter value: ");
            LengthUnit from = ReadUnit();
            LengthUnit to = ReadUnit();

            double result = _service.Convert(value, from, to);

            Console.WriteLine($"Converted Value: {result}");
            Pause();
        }

        // ================= ADDITION UC6 =================

        private void HandleAdditionDefault()
        {
            double v1 = ReadValue("Enter first value: ");
            LengthUnit u1 = ReadUnit();

            double v2 = ReadValue("Enter second value: ");
            LengthUnit u2 = ReadUnit();

            var first = new QuantityLength(v1, u1);
            var second = new QuantityLength(v2, u2);

            var result = _service.AddLength(first, second);

            Console.WriteLine($"Result (Unit of first operand): {result}");
            Pause();
        }

        // ================= ADDITION UC7 =================

        private void HandleAdditionExplicit()
        {
            double v1 = ReadValue("Enter first value: ");
            LengthUnit u1 = ReadUnit();

            double v2 = ReadValue("Enter second value: ");
            LengthUnit u2 = ReadUnit();

            Console.WriteLine("Select target unit for result:");
            LengthUnit target = ReadUnit();

            var first = new QuantityLength(v1, u1);
            var second = new QuantityLength(v2, u2);

            var result = _service.AddLength(first, second, target);

            Console.WriteLine($"Result (Explicit Target Unit): {result}");
            Pause();
        }

        // ================= HELPERS =================

        private double ReadValue(string message)
        {
            Console.Write(message);

            if (!double.TryParse(Console.ReadLine(), out double value) ||
                !double.IsFinite(value))
            {
                throw new ArgumentException("Invalid numeric value.");
            }

            return value;
        }

        private LengthUnit ReadUnit()
        {
            Console.WriteLine("Select Unit:");
            Console.WriteLine("1. Feet");
            Console.WriteLine("2. Inches");
            Console.WriteLine("3. Yards");
            Console.WriteLine("4. Centimeters");

            return Console.ReadLine() switch
            {
                "1" => LengthUnit.Feet,
                "2" => LengthUnit.Inches,
                "3" => LengthUnit.Yards,
                "4" => LengthUnit.Centimeters,
                _ => throw new ArgumentException("Invalid unit selection.")
            };
        }

        private void Pause()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}