using System;
using QuantityMeasurementApp.Models;
using QuantityMeasurementApp.Services;

namespace QuantityMeasurementApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var firstMeasurement = new Feet(1.0);
            var secondMeasurement = new Feet(1.0);

            var service = new QuantityMeasurementService();

            bool result = service.ConvertUnits(firstMeasurement, secondMeasurement);

            Console.WriteLine("Input: 1.0 ft and 1.0 ft");
            Console.WriteLine($"Output: Equal ({result})");
        }
    }
}