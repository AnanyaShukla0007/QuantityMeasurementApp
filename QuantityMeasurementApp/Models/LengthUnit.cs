using System;

namespace QuantityMeasurementApp.Models
{
    /// <summary>
    /// Defines supported length units and their conversion factors relative to Feet (base unit).
    /// </summary>
    public enum LengthUnit
    {
        Feet,
        Inches
    }

    public static class LengthUnitExtensions
    {
        // Returns conversion factor relative to Feet
        public static double ToFeetFactor(this LengthUnit unit)
        {
            return unit switch
            {
                LengthUnit.Feet => 1.0,
                LengthUnit.Inches => 1.0 / 12.0,
                _ => throw new ArgumentException("Unsupported length unit.")
            };
        }
    }
}