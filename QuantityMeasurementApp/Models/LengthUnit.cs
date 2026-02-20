using System;

namespace QuantityMeasurementApp.Models
{
    public enum LengthUnit
    {
        Feet,
        Inches,
        Yards,
        Centimeters
    }

    public static class LengthUnitExtensions
    {
        /// <summary>
        /// Converts unit to base unit (Feet).
        /// </summary>
        public static double ToFeetFactor(this LengthUnit unit)
        {
            return unit switch
            {
                LengthUnit.Feet => 1.0,
                LengthUnit.Inches => 1.0 / 12.0,
                LengthUnit.Yards => 3.0,
                LengthUnit.Centimeters => 0.393701 / 12.0,
                _ => throw new ArgumentException("Unsupported length unit.")
            };
        }
    }
}