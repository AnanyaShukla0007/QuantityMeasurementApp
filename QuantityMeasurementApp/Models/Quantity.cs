using System;
using QuantityMeasurementApp.Interface;
using QuantityMeasurementApp.Utilities;

namespace QuantityMeasurementApp.Models
{
    public sealed class Quantity<U> where U : IMeasurable
    {
        private const double Epsilon = 1e-6;

        public double Value { get; }
        public U Unit { get; }

        public Quantity(double value, U unit)
        {
            if (!double.IsFinite(value))
                throw new ArgumentException("Invalid numeric value.");

            Unit = unit ?? throw new ArgumentException("Unit cannot be null.");
            Value = value;
        }

        private enum ArithmeticOperation
        {
            ADD,
            SUBTRACT,
            DIVIDE
        }

        private void ValidateArithmeticOperands(
            Quantity<U> other,
            U targetUnit,
            bool targetRequired)
        {
            if (other is null)
                throw new ArgumentException("Operand cannot be null.");

            if (Unit.GetType() != other.Unit.GetType())
                throw new ArgumentException("Cross-category operation not allowed.");

            if (targetRequired && targetUnit == null)
                throw new ArgumentException("Target unit cannot be null.");
        }

        private double PerformBaseArithmetic(
            Quantity<U> other,
            ArithmeticOperation operation)
        {
            // UC14 enforcement
            Unit.ValidateOperationSupport(operation.ToString());
            other.Unit.ValidateOperationSupport(operation.ToString());

            double leftBase = Unit.ConvertToBaseUnit(Value);
            double rightBase = other.Unit.ConvertToBaseUnit(other.Value);

            return operation switch
            {
                ArithmeticOperation.ADD => leftBase + rightBase,
                ArithmeticOperation.SUBTRACT => leftBase - rightBase,
                ArithmeticOperation.DIVIDE =>
                    Math.Abs(rightBase) < Epsilon
                        ? throw new ArithmeticException("Cannot divide by zero.")
                        : leftBase / rightBase,
                _ => throw new InvalidOperationException()
            };
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Quantity<U> other)
                return false;

            if (Unit.GetType() != other.Unit.GetType())
                return false;

            double left = Unit.ConvertToBaseUnit(Value);
            double right = other.Unit.ConvertToBaseUnit(other.Value);

            return Math.Abs(left - right) < Epsilon;
        }

        public override int GetHashCode()
            => Unit.ConvertToBaseUnit(Value).GetHashCode();

        public Quantity<U> ConvertTo(U targetUnit)
        {
            if (targetUnit == null)
                throw new ArgumentException("Target unit cannot be null.");

            double baseValue = Unit.ConvertToBaseUnit(Value);
            double converted = targetUnit.ConvertFromBaseUnit(baseValue);

            return new Quantity<U>(
                RoundingHelper.Round(converted),
                targetUnit);
        }

        public Quantity<U> Add(Quantity<U> other)
        {
            ValidateArithmeticOperands(other, default!, false);
            double baseResult = PerformBaseArithmetic(other, ArithmeticOperation.ADD);
            double converted = Unit.ConvertFromBaseUnit(baseResult);
            return new Quantity<U>(RoundingHelper.Round(converted), Unit);
        }

        public Quantity<U> Subtract(Quantity<U> other)
        {
            ValidateArithmeticOperands(other, default!, false);
            double baseResult = PerformBaseArithmetic(other, ArithmeticOperation.SUBTRACT);
            double converted = Unit.ConvertFromBaseUnit(baseResult);
            return new Quantity<U>(RoundingHelper.Round(converted), Unit);
        }

        public double Divide(Quantity<U> other)
        {
            ValidateArithmeticOperands(other, default!, false);
            return PerformBaseArithmetic(other, ArithmeticOperation.DIVIDE);
        }

        public override string ToString()
            => $"Quantity({Value}, {Unit.GetUnitName()})";
    }
}