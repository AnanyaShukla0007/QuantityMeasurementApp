using System;
using QuantityMeasurementApp.Interface;
using QuantityMeasurementApp.Utilities;

namespace QuantityMeasurementApp.Models
{
    public sealed class Quantity<U> where U : struct, Enum
    {
        private const double Epsilon = 1e-6;

        public double Value { get; }
        public U Unit { get; }

        public Quantity(double value, U unit)
        {
            if (!double.IsFinite(value))
                throw new ArgumentException("Invalid numeric value.");

            Value = value;
            Unit = unit;
        }

        private double ToBase()
        {
            dynamic unit = Unit;
            return unit.ConvertToBaseUnit(Value);
        }

        private double FromBase(double baseValue, U target)
        {
            dynamic unit = target;
            return unit.ConvertFromBaseUnit(baseValue);
        }

        private static void ValidateOperands(Quantity<U> first, Quantity<U> second)
        {
            if (first is null || second is null)
                throw new ArgumentException("Operands cannot be null.");
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Quantity<U> other)
                return false;

            return Math.Abs(ToBase() - other.ToBase()) < Epsilon;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Math.Round(ToBase(), 6));
        }

        public Quantity<U> ConvertTo(U targetUnit)
        {
            double converted = FromBase(ToBase(), targetUnit);
            return new Quantity<U>(RoundingHelper.Round(converted), targetUnit);
        }

        public Quantity<U> Add(Quantity<U> other)
        {
            ValidateOperands(this, other);

            double baseSum = ToBase() + other.ToBase();
            double converted = FromBase(baseSum, Unit);

            return new Quantity<U>(RoundingHelper.Round(converted), Unit);
        }

        public Quantity<U> Add(Quantity<U> other, U targetUnit)
        {
            ValidateOperands(this, other);

            double baseSum = ToBase() + other.ToBase();
            double converted = FromBase(baseSum, targetUnit);

            return new Quantity<U>(RoundingHelper.Round(converted), targetUnit);
        }

        public Quantity<U> Subtract(Quantity<U> other)
        {
            ValidateOperands(this, other);

            double baseDiff = ToBase() - other.ToBase();
            double converted = FromBase(baseDiff, Unit);

            return new Quantity<U>(RoundingHelper.Round(converted), Unit);
        }

        public Quantity<U> Subtract(Quantity<U> other, U targetUnit)
        {
            ValidateOperands(this, other);

            double baseDiff = ToBase() - other.ToBase();
            double converted = FromBase(baseDiff, targetUnit);

            return new Quantity<U>(RoundingHelper.Round(converted), targetUnit);
        }

        public double Divide(Quantity<U> other)
        {
            ValidateOperands(this, other);

            double divisor = other.ToBase();

            if (Math.Abs(divisor) < Epsilon)
                throw new ArithmeticException("Cannot divide by zero.");

            return ToBase() / divisor;
        }

        public override string ToString()
        {
            return $"Quantity({Value}, {Unit})";
        }
    }
}