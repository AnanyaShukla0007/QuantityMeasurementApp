using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Tests
{
    [TestClass]
    public class LengthUnitTests
    {
        private const double Epsilon = 1e-6;

        [TestMethod]
        public void testConvertToBaseUnit_InchesToFeet()
        {
            double result = LengthUnit.Inches.ConvertToBaseUnit(12.0);
            Assert.AreEqual(1.0, result, Epsilon);
        }

        [TestMethod]
        public void testConvertToBaseUnit_YardsToFeet()
        {
            double result = LengthUnit.Yards.ConvertToBaseUnit(1.0);
            Assert.AreEqual(3.0, result, Epsilon);
        }

        [TestMethod]
        public void testConvertFromBaseUnit_FeetToInches()
        {
            double result = LengthUnit.Inches.ConvertFromBaseUnit(1.0);
            Assert.AreEqual(12.0, result, Epsilon);
        }

        [TestMethod]
        public void testConvertFromBaseUnit_FeetToCentimeters()
        {
            double result = LengthUnit.Centimeters.ConvertFromBaseUnit(1.0);
            Assert.AreEqual(30.48, result, Epsilon);
        }

        [TestMethod]
        public void testConversionFactor_Feet()
        {
            Assert.AreEqual(1.0,
                LengthUnit.Feet.GetConversionFactor(),
                Epsilon);
        }
    }
}