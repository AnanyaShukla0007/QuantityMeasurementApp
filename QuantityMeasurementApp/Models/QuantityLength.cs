using System;

namespace QuantityMeasurementApp.Models
{
    /// <summary>
    /// Represents a generic length measurement and provides cross-unit equality comparison.
    /// </summary>
    public sealed class QuantityLength
    {
        // Stores numeric value
        public double Value { get; }

        // Stores associated unit
        public LengthUnit Unit { get; }

        // Constructor initializes value and unit
        public QuantityLength(double value, LengthUnit unit)
        {
            if (double.IsNaN(value) || double.IsInfinity(value))
                throw new ArgumentException("Invalid numeric value.");

            Value = value;
            Unit = unit;
        }

        // Converts value to base unit (Feet)
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

            return Math.Abs(ConvertToFeet() - other.ConvertToFeet()) < 0.0001;
        }

        public override int GetHashCode()
        {
            return ConvertToFeet().GetHashCode();
        }
    }
}