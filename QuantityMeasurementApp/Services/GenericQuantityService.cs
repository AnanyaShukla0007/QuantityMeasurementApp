using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Services
{
    public class GenericQuantityService
    {
        public bool ValidateEquality<U>(double v1, U u1, double v2, U u2)
            where U : struct, Enum
        {
            return new Quantity<U>(v1, u1)
                .Equals(new Quantity<U>(v2, u2));
        }

        public Quantity<U> Convert<U>(double value, U from, U to)
            where U : struct, Enum
        {
            return new Quantity<U>(value, from).ConvertTo(to);
        }

        public Quantity<U> Add<U>(Quantity<U> q1, Quantity<U> q2)
            where U : struct, Enum
        {
            return q1.Add(q2);
        }

        public Quantity<U> Subtract<U>(Quantity<U> q1, Quantity<U> q2)
            where U : struct, Enum
        {
            return q1.Subtract(q2);
        }

        public double Divide<U>(Quantity<U> q1, Quantity<U> q2)
            where U : struct, Enum
        {
            return q1.Divide(q2);
        }
    }
}