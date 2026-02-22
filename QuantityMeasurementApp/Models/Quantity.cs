using System;

namespace QuantityMeasurementApp.Models
{
    public sealed class Quantity<TUnit>
        where TUnit : struct, Enum
    {
        private const double Epsilon = 1e-6;

        public double Value { get; }
        public TUnit Unit { get; }

        public Quantity(double value, TUnit unit)
        {
            if (!double.IsFinite(value))
                throw new ArgumentException("Invalid numeric value.");

            Value = value;
            Unit = unit;
        }

        private double ToBaseUnit()
        {
            dynamic u = Unit;
            return u.ConvertToBaseUnit(Value);
        }

        public Quantity<TUnit> ConvertTo(TUnit target)
        {
            dynamic source = Unit;
            dynamic targetUnit = target;

            double baseValue = source.ConvertToBaseUnit(Value);
            double converted = targetUnit.ConvertFromBaseUnit(baseValue);

            return new Quantity<TUnit>(converted, target);
        }

        public Quantity<TUnit> Add(Quantity<TUnit> other)
        {
            return Add(other, this.Unit);
        }

        public Quantity<TUnit> Add(Quantity<TUnit> other, TUnit target)
        {
            dynamic t = target;

            double baseSum = this.ToBaseUnit() + other.ToBaseUnit();
            double converted = t.ConvertFromBaseUnit(baseSum);

            return new Quantity<TUnit>(converted, target);
        }

        public override bool Equals(object obj)
        {
            if (obj is not Quantity<TUnit> other)
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