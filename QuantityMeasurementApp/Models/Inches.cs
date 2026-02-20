namespace QuantityMeasurementApp.Models
{
    /// <summary>
    /// Represents a measurement in Inches.
    /// Immutable value object.
    /// </summary>
    public sealed class Inches
    {
        public double TotalUnits { get; }

        public Inches(double totalUnits)
        {
            TotalUnits = totalUnits;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            if (obj is not Inches other)
                return false;

            return TotalUnits.Equals(other.TotalUnits);
        }

        public override int GetHashCode()
        {
            return TotalUnits.GetHashCode();
        }
    }
}