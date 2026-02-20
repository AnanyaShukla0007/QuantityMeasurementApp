using System;
using QuantityMeasurementApp.Services;

namespace QuantityMeasurementApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var measurementService = new QuantityMeasurementService();

            bool inchResult = measurementService.ValidateInchesEquality(1.0, 1.0);
            bool feetResult = measurementService.ValidateFeetEquality(1.0, 1.0);

            Console.WriteLine("Input: 1.0 inch and 1.0 inch");
            Console.WriteLine($"Output: Equal ({inchResult})");

            Console.WriteLine();

            Console.WriteLine("Input: 1.0 ft and 1.0 ft");
            Console.WriteLine($"Output: Equal ({feetResult})");
        }
    }
}