using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Service.Mappers
{
    public static class WeightUnitMapper
    {
        public static WeightUnit Map(string unit)
        {
            return unit.ToLower() switch
            {
                "gram" => WeightUnit.Gram,
                "kilogram" => WeightUnit.Kilogram,
                "pound" => WeightUnit.Pound,
                _ => throw new ArgumentException("Invalid Weight unit")
            };
        }
    }
}