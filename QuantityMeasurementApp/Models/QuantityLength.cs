using System;

namespace QuantityMeasurementApp.Models
{
    /// <summary>
    /// Immutable value object supporting equality (UC1–UC4),
    /// conversion (UC5),
    /// addition with implicit target (UC6),
    /// addition with explicit target (UC7).
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

            Unit = unit;
            Value = value;
        }

        // ---------------- BASE NORMALIZATION ----------------

        private double ToBaseUnit()
        {
            return Value * Unit.ConversionFactor();
        }

        private static QuantityLength AddInternal(
            QuantityLength first,
            QuantityLength second,
            LengthUnit targetUnit)
        {
            double baseSum =
                first.ToBaseUnit() +
                second.ToBaseUnit();

            double converted =
                baseSum / targetUnit.ConversionFactor();

            return new QuantityLength(converted, targetUnit);
        }

        // ---------------- UC5 ----------------

        public QuantityLength ConvertTo(LengthUnit targetUnit)
        {
            double baseValue = ToBaseUnit();
            double converted =
                baseValue / targetUnit.ConversionFactor();

            return new QuantityLength(converted, targetUnit);
        }

        public static double Convert(double value,
                                     LengthUnit from,
                                     LengthUnit to)
        {
            if (!double.IsFinite(value))
                throw new ArgumentException("Invalid numeric value.");

            double baseValue =
                value * from.ConversionFactor();

            return baseValue / to.ConversionFactor();
        }

        // ---------------- UC6 ----------------
        // Default: result in first operand unit

        public QuantityLength Add(QuantityLength other)
        {
            if (other is null)
                throw new ArgumentException("Second operand cannot be null.");

            return AddInternal(this, other, this.Unit);
        }

        public static QuantityLength Add(
            QuantityLength first,
            QuantityLength second)
        {
            if (first is null || second is null)
                throw new ArgumentException("Operands cannot be null.");

            return AddInternal(first, second, first.Unit);
        }

        // ---------------- UC7 ----------------
        // Explicit target unit

        public QuantityLength Add(
            QuantityLength other,
            LengthUnit targetUnit)
        {
            if (other is null)
                throw new ArgumentException("Second operand cannot be null.");

            return AddInternal(this, other, targetUnit);
        }

        public static QuantityLength Add(
            QuantityLength first,
            QuantityLength second,
            LengthUnit targetUnit)
        {
            if (first is null || second is null)
                throw new ArgumentException("Operands cannot be null.");

            return AddInternal(first, second, targetUnit);
        }

        // ---------------- EQUALITY ----------------

        public override bool Equals(object obj)
        {
            if (obj is not QuantityLength other)
                return false;

            return Math.Abs(
                this.ToBaseUnit() -
                other.ToBaseUnit()) < Epsilon;
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