using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Models;
using QuantityMeasurementApp.Services;

namespace QuantityMeasurementApp.Tests
{
    [TestClass]
    public class QuantityLengthUC7Tests
    {
        private const double Epsilon = 1e-3;

        private readonly QuantityMeasurementService _service =
            new QuantityMeasurementService();

        [DataTestMethod]
        [DataRow(1.0, LengthUnit.Feet, 12.0, LengthUnit.Inches, LengthUnit.Feet, 2.0)]
        [DataRow(1.0, LengthUnit.Feet, 12.0, LengthUnit.Inches, LengthUnit.Inches, 24.0)]
        [DataRow(1.0, LengthUnit.Feet, 12.0, LengthUnit.Inches, LengthUnit.Yards, 0.667)]
        [DataRow(1.0, LengthUnit.Inches, 1.0, LengthUnit.Inches, LengthUnit.Centimeters, 5.08)]
        [DataRow(2.0, LengthUnit.Yards, 3.0, LengthUnit.Feet, LengthUnit.Yards, 3.0)]
        [DataRow(2.0, LengthUnit.Yards, 3.0, LengthUnit.Feet, LengthUnit.Feet, 9.0)]
        [DataRow(5.0, LengthUnit.Feet, 0.0, LengthUnit.Inches, LengthUnit.Yards, 1.667)]
        [DataRow(5.0, LengthUnit.Feet, -2.0, LengthUnit.Feet, LengthUnit.Inches, 36.0)]
        public void testAddition_ExplicitTargetUnit(
            double v1, LengthUnit u1,
            double v2, LengthUnit u2,
            LengthUnit target,
            double expected)
        {
            var result =
                _service.AddLength(
                    new QuantityLength(v1, u1),
                    new QuantityLength(v2, u2),
                    target);

            Assert.AreEqual(expected, result.Value, Epsilon);
            Assert.AreEqual(target, result.Unit);
        }

        [TestMethod]
        public void testAddition_ExplicitTargetUnit_Commutativity()
        {
            var result1 =
                _service.AddLength(
                    new QuantityLength(1.0, LengthUnit.Feet),
                    new QuantityLength(12.0, LengthUnit.Inches),
                    LengthUnit.Yards);

            var result2 =
                _service.AddLength(
                    new QuantityLength(12.0, LengthUnit.Inches),
                    new QuantityLength(1.0, LengthUnit.Feet),
                    LengthUnit.Yards);

            Assert.IsTrue(result1.Equals(result2));
        }
    }
}