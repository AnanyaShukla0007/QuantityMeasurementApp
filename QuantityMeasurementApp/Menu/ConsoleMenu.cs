using System;
using QuantityMeasurementApp.Models;
using QuantityMeasurementApp.Services;

namespace QuantityMeasurementApp.Menu
{
    public class ConsoleMenu
    {
        private readonly QuantityMeasurementService _lengthService;
        private readonly WeightMeasurementService _weightService;
        private readonly GenericQuantityService _genericService;

        public ConsoleMenu()
        {
            _lengthService = new QuantityMeasurementService();
            _weightService = new WeightMeasurementService();
            _genericService = new GenericQuantityService();
        }

        public void Run()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== MAIN MENU ===");
                Console.WriteLine("1. Length Operations");
                Console.WriteLine("2. Weight Operations");
                Console.WriteLine("3. Volume Operations");
                Console.WriteLine("4. Generic Demo");
                Console.WriteLine("5. Exit");

                switch (Console.ReadLine())
                {
                    case "1": ShowLengthMenu(); break;
                    case "2": ShowWeightMenu(); break;
                    case "3": ShowVolumeMenu(); break;
                    case "4": ShowGenericDemo(); break;
                    case "5": return;
                    default: Invalid(); break;
                }
            }
        }

        // ======================================================
        // LENGTH MENU (UC1–UC7 + UC12)
        // ======================================================

        private void ShowLengthMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== LENGTH MENU ===");
                Console.WriteLine("1. Equality");
                Console.WriteLine("2. Conversion");
                Console.WriteLine("3. Addition");
                Console.WriteLine("4. Subtraction");
                Console.WriteLine("5. Division");
                Console.WriteLine("6. Back");

                switch (Console.ReadLine())
                {
                    case "1": HandleLengthEquality(); break;
                    case "2": HandleLengthConversion(); break;
                    case "3": HandleLengthAddition(); break;
                    case "4": HandleLengthSubtraction(); break;
                    case "5": HandleLengthDivision(); break;
                    case "6": return;
                    default: Invalid(); break;
                }
            }
        }

        private void HandleLengthEquality()
        {
            double v1 = ReadValue("Enter first value: ");
            LengthUnit u1 = ReadLengthUnit();
            double v2 = ReadValue("Enter second value: ");
            LengthUnit u2 = ReadLengthUnit();

            bool result = _lengthService.ValidateLengthEquality(v1, u1, v2, u2);
            Console.WriteLine($"Equal: {result}");
            Pause();
        }

        private void HandleLengthConversion()
        {
            double value = ReadValue("Enter value: ");
            LengthUnit from = ReadLengthUnit();
            LengthUnit to = ReadLengthUnit();

            double result = _lengthService.Convert(value, from, to);
            Console.WriteLine($"Converted: {result}");
            Pause();
        }

        private void HandleLengthAddition()
        {
            var q1 = ReadLength();
            var q2 = ReadLength();

            var result = _lengthService.AddLength(q1, q2);
            Console.WriteLine($"Result: {result}");
            Pause();
        }

        private void HandleLengthSubtraction()
        {
            var q1 = ReadLength();
            var q2 = ReadLength();

            var result = q1.Subtract(q2);
            Console.WriteLine($"Result: {result}");
            Pause();
        }

        private void HandleLengthDivision()
        {
            var q1 = ReadLength();
            var q2 = ReadLength();

            double result = q1.Divide(q2);
            Console.WriteLine($"Ratio: {result}");
            Pause();
        }

        // ======================================================
        // WEIGHT MENU (UC9 + UC12)
        // ======================================================

        private void ShowWeightMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== WEIGHT MENU ===");
                Console.WriteLine("1. Equality");
                Console.WriteLine("2. Conversion");
                Console.WriteLine("3. Addition");
                Console.WriteLine("4. Subtraction");
                Console.WriteLine("5. Division");
                Console.WriteLine("6. Back");

                switch (Console.ReadLine())
                {
                    case "1": HandleWeightEquality(); break;
                    case "2": HandleWeightConversion(); break;
                    case "3": HandleWeightAddition(); break;
                    case "4": HandleWeightSubtraction(); break;
                    case "5": HandleWeightDivision(); break;
                    case "6": return;
                    default: Invalid(); break;
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
            Console.WriteLine($"Converted: {result}");
            Pause();
        }

        private void HandleWeightAddition()
        {
            var q1 = ReadWeight();
            var q2 = ReadWeight();

            var result = _weightService.AddWeight(q1, q2);
            Console.WriteLine($"Result: {result}");
            Pause();
        }

        private void HandleWeightSubtraction()
        {
            var q1 = ReadWeight();
            var q2 = ReadWeight();

            var result = q1.Subtract(q2);
            Console.WriteLine($"Result: {result}");
            Pause();
        }

        private void HandleWeightDivision()
        {
            var q1 = ReadWeight();
            var q2 = ReadWeight();

            double result = q1.Divide(q2);
            Console.WriteLine($"Ratio: {result}");
            Pause();
        }

        // ======================================================
        // VOLUME MENU (UC11 + UC12)
        // ======================================================

        private void ShowVolumeMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== VOLUME MENU ===");
                Console.WriteLine("1. Equality");
                Console.WriteLine("2. Conversion");
                Console.WriteLine("3. Addition");
                Console.WriteLine("4. Subtraction");
                Console.WriteLine("5. Division");
                Console.WriteLine("6. Back");

                switch (Console.ReadLine())
                {
                    case "1": HandleVolumeEquality(); break;
                    case "2": HandleVolumeConversion(); break;
                    case "3": HandleVolumeAddition(); break;
                    case "4": HandleVolumeSubtraction(); break;
                    case "5": HandleVolumeDivision(); break;
                    case "6": return;
                    default: Invalid(); break;
                }
            }
        }

        private void HandleVolumeEquality()
        {
            double v1 = ReadValue("Enter first value: ");
            VolumeUnit u1 = ReadVolumeUnit();
            double v2 = ReadValue("Enter second value: ");
            VolumeUnit u2 = ReadVolumeUnit();

            bool result = _genericService.ValidateEquality(v1, u1, v2, u2);
            Console.WriteLine($"Equal: {result}");
            Pause();
        }

        private void HandleVolumeConversion()
        {
            double value = ReadValue("Enter value: ");
            VolumeUnit from = ReadVolumeUnit();
            VolumeUnit to = ReadVolumeUnit();

            var result = _genericService.Convert(value, from, to);
            Console.WriteLine($"Converted: {result}");
            Pause();
        }

        private void HandleVolumeAddition()
        {
            var q1 = new Quantity<VolumeUnit>(
                ReadValue("Enter first value: "),
                ReadVolumeUnit());

            var q2 = new Quantity<VolumeUnit>(
                ReadValue("Enter second value: "),
                ReadVolumeUnit());

            var result = _genericService.Add(q1, q2);
            Console.WriteLine($"Result: {result}");
            Pause();
        }

        private void HandleVolumeSubtraction()
        {
            var q1 = new Quantity<VolumeUnit>(
                ReadValue("Enter first value: "),
                ReadVolumeUnit());

            var q2 = new Quantity<VolumeUnit>(
                ReadValue("Enter second value: "),
                ReadVolumeUnit());

            var result = _genericService.Subtract(q1, q2);
            Console.WriteLine($"Result: {result}");
            Pause();
        }

        private void HandleVolumeDivision()
        {
            var q1 = new Quantity<VolumeUnit>(
                ReadValue("Enter first value: "),
                ReadVolumeUnit());

            var q2 = new Quantity<VolumeUnit>(
                ReadValue("Enter second value: "),
                ReadVolumeUnit());

            double result = _genericService.Divide(q1, q2);
            Console.WriteLine($"Ratio: {result}");
            Pause();
        }

        // ======================================================
        // GENERIC DEMO (UC10)
        // ======================================================

        private void ShowGenericDemo()
        {
            Console.Clear();
            var l1 = new Quantity<LengthUnit>(1, LengthUnit.Feet);
            var l2 = new Quantity<LengthUnit>(12, LengthUnit.Inches);

            Console.WriteLine("Generic Add Example:");
            Console.WriteLine(l1.Add(l2));
            Pause();
        }

        // ======================================================
        // HELPERS
        // ======================================================

        private QuantityLength ReadLength() =>
            new QuantityLength(ReadValue("Enter value: "), ReadLengthUnit());

        private QuantityWeight ReadWeight() =>
            new QuantityWeight(ReadValue("Enter value: "), ReadWeightUnit());

        private double ReadValue(string message)
        {
            Console.Write(message);
            if (!double.TryParse(Console.ReadLine(), out double value) || !double.IsFinite(value))
                throw new ArgumentException("Invalid numeric value.");
            return value;
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
                _ => throw new ArgumentException("Invalid unit")
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
                _ => throw new ArgumentException("Invalid unit")
            };
        }

        private VolumeUnit ReadVolumeUnit()
        {
            Console.WriteLine("1. Litre");
            Console.WriteLine("2. Millilitre");
            Console.WriteLine("3. Gallon");

            return Console.ReadLine() switch
            {
                "1" => VolumeUnit.LITRE,
                "2" => VolumeUnit.MILLILITRE,
                "3" => VolumeUnit.GALLON,
                _ => throw new ArgumentException("Invalid unit")
            };
        }

        private void Invalid()
        {
            Console.WriteLine("Invalid choice.");
            Pause();
        }

        private void Pause()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}