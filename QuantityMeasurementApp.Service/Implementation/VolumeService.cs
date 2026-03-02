using QuantityMeasurementApp.Models;
using QuantityMeasurementApp.Service.Interfaces;
using QuantityMeasurementApp.Service.Mappers;

namespace QuantityMeasurementApp.Service.Implementations
{
    public class VolumeService : IVolumeService
    {
        public double Convert(double value, string from, string to)
        {
            var fromUnit = VolumeUnitMapper.Map(from);
            var toUnit = VolumeUnitMapper.Map(to);

            var quantity = new Quantity<VolumeUnit>(value, fromUnit);
            return quantity.ConvertTo(toUnit).Value;
        }

        public bool Equal(double v1, string u1, double v2, string u2)
        {
            var q1 = new Quantity<VolumeUnit>(v1, VolumeUnitMapper.Map(u1));
            var q2 = new Quantity<VolumeUnit>(v2, VolumeUnitMapper.Map(u2));
            return q1.Equals(q2);
        }

        public double Add(double v1, string u1, double v2, string u2)
        {
            var q1 = new Quantity<VolumeUnit>(v1, VolumeUnitMapper.Map(u1));
            var q2 = new Quantity<VolumeUnit>(v2, VolumeUnitMapper.Map(u2));
            return q1.Add(q2).Value;
        }

        public double Subtract(double v1, string u1, double v2, string u2)
        {
            var q1 = new Quantity<VolumeUnit>(v1, VolumeUnitMapper.Map(u1));
            var q2 = new Quantity<VolumeUnit>(v2, VolumeUnitMapper.Map(u2));
            return q1.Subtract(q2).Value;
        }

        public double Divide(double v1, string u1, double v2, string u2)
        {
            var q1 = new Quantity<VolumeUnit>(v1, VolumeUnitMapper.Map(u1));
            var q2 = new Quantity<VolumeUnit>(v2, VolumeUnitMapper.Map(u2));
            return q1.Divide(q2);
        }
    }
}