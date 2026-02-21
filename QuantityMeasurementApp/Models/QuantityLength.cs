using System;

namespace QuantityMeasurementApp.Models
{
    /// <summary>
    /// Represents an immutable length value object supporting equality and unit conversion.
    /// </summary>
    public sealed class QuantityLength
    {
        private const double Epsilon = 1e-6;

        public double Value { get; }

        public LengthUnit Unit { get; }

        public QuantityLength(double value, LengthUnit unit)
        {
            if (!double.IsFinite(value))
                throw new ArgumentException("Value must be finite.");

            Value = value;
            Unit = unit;
        }

        // Converts instance to another unit and returns new QuantityLength
        public QuantityLength ConvertTo(LengthUnit targetUnit)
        {
            double converted = Convert(Value, Unit, targetUnit);
            return new QuantityLength(converted, targetUnit);
        }

        // Static conversion API
        public static double Convert(double value, LengthUnit source, LengthUnit target)
        {
            if (!double.IsFinite(value))
                throw new ArgumentException("Value must be finite.");

            // Normalize to base unit (Feet)
            double baseValue = value * source.ToFeetFactor();

            // Convert from base unit to target
            double result = baseValue / target.ToFeetFactor();

            return result;
        }

        private double ConvertToFeet()
        {
            return Value * Unit.ToFeetFactor();
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            if (obj is not QuantityLength other)
                return false;

            return Math.Abs(ConvertToFeet() - other.ConvertToFeet()) < Epsilon;
        }

        public override int GetHashCode()
        {
            return ConvertToFeet().GetHashCode();
        }

        public override string ToString()
        {
            return $"{Value} {Unit}";
        }
    }
}