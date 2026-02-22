using System;

namespace QuantityMeasurementApp.Models
{
    public enum LengthUnit : byte
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

        public static double ConvertToBaseUnit(this LengthUnit unit, double value)
            => value * unit.ConversionFactor();

        public static double ConvertFromBaseUnit(this LengthUnit unit, double baseValue)
            => baseValue / unit.ConversionFactor();

        public static string UnitName(this LengthUnit unit)
            => unit.ToString();
    }
}