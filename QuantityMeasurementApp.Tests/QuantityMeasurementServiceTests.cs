using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Models;
using QuantityMeasurementApp.Services;

namespace QuantityMeasurementApp.Tests
{
    /// <summary>
    /// UC5 comprehensive conversion test suite.
    /// </summary>
    [TestClass]
    public class QuantityMeasurementServiceTests
    {
        private const double Epsilon = 1e-6;

        [TestMethod]
        public void testConversion_FeetToInches()
        {
            double result = QuantityLength.Convert(1.0, LengthUnit.Feet, LengthUnit.Inches);
            Assert.AreEqual(12.0, result, Epsilon);
        }

        [TestMethod]
        public void testConversion_InchesToFeet()
        {
            double result = QuantityLength.Convert(24.0, LengthUnit.Inches, LengthUnit.Feet);
            Assert.AreEqual(2.0, result, Epsilon);
        }

        [TestMethod]
        public void testConversion_YardsToInches()
        {
            double result = QuantityLength.Convert(1.0, LengthUnit.Yards, LengthUnit.Inches);
            Assert.AreEqual(36.0, result, Epsilon);
        }

        [TestMethod]
        public void testConversion_InchesToYards()
        {
            double result = QuantityLength.Convert(72.0, LengthUnit.Inches, LengthUnit.Yards);
            Assert.AreEqual(2.0, result, Epsilon);
        }

        [TestMethod]
        public void testConversion_CentimetersToInches()
        {
            double result = QuantityLength.Convert(2.54, LengthUnit.Centimeters, LengthUnit.Inches);
            Assert.AreEqual(1.0, result, 1e-3);
        }

        [TestMethod]
        public void testConversion_FeetToYards()
        {
            double result = QuantityLength.Convert(6.0, LengthUnit.Feet, LengthUnit.Yards);
            Assert.AreEqual(2.0, result, Epsilon);
        }

        [TestMethod]
        public void testConversion_RoundTrip_PreservesValue()
        {
            double original = 5.0;

            double converted = QuantityLength.Convert(original, LengthUnit.Feet, LengthUnit.Inches);
            double back = QuantityLength.Convert(converted, LengthUnit.Inches, LengthUnit.Feet);

            Assert.AreEqual(original, back, Epsilon);
        }

        [TestMethod]
        public void testConversion_ZeroValue()
        {
            double result = QuantityLength.Convert(0.0, LengthUnit.Feet, LengthUnit.Inches);
            Assert.AreEqual(0.0, result, Epsilon);
        }

        [TestMethod]
        public void testConversion_NegativeValue()
        {
            double result = QuantityLength.Convert(-1.0, LengthUnit.Feet, LengthUnit.Inches);
            Assert.AreEqual(-12.0, result, Epsilon);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void testConversion_InvalidValue_Throws()
        {
            QuantityLength.Convert(double.NaN, LengthUnit.Feet, LengthUnit.Inches);
        }

        [TestMethod]
        public void testConversion_PrecisionTolerance()
        {
            double result = QuantityLength.Convert(1.0, LengthUnit.Centimeters, LengthUnit.Inches);
            Assert.IsTrue(Math.Abs(result - 0.393701) < 1e-6);
        }
    }
}