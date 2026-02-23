namespace QuantityMeasurementApp.Interface
{
    public interface IMeasurable
    {
        double ConvertToBaseUnit(double value);
        double ConvertFromBaseUnit(double baseValue);
        string GetUnitName();

        // UC14 – Optional arithmetic support
        bool SupportsArithmetic();
        void ValidateOperationSupport(string operation);
    }
}