using System;
using QuantityMeasurementApp.Models;
using QuantityMeasurementApp.Services;

namespace QuantityMeasurementApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var service = new QuantityMeasurementService();

            bool result1 = service.ValidateLengthEquality(1.0, LengthUnit.Feet,
                                                          12.0, LengthUnit.Inches);

            bool result2 = service.ValidateLengthEquality(1.0, LengthUnit.Inches,
                                                          1.0, LengthUnit.Inches);

            Console.WriteLine("Input: Quantity(1.0, Feet) and Quantity(12.0, Inches)");
            Console.WriteLine($"Output: Equal ({result1})");

            Console.WriteLine();

            Console.WriteLine("Input: Quantity(1.0, Inches) and Quantity(1.0, Inches)");
            Console.WriteLine($"Output: Equal ({result2})");
        }
    }
}