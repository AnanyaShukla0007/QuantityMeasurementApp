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

            Console.WriteLine(service.ValidateLengthEquality(1.0, LengthUnit.Yards,
                                                            3.0, LengthUnit.Feet));

            Console.WriteLine(service.ValidateLengthEquality(1.0, LengthUnit.Yards,
                                                            36.0, LengthUnit.Inches));

            Console.WriteLine(service.ValidateLengthEquality(1.0, LengthUnit.Centimeters,
                                                            0.393701, LengthUnit.Inches));

            Console.WriteLine(service.ValidateLengthEquality(2.0, LengthUnit.Yards,
                                                            6.0, LengthUnit.Feet));
        }
    }
}