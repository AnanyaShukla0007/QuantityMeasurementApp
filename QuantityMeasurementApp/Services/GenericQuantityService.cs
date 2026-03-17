using QuantityMeasurementApp.Models;
using QuantityMeasurementApp.Interface;

namespace QuantityMeasurementApp.Services
{
    public class GenericQuantityService
    {
        public bool ValidateEquality<U>(
            double v1, U u1,
            double v2, U u2)
            where U : IMeasurable
        {
            var q1 = new Quantity<U>(v1, u1);
            var q2 = new Quantity<U>(v2, u2);
            return q1.Equals(q2);
        }

        public Quantity<U> Convert<U>(
            double value, U from, U to)
            where U : IMeasurable
        {
            var q = new Quantity<U>(value, from);
            return q.ConvertTo(to);
        }

        public Quantity<U> Add<U>(
            Quantity<U> q1,
            Quantity<U> q2)
            where U : IMeasurable
        {
            return q1.Add(q2);
        }

        public Quantity<U> Subtract<U>(
            Quantity<U> q1,
            Quantity<U> q2)
            where U : IMeasurable
        {
            return q1.Subtract(q2);
        }

        public double Divide<U>(
            Quantity<U> q1,
            Quantity<U> q2)
            where U : IMeasurable
        {
            return q1.Divide(q2);
        }
    }
}