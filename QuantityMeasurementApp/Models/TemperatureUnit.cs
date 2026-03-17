using System;
using QuantityMeasurementApp.Interface;

namespace QuantityMeasurementApp.Models
{
    public sealed class TemperatureUnit : IMeasurable
    {
        private readonly string _name;

        private TemperatureUnit(string name)
        {
            _name = name;
        }

        public static readonly TemperatureUnit CELSIUS =
            new TemperatureUnit("Celsius");

        public static readonly TemperatureUnit FAHRENHEIT =
            new TemperatureUnit("Fahrenheit");

        public static readonly TemperatureUnit KELVIN =
            new TemperatureUnit("Kelvin");

        // Base unit: Celsius
        public double ConvertToBaseUnit(double value)
        {
            if (this == CELSIUS)
                return value;

            if (this == FAHRENHEIT)
                return (value - 32) * 5.0 / 9.0;

            if (this == KELVIN)
                return value - 273.15;

            throw new ArgumentException("Invalid temperature unit.");
        }

        public double ConvertFromBaseUnit(double baseValue)
        {
            if (this == CELSIUS)
                return baseValue;

            if (this == FAHRENHEIT)
                return (baseValue * 9.0 / 5.0) + 32;

            if (this == KELVIN)
                return baseValue + 273.15;

            throw new ArgumentException("Invalid temperature unit.");
        }

        public string GetUnitName()
            => _name;

        public bool SupportsArithmetic() => false;

        public void ValidateOperationSupport(string operation)
        {
            throw new NotSupportedException(
                $"Temperature does not support {operation} operation.");
        }
    }
}