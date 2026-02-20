using System;

namespace QuantityMeasurementApp.Exceptions
{
    /// <summary>
    /// Thrown when an invalid unit or unsupported unit conversion is attempted.
    /// </summary>
    public class InvalidUnitException : Exception
    {
        public InvalidUnitException()
        {
        }

        public InvalidUnitException(string message)
            : base(message)
        {
        }

        public InvalidUnitException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}