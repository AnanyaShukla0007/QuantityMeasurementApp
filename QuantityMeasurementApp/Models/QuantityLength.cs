using System;

namespace QuantityMeasurementApp.Models
{
    /// <summary>
    /// Represents a length quantity with value and unit.
    /// Eliminates duplication using DRY principle.
    /// </summary>
    public sealed class QuantityLength
    {
        public double Value { get; }
        public LengthUnit Unit { get; }

        public QuantityLength(double value, LengthUnit unit)
        {
            if (double.IsNaN(value) || double.IsInfinity(value))
                throw new ArgumentException("Invalid numeric value.");

            Value = value;
            Unit = unit;
        }

        /// <summary>
        /// Converts quantity to base unit (Feet).
        /// </summary>
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