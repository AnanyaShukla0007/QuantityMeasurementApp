using System;

namespace QuantityMeasurementApp.Models
{
    /// <summary>
    /// Immutable value object supporting equality, conversion (UC5), and addition (UC6).
    /// </summary>
    public sealed class QuantityLength
    {
        private const double Epsilon = 1e-6;

        public double Value { get; }

        public LengthUnit Unit { get; }

        public QuantityLength(double value, LengthUnit unit)
        {
            if (!double.IsFinite(value))
                throw new ArgumentException("Invalid numeric value.");

            Value = value;
            Unit = unit;
        }

        private double ToBaseUnit()
        {
            return Value * Unit.ConversionFactor();
        }

        public QuantityLength ConvertTo(LengthUnit targetUnit)
        {
            double baseValue = ToBaseUnit();
            double converted = baseValue / targetUnit.ConversionFactor();
            return new QuantityLength(converted, targetUnit);
        }

        public static double Convert(double value, LengthUnit from, LengthUnit to)
        {
            if (!double.IsFinite(value))
                throw new ArgumentException("Invalid numeric value.");

            double baseValue = value * from.ConversionFactor();
            return baseValue / to.ConversionFactor();
        }

        // ✅ Instance addition (UC6)
        public QuantityLength Add(QuantityLength other)
        {
            if (other is null)
                throw new ArgumentException("Second operand cannot be null.");

            double sumBase = this.ToBaseUnit() + other.ToBaseUnit();
            double resultValue = sumBase / this.Unit.ConversionFactor();

            return new QuantityLength(resultValue, this.Unit);
        }

        // ✅ Static addition (used by Service)
        public static QuantityLength Add(QuantityLength first, QuantityLength second)
        {
            if (first is null || second is null)
                throw new ArgumentException("Operands cannot be null.");

            return first.Add(second);
        }

        public override bool Equals(object obj)
        {
            if (obj is not QuantityLength other)
                return false;

            return Math.Abs(this.ToBaseUnit() - other.ToBaseUnit()) < Epsilon;
        }

        public override int GetHashCode()
        {
            return ToBaseUnit().GetHashCode();
        }

        public override string ToString()
        {
            return $"Quantity({Value}, {Unit})";
        }
    }
}