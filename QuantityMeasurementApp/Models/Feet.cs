namespace QuantityMeasurementApp.Models
{
    /// <summary>
    /// Represents a measurement in Feet.
    /// Immutable value object.
    /// </summary>
    public sealed class Feet
    {
        public double TotalUnits { get; }

        public Feet(double totalUnits)
        {
            TotalUnits = totalUnits;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            if (obj is not Feet other)
                return false;

            return TotalUnits.Equals(other.TotalUnits);
        }

        public override int GetHashCode()
        {
            return TotalUnits.GetHashCode();
        }
    }
}