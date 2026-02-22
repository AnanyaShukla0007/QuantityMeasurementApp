using System;
using QuantityMeasurementApp.Utilities;

namespace QuantityMeasurementApp.Models
{
    /// <summary>
    /// Immutable length value object.
    /// Conversion responsibility delegated to LengthUnit.
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

        private double ToBaseUnit()
        {
            return Unit.ConvertToBaseUnit(Value);
        }

        // ---------------- CONVERSION ----------------

        public QuantityLength ConvertTo(LengthUnit targetUnit)
        {
            double baseValue = ToBaseUnit();
            double converted = targetUnit.ConvertFromBaseUnit(baseValue);

            return new QuantityLength(
                RoundingHelper.RoundToTwoDecimals(converted),
                targetUnit);
        }

        public static double Convert(double value,
                                     LengthUnit from,
                                     LengthUnit to)
        {
            if (!double.IsFinite(value))
                throw new ArgumentException("Invalid numeric value.");

            double baseValue = from.ConvertToBaseUnit(value);
            double converted = to.ConvertFromBaseUnit(baseValue);

            return RoundingHelper.RoundToTwoDecimals(converted);
        }

        // ---------------- ADDITION ----------------

        private static QuantityLength AddInternal(
            QuantityLength first,
            QuantityLength second,
            LengthUnit targetUnit)
        {
            double baseSum =
                first.ToBaseUnit() +
                second.ToBaseUnit();

            double converted =
                targetUnit.ConvertFromBaseUnit(baseSum);

            return new QuantityLength(
                RoundingHelper.RoundToTwoDecimals(converted),
                targetUnit);
        }

        public static QuantityLength Add(
            QuantityLength first,
            QuantityLength second)
        {
            if (first is null || second is null)
                throw new ArgumentException("Operands cannot be null.");

            return AddInternal(first, second, first.Unit);
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