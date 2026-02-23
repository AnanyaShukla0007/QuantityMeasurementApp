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
        private readonly TemperatureMeasurementService _temperatureService;

        public ConsoleMenu()
        {
            _lengthService = new QuantityMeasurementService();
            _weightService = new WeightMeasurementService();
            _genericService = new GenericQuantityService();
            _temperatureService = new TemperatureMeasurementService();
        }

        public void Run()
        {
            while (true)
            {
                Console.WriteLine("=== MAIN MENU ===");
                Console.WriteLine("1. Length Operations");
                Console.WriteLine("2. Weight Operations");
                Console.WriteLine("3. Volume Operations");
                Console.WriteLine("4. Temperature Operations");
                Console.WriteLine("5. Exit");

                switch (Console.ReadLine())
                {
                    case "1": ShowLengthMenu(); break;
                    case "2": ShowWeightMenu(); break;
                    case "3": ShowVolumeMenu(); break;
                    case "4": ShowTemperatureMenu(); break;
                    case "5": return;
                    default: Invalid(); break;
                }
            }
        }

        // ======================================================
        // LENGTH MENU
        // ======================================================

        private void ShowLengthMenu()
        {
            while (true)
            {
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
            var q1 = ReadLength();
            var q2 = ReadLength();
            Console.WriteLine($"Equal: {q1.Equals(q2)}");
            Pause();
        }

        private void HandleLengthConversion()
        {
            var q = ReadLength();
            LengthUnit target = ReadLengthUnit();
            Console.WriteLine($"Converted: {q.ConvertTo(target)}");
            Pause();
        }

        private void HandleLengthAddition()
        {
            var q1 = ReadLength();
            var q2 = ReadLength();
            Console.WriteLine($"Result: {q1.Add(q2)}");
            Pause();
        }

        private void HandleLengthSubtraction()
        {
            var q1 = ReadLength();
            var q2 = ReadLength();
            Console.WriteLine($"Result: {q1.Subtract(q2)}");
            Pause();
        }

        private void HandleLengthDivision()
        {
            var q1 = ReadLength();
            var q2 = ReadLength();
            Console.WriteLine($"Ratio: {q1.Divide(q2)}");
            Pause();
        }

        // ======================================================
        // WEIGHT MENU
        // ======================================================

        private void ShowWeightMenu()
        {
            while (true)
            {
                
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
            var q1 = ReadWeight();
            var q2 = ReadWeight();
            Console.WriteLine($"Equal: {q1.Equals(q2)}");
            Pause();
        }

        private void HandleWeightConversion()
        {
            var q = ReadWeight();
            WeightUnit target = ReadWeightUnit();
            Console.WriteLine($"Converted: {q.ConvertTo(target)}");
            Pause();
        }

        private void HandleWeightAddition()
        {
            var q1 = ReadWeight();
            var q2 = ReadWeight();
            Console.WriteLine($"Result: {q1.Add(q2)}");
            Pause();
        }

        private void HandleWeightSubtraction()
        {
            var q1 = ReadWeight();
            var q2 = ReadWeight();
            Console.WriteLine($"Result: {q1.Subtract(q2)}");
            Pause();
        }

        private void HandleWeightDivision()
        {
            var q1 = ReadWeight();
            var q2 = ReadWeight();
            Console.WriteLine($"Ratio: {q1.Divide(q2)}");
            Pause();
        }

        // ======================================================
        // VOLUME MENU
        // ======================================================

        private void ShowVolumeMenu()
        {
            while (true)
            {
                
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
            var q1 = ReadVolume();
            var q2 = ReadVolume();
            Console.WriteLine($"Equal: {q1.Equals(q2)}");
            Pause();
        }

        private void HandleVolumeConversion()
        {
            var q = ReadVolume();
            VolumeUnit target = ReadVolumeUnit();
            Console.WriteLine($"Converted: {q.ConvertTo(target)}");
            Pause();
        }

        private void HandleVolumeAddition()
        {
            var q1 = ReadVolume();
            var q2 = ReadVolume();
            Console.WriteLine($"Result: {q1.Add(q2)}");
            Pause();
        }

        private void HandleVolumeSubtraction()
        {
            var q1 = ReadVolume();
            var q2 = ReadVolume();
            Console.WriteLine($"Result: {q1.Subtract(q2)}");
            Pause();
        }

        private void HandleVolumeDivision()
        {
            var q1 = ReadVolume();
            var q2 = ReadVolume();
            Console.WriteLine($"Ratio: {q1.Divide(q2)}");
            Pause();
        }

        // ======================================================
        // TEMPERATURE MENU (UC14)
        // ======================================================

        private void ShowTemperatureMenu()
        {
            while (true)
            {
               
                Console.WriteLine("=== TEMPERATURE MENU ===");
                Console.WriteLine("1. Equality");
                Console.WriteLine("2. Conversion");
                Console.WriteLine("3. Back");

                switch (Console.ReadLine())
                {
                    case "1": HandleTemperatureEquality(); break;
                    case "2": HandleTemperatureConversion(); break;
                    case "3": return;
                    default: Invalid(); break;
                }
            }
        }

        private void HandleTemperatureEquality()
        {
            double v1 = ReadValue("Enter first value: ");
            TemperatureUnit u1 = ReadTemperatureUnit();
            double v2 = ReadValue("Enter second value: ");
            TemperatureUnit u2 = ReadTemperatureUnit();

            bool result = _temperatureService.ValidateEquality(v1, u1, v2, u2);
            Console.WriteLine($"Equal: {result}");
            Pause();
        }

        private void HandleTemperatureConversion()
        {
            double value = ReadValue("Enter value: ");
            TemperatureUnit from = ReadTemperatureUnit();

            TemperatureUnit to = ReadTemperatureUnit();

            var result = _temperatureService.Convert(value, from, to);
            Console.WriteLine($"Converted: {result}");
            Pause();
        }

        // ======================================================
        // HELPERS
        // ======================================================

        private Quantity<LengthUnit> ReadLength()
            => new Quantity<LengthUnit>(ReadValue("Enter value: "), ReadLengthUnit());

        private Quantity<WeightUnit> ReadWeight()
            => new Quantity<WeightUnit>(ReadValue("Enter value: "), ReadWeightUnit());

        private Quantity<VolumeUnit> ReadVolume()
            => new Quantity<VolumeUnit>(ReadValue("Enter value: "), ReadVolumeUnit());

        private double ReadValue(string message)
        {
            Console.Write(message);
            if (!double.TryParse(Console.ReadLine(), out double value))
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

        private TemperatureUnit ReadTemperatureUnit()
        {
            Console.WriteLine("1. Celsius");
            Console.WriteLine("2. Fahrenheit");
            Console.WriteLine("3. Kelvin");

            return Console.ReadLine() switch
            {
                "1" => TemperatureUnit.CELSIUS,
                "2" => TemperatureUnit.FAHRENHEIT,
                "3" => TemperatureUnit.KELVIN,
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