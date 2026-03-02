namespace QuantityMeasurementApp.Model.DTOs
{
    public class ArithmeticRequestDto
    {
        public double Value1 { get; set; }
        public string Unit1 { get; set; } = string.Empty;

        public double Value2 { get; set; }
        public string Unit2 { get; set; } = string.Empty;
    }
}