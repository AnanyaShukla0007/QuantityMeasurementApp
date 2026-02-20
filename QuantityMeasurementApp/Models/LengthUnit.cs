namespace QuantityMeasurementApp.Models
{
    /// <summary>
    /// Supported length units with conversion factor relative to Feet (base unit).
    /// </summary>
    public enum LengthUnit
    {
        Feet,
        Inches
    }

    public static class LengthUnitExtensions
    {
        /// <summary>
        /// Returns conversion factor relative to base unit (Feet).
        /// </summary>
        public static double ToFeetFactor(this LengthUnit unit)
        {
            return unit switch
            {
                LengthUnit.Feet => 1.0,
                LengthUnit.Inches => 1.0 / 12.0,
                _ => throw new ArgumentException("Unsupported length unit.")
            };
        }
    }
}