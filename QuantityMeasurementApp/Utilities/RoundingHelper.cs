using System;

namespace QuantityMeasurementApp.Utilities
{
    public static class RoundingHelper
    {
        public static double Round(double value)
        {
            return Math.Round(value, 2, MidpointRounding.AwayFromZero);
        }
    }
}