using QuantityMeasurementApp.Models;
using QuantityMeasurementApp.Service.Interfaces;
using QuantityMeasurementApp.Service.Mappers;

namespace QuantityMeasurementApp.Service.Implementations
{
    public class WeightService : IWeightService
    {
        public double Convert(double value, string from, string to)
        {
            var fromUnit = WeightUnitMapper.Map(from);
            var toUnit = WeightUnitMapper.Map(to);

            var quantity = new Quantity<WeightUnit>(value, fromUnit);
            return quantity.ConvertTo(toUnit).Value;
        }

        public bool Equal(double v1, string u1, double v2, string u2)
        {
            var q1 = new Quantity<WeightUnit>(v1, WeightUnitMapper.Map(u1));
            var q2 = new Quantity<WeightUnit>(v2, WeightUnitMapper.Map(u2));
            return q1.Equals(q2);
        }

        public double Add(double v1, string u1, double v2, string u2)
        {
            var q1 = new Quantity<WeightUnit>(v1, WeightUnitMapper.Map(u1));
            var q2 = new Quantity<WeightUnit>(v2, WeightUnitMapper.Map(u2));
            return q1.Add(q2).Value;
        }

        public double Subtract(double v1, string u1, double v2, string u2)
        {
            var q1 = new Quantity<WeightUnit>(v1, WeightUnitMapper.Map(u1));
            var q2 = new Quantity<WeightUnit>(v2, WeightUnitMapper.Map(u2));
            return q1.Subtract(q2).Value;
        }

        public double Divide(double v1, string u1, double v2, string u2)
        {
            var q1 = new Quantity<WeightUnit>(v1, WeightUnitMapper.Map(u1));
            var q2 = new Quantity<WeightUnit>(v2, WeightUnitMapper.Map(u2));
            return q1.Divide(q2);
        }
    }
}