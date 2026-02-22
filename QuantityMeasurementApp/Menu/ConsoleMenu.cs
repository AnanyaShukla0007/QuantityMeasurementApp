using System;
using QuantityMeasurementApp.Models;
using QuantityMeasurementApp.Services;

namespace QuantityMeasurementApp.Menu
{
    /// <summary>
    /// Hierarchical console menu supporting UC1–UC10.
    /// No removals. Generic (UC10) appended.
    /// </summary>
    public class ConsoleMenu
    {
        private readonly QuantityMeasurementService _service;
        private readonly WeightMeasurementService _weightService;
        private readonly GenericQuantityService _genericService;

        public ConsoleMenu()
        {
            _service = new QuantityMeasurementService();
            _weightService = new WeightMeasurementService();
            _genericService = new GenericQuantityService(); // UC10
        }

        public void Run()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("=== QUANTITY MEASUREMENT APP MAIN MENU ===");
                Console.WriteLine("1. Length Operations");
                Console.WriteLine("2. Weight Operations");
                Console.WriteLine("3. Generic Quantity Operations");
                Console.WriteLine("4. Exit");

                string? choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            ShowLengthMenu();
                            break;
                        case "2":
                            ShowWeightMenu();
                            break;
                        case "3":
                            ShowGenericMenu();
                            break;
                        case "4":
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

        // ============================================================
        // ======================= LENGTH ==============================
        // ============================================================

        private void ShowLengthMenu()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("=== LENGTH MENU ===");
                Console.WriteLine("1. Equality");
                Console.WriteLine("2. Conversion");
                Console.WriteLine("3. Addition - Default Unit");
                Console.WriteLine("4. Addition - Explicit Target Unit");
                Console.WriteLine("5. Back");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": HandleLengthEquality(); break;
                    case "2": HandleLengthConversion(); break;
                    case "3": HandleLengthAdditionDefault(); break;
                    case "4": HandleLengthAdditionExplicit(); break;
                    case "5": return;
                    default:
                        Console.WriteLine("Invalid choice.");
                        Pause();
                        break;
                }
            }
        }

        private void HandleLengthEquality()
        {
            double v1 = ReadValue("Enter first value: ");
            LengthUnit u1 = ReadLengthUnit();

            double v2 = ReadValue("Enter second value: ");
            LengthUnit u2 = ReadLengthUnit();

            bool result = _service.ValidateLengthEquality(v1, u1, v2, u2);

            Console.WriteLine($"Equal: {result}");
            Pause();
        }

        private void HandleLengthConversion()
        {
            double value = ReadValue("Enter value: ");
            LengthUnit from = ReadLengthUnit();
            LengthUnit to = ReadLengthUnit();

            double result = _service.Convert(value, from, to);

            Console.WriteLine($"Converted Value: {result}");
            Pause();
        }

        private void HandleLengthAdditionDefault()
        {
            var q1 = ReadLength();
            var q2 = ReadLength();

            var result = _service.AddLength(q1, q2);

            Console.WriteLine($"Result: {result}");
            Pause();
        }

        private void HandleLengthAdditionExplicit()
        {
            var q1 = ReadLength();
            var q2 = ReadLength();

            Console.WriteLine("Select target unit:");
            LengthUnit target = ReadLengthUnit();

            var result = _service.AddLength(q1, q2, target);

            Console.WriteLine($"Result: {result}");
            Pause();
        }

        // ============================================================
        // ======================= WEIGHT ==============================
        // ============================================================

        private void ShowWeightMenu()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("=== WEIGHT MENU ===");
                Console.WriteLine("1. Equality");
                Console.WriteLine("2. Conversion");
                Console.WriteLine("3. Addition - Default Unit");
                Console.WriteLine("4. Addition - Explicit Target Unit");
                Console.WriteLine("5. Back");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": HandleWeightEquality(); break;
                    case "2": HandleWeightConversion(); break;
                    case "3": HandleWeightAdditionDefault(); break;
                    case "4": HandleWeightAdditionExplicit(); break;
                    case "5": return;
                    default:
                        Console.WriteLine("Invalid choice.");
                        Pause();
                        break;
                }
            }
        }

        private void HandleWeightEquality()
        {
            double v1 = ReadValue("Enter first value: ");
            WeightUnit u1 = ReadWeightUnit();

            double v2 = ReadValue("Enter second value: ");
            WeightUnit u2 = ReadWeightUnit();

            bool result = _weightService.ValidateWeightEquality(v1, u1, v2, u2);

            Console.WriteLine($"Equal: {result}");
            Pause();
        }

        private void HandleWeightConversion()
        {
            double value = ReadValue("Enter value: ");
            WeightUnit from = ReadWeightUnit();
            WeightUnit to = ReadWeightUnit();

            double result = _weightService.Convert(value, from, to);

            Console.WriteLine($"Converted Value: {result}");
            Pause();
        }

        private void HandleWeightAdditionDefault()
        {
            var q1 = ReadWeight();
            var q2 = ReadWeight();

            var result = _weightService.AddWeight(q1, q2);

            Console.WriteLine($"Result: {result}");
            Pause();
        }

        private void HandleWeightAdditionExplicit()
        {
            var q1 = ReadWeight();
            var q2 = ReadWeight();

            Console.WriteLine("Select target unit:");
            WeightUnit target = ReadWeightUnit();

            var result = _weightService.AddWeight(q1, q2, target);

            Console.WriteLine($"Result: {result}");
            Pause();
        }

        // ============================================================
        // ======================= GENERIC =============================
        // ============================================================

        private void ShowGenericMenu()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("=== GENERIC QUANTITY MENU (UC10) ===");
                Console.WriteLine("1. Length Example");
                Console.WriteLine("2. Weight Example");
                Console.WriteLine("3. Back");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        var l1 = new Quantity<LengthUnit>(1, LengthUnit.Feet);
                        var l2 = new Quantity<LengthUnit>(12, LengthUnit.Inches);
                        Console.WriteLine(l1.Add(l2, LengthUnit.Feet));
                        Pause();
                        break;

                    case "2":
                        var w1 = new Quantity<WeightUnit>(1, WeightUnit.Kilogram);
                        var w2 = new Quantity<WeightUnit>(1000, WeightUnit.Gram);
                        Console.WriteLine(w1.Add(w2, WeightUnit.Kilogram));
                        Pause();
                        break;

                    case "3":
                        return;
                }
            }
        }

        // ============================================================
        // ======================= HELPERS =============================
        // ============================================================

        private double ReadValue(string message)
        {
            Console.Write(message);

            if (!double.TryParse(Console.ReadLine(), out double value) ||
                !double.IsFinite(value))
                throw new ArgumentException("Invalid numeric value.");

            return value;
        }

        private QuantityLength ReadLength()
        {
            double value = ReadValue("Enter value: ");
            LengthUnit unit = ReadLengthUnit();
            return new QuantityLength(value, unit);
        }

        private QuantityWeight ReadWeight()
        {
            double value = ReadValue("Enter value: ");
            WeightUnit unit = ReadWeightUnit();
            return new QuantityWeight(value, unit);
        }

        private LengthUnit ReadLengthUnit()
        {
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

        private WeightUnit ReadWeightUnit()
        {
            Console.WriteLine("1. Kilogram");
            Console.WriteLine("2. Gram");
            Console.WriteLine("3. Pound");

            return Console.ReadLine() switch
            {
                "1" => WeightUnit.Kilogram,
                "2" => WeightUnit.Gram,
                "3" => WeightUnit.Pound,
                _ => throw new ArgumentException("Invalid weight unit selection.")
            };
        }

        private void Pause()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}