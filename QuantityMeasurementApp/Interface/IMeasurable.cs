namespace QuantityMeasurementApp.Interface
{
    public interface IMeasurable
    {
        double ConversionFactor { get; }

        double ConvertToBaseUnit(double value);

        double ConvertFromBaseUnit(double baseValue);

        string UnitName { get; }
    }
}