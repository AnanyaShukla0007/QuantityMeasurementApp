using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Tests
{
    [TestClass]
    public class UC14TemperatureTests
    {
        private const double Epsilon = 0.001;

        // =========================
        // EQUALITY TESTS
        // =========================

        [TestMethod]
        public void Temperature_0C_Equals_32F()
        {
            var c = new Quantity<TemperatureUnit>(0, TemperatureUnit.CELSIUS);
            var f = new Quantity<TemperatureUnit>(32, TemperatureUnit.FAHRENHEIT);

            Assert.IsTrue(c.Equals(f));
        }

        [TestMethod]
        public void Temperature_100C_Equals_212F()
        {
            var c = new Quantity<TemperatureUnit>(100, TemperatureUnit.CELSIUS);
            var f = new Quantity<TemperatureUnit>(212, TemperatureUnit.FAHRENHEIT);

            Assert.IsTrue(c.Equals(f));
        }

        [TestMethod]
        public void Temperature_Negative40C_Equals_Negative40F()
        {
            var c = new Quantity<TemperatureUnit>(-40, TemperatureUnit.CELSIUS);
            var f = new Quantity<TemperatureUnit>(-40, TemperatureUnit.FAHRENHEIT);

            Assert.IsTrue(c.Equals(f));
        }

        // =========================
        // CONVERSION TESTS
        // =========================

        [TestMethod]
        public void Convert_Celsius_To_Fahrenheit()
        {
            var c = new Quantity<TemperatureUnit>(100, TemperatureUnit.CELSIUS);
            var result = c.ConvertTo(TemperatureUnit.FAHRENHEIT);

            Assert.AreEqual(212, result.Value, Epsilon);
        }

        [TestMethod]
        public void Convert_Fahrenheit_To_Celsius()
        {
            var f = new Quantity<TemperatureUnit>(32, TemperatureUnit.FAHRENHEIT);
            var result = f.ConvertTo(TemperatureUnit.CELSIUS);

            Assert.AreEqual(0, result.Value, Epsilon);
        }

        [TestMethod]
        public void Convert_Celsius_To_Kelvin()
        {
            var c = new Quantity<TemperatureUnit>(0, TemperatureUnit.CELSIUS);
            var result = c.ConvertTo(TemperatureUnit.KELVIN);

            Assert.AreEqual(273.15, result.Value, Epsilon);
        }

        // =========================
        // UNSUPPORTED OPERATIONS
        // =========================

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void Temperature_Add_ShouldThrow()
        {
            var t1 = new Quantity<TemperatureUnit>(100, TemperatureUnit.CELSIUS);
            var t2 = new Quantity<TemperatureUnit>(50, TemperatureUnit.CELSIUS);

            t1.Add(t2);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void Temperature_Subtract_ShouldThrow()
        {
            var t1 = new Quantity<TemperatureUnit>(100, TemperatureUnit.CELSIUS);
            var t2 = new Quantity<TemperatureUnit>(50, TemperatureUnit.CELSIUS);

            t1.Subtract(t2);
        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void Temperature_Divide_ShouldThrow()
        {
            var t1 = new Quantity<TemperatureUnit>(100, TemperatureUnit.CELSIUS);
            var t2 = new Quantity<TemperatureUnit>(50, TemperatureUnit.CELSIUS);

            t1.Divide(t2);
        }

        // =========================
        // CROSS CATEGORY CHECK
        // =========================

        [TestMethod]
        public void Temperature_NotEqual_Length()
        {
            var temp = new Quantity<TemperatureUnit>(100, TemperatureUnit.CELSIUS);
            var length = new Quantity<LengthUnit>(100, LengthUnit.Feet);

            Assert.IsFalse(temp.Equals(length));
        }
    }
}