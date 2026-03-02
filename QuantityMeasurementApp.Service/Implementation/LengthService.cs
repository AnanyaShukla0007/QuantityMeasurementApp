using QuantityMeasurementApp.Models;
using QuantityMeasurementApp.Service.Interfaces;
using QuantityMeasurementApp.Service.Mappers;

namespace QuantityMeasurementApp.Service.Implementations
{
    public class LengthService : ILengthService
    {
        public double Convert(double value, string from, string to)
        {
            var fromUnit = LengthUnitMapper.Map(from);
            var toUnit = LengthUnitMapper.Map(to);

            var quantity = new Quantity<LengthUnit>(value, fromUnit);
            return quantity.ConvertTo(toUnit).Value;
        }

        public bool Equal(double v1, string u1, double v2, string u2)
        {
            var q1 = new Quantity<LengthUnit>(v1, LengthUnitMapper.Map(u1));
            var q2 = new Quantity<LengthUnit>(v2, LengthUnitMapper.Map(u2));
            return q1.Equals(q2);
        }

        public double Add(double v1, string u1, double v2, string u2)
        {
            var q1 = new Quantity<LengthUnit>(v1, LengthUnitMapper.Map(u1));
            var q2 = new Quantity<LengthUnit>(v2, LengthUnitMapper.Map(u2));
            return q1.Add(q2).Value;
        }

        public double Subtract(double v1, string u1, double v2, string u2)
        {
            var q1 = new Quantity<LengthUnit>(v1, LengthUnitMapper.Map(u1));
            var q2 = new Quantity<LengthUnit>(v2, LengthUnitMapper.Map(u2));
            return q1.Subtract(q2).Value;
        }

        public double Divide(double v1, string u1, double v2, string u2)
        {
            var q1 = new Quantity<LengthUnit>(v1, LengthUnitMapper.Map(u1));
            var q2 = new Quantity<LengthUnit>(v2, LengthUnitMapper.Map(u2));
            return q1.Divide(q2);
        }
    }
}