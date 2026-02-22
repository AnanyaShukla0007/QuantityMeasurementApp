using System;

namespace QuantityMeasurementApp.Models
{
    public enum WeightUnit : byte
    {
        Kilogram,
        Gram,
        Pound
    }

    public static class WeightUnitExtensions
    {
        public static double ConversionFactor(this WeightUnit unit)
        {
            return unit switch
            {
                WeightUnit.Kilogram => 1.0,
                WeightUnit.Gram => 0.001,
                WeightUnit.Pound => 0.453592,
                _ => throw new ArgumentException("Unsupported unit.")
            };
        }

        public static double ConvertToBaseUnit(this WeightUnit unit, double value)
            => value * unit.ConversionFactor();

        public static double ConvertFromBaseUnit(this WeightUnit unit, double baseValue)
            => baseValue / unit.ConversionFactor();

        public static string UnitName(this WeightUnit unit)
            => unit.ToString();
    }
}