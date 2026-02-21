using System;

namespace QuantityMeasurementApp.Models
{
    /// <summary>
    /// Defines supported length units and provides conversion factors relative to base unit (Feet).
    /// </summary>
    public enum LengthUnit
    {
        Feet,
        Inches,
        Yards,
        Centimeters
    }

    public static class LengthUnitExtensions
    {
        public static double ConversionFactor(this LengthUnit unit)
        {
            return unit switch
            {
                LengthUnit.Feet => 1.0,
                LengthUnit.Inches => 1.0 / 12.0,
                LengthUnit.Yards => 3.0,
                LengthUnit.Centimeters => 0.0328084,
                _ => throw new ArgumentException("Unsupported unit.")
            };
        }
    }
}