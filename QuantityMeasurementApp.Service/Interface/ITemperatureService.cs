namespace QuantityMeasurementApp.Service.Interfaces
{
    public interface ITemperatureService
    {
        double Convert(double value, string from, string to);
        bool Equal(double v1, string u1, double v2, string u2);
    }
}