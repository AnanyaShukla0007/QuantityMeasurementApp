using System;
using QuantityMeasurementApp.Utilities;

namespace QuantityMeasurementApp.Models
{
    /// <summary>
    /// Immutable weight value object.
    /// </summary>
    public sealed class QuantityWeight
    {
        private const double Epsilon = 1e-6;

        public double Value { get; }
        public WeightUnit Unit { get; }

        public QuantityWeight(double value, WeightUnit unit)
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

        public QuantityWeight ConvertTo(WeightUnit targetUnit)
        {
            double baseValue = ToBaseUnit();
            double converted = targetUnit.ConvertFromBaseUnit(baseValue);

            return new QuantityWeight(
                RoundingHelper.RoundToTwoDecimals(converted),
                targetUnit);
        }

        public static double Convert(double value,
                                     WeightUnit from,
                                     WeightUnit to)
        {
            if (!double.IsFinite(value))
                throw new ArgumentException("Invalid numeric value.");

            double baseValue = from.ConvertToBaseUnit(value);
            double converted = to.ConvertFromBaseUnit(baseValue);

            return RoundingHelper.RoundToTwoDecimals(converted);
        }

        // ---------------- ADDITION ----------------

        private static QuantityWeight AddInternal(
            QuantityWeight first,
            QuantityWeight second,
            WeightUnit targetUnit)
        {
            double baseSum =
                first.ToBaseUnit() +
                second.ToBaseUnit();

            double converted =
                targetUnit.ConvertFromBaseUnit(baseSum);

            return new QuantityWeight(
                RoundingHelper.RoundToTwoDecimals(converted),
                targetUnit);
        }

        public static QuantityWeight Add(
            QuantityWeight first,
            QuantityWeight second)
        {
            if (first is null || second is null)
                throw new ArgumentException("Operands cannot be null.");

            return AddInternal(first, second, first.Unit);
        }

        public static QuantityWeight Add(
            QuantityWeight first,
            QuantityWeight second,
            WeightUnit targetUnit)
        {
            if (first is null || second is null)
                throw new ArgumentException("Operands cannot be null.");

            return AddInternal(first, second, targetUnit);
        }

        // ---------------- EQUALITY ----------------

        public override bool Equals(object obj)
        {
            if (obj is not QuantityWeight other)
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