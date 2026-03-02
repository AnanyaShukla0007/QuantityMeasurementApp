namespace QuantityMeasurementApp.Service.Interfaces
{
    public interface ILengthService
    {
        double Convert(double value, string from, string to);
        bool Equal(double v1, string u1, double v2, string u2);
        double Add(double v1, string u1, double v2, string u2);
        double Subtract(double v1, string u1, double v2, string u2);
        double Divide(double v1, string u1, double v2, string u2);
    }
}