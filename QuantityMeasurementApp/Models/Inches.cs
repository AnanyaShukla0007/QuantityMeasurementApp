using System;

namespace QuantityMeasurementApp.Models
{
    /// <summary>
    /// Represents a value object for Inches measurement and provides equality comparison logic.
    /// </summary>
    public sealed class Inches
    {
        // Stores numeric value in inches
        public double Value { get; }

        // Constructor initializes measurement value
        public Inches(double value)
        {
            // Validate numeric input
            if (double.IsNaN(value) || double.IsInfinity(value))
                throw new ArgumentException("Invalid numeric value.");

            // Assign validated value
            Value = value;
        }

        public override bool Equals(object? obj)
        {
            // Check reflexive property
            if (ReferenceEquals(this, obj))
                return true;

            // Ensure object is not null and type matches
            if (obj is not Inches other)
                return false;

            // Compare stored numeric values
            return Value.Equals(other.Value);
        }

        public override int GetHashCode()
        {
            // Generate hash code based on Value
            return Value.GetHashCode();
        }
    }
}