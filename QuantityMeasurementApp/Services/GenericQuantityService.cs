using System;
using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Services
{
    /// <summary>
    /// UC10: Generic service layer for Quantity<TUnit>.
    /// Works for any enum unit type that supports conversion extensions.
    /// Preserves SRP and keeps controller/menu thin.
    /// </summary>
    public class GenericQuantityService
    {
        // ---------------- GENERIC EQUALITY ----------------

        public bool ValidateEquality<TUnit>(
            double v1, TUnit u1,
            double v2, TUnit u2)
            where TUnit : struct, Enum
        {
            var q1 = new Quantity<TUnit>(v1, u1);
            var q2 = new Quantity<TUnit>(v2, u2);

            return q1.Equals(q2);
        }

        // ---------------- GENERIC CONVERSION ----------------

        public Quantity<TUnit> Convert<TUnit>(
            double value,
            TUnit from,
            TUnit to)
            where TUnit : struct, Enum
        {
            var quantity = new Quantity<TUnit>(value, from);
            return quantity.ConvertTo(to);
        }

        // ---------------- GENERIC ADDITION (DEFAULT) ----------------

        public Quantity<TUnit> Add<TUnit>(
            Quantity<TUnit> first,
            Quantity<TUnit> second)
            where TUnit : struct, Enum
        {
            if (first is null || second is null)
                throw new ArgumentException("Operands cannot be null.");

            return first.Add(second);
        }

        // ---------------- GENERIC ADDITION (EXPLICIT TARGET) ----------------

        public Quantity<TUnit> Add<TUnit>(
            Quantity<TUnit> first,
            Quantity<TUnit> second,
            TUnit target)
            where TUnit : struct, Enum
        {
            if (first is null || second is null)
                throw new ArgumentException("Operands cannot be null.");

            return first.Add(second, target);
        }
    }
}