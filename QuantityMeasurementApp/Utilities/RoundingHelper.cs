using System;

namespace QuantityMeasurementApp.Utilities
{
    public static class RoundingHelper
    {
        public static double RoundToTwoDecimals(double value)
        {
            return Math.Round(value, 3); // matching UC7 tolerance
        }
    }
}