using System;

namespace QuantityMeasurementApp.Models
{
    /// <summary>
    /// Represents a measurement in feet.
    /// Immutable value object.
    /// </summary>
    public sealed class Feet
    {
        public double Value { get; }

        public Feet(double value)
        {
            if (double.IsNaN(value) || double.IsInfinity(value))
                throw new ArgumentException("Invalid numeric value.");

            Value = value;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            if (obj is not Feet other)
                return false;

            return Value.Equals(other.Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}