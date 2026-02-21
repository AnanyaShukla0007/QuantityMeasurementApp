using System;

namespace QuantityMeasurementApp.Models
{
    /// <summary>
    /// Defines supported length units and their conversion factors relative to Feet (base unit).
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
        // Returns conversion factor relative to Feet
        public static double ToFeetFactor(this LengthUnit unit)
        {
            return unit switch
            {
                // 1 Foot = 1 Foot
                LengthUnit.Feet => 1.0,

                // 1 Inch = 1/12 Foot
                LengthUnit.Inches => 1.0 / 12.0,

                // 1 Yard = 3 Feet
                LengthUnit.Yards => 3.0,

                // 1 cm = 0.393701 inch → convert inch to feet
                LengthUnit.Centimeters => 0.393701 / 12.0,

                // Reject unsupported units
                _ => throw new ArgumentException("Unsupported length unit.")
            };
        }
    }
}