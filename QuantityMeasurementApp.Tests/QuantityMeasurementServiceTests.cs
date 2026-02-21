using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Models;
using QuantityMeasurementApp.Services;
using System;

namespace QuantityMeasurementApp.Tests
{
    [TestClass]
    public class QuantityLengthUC6Tests
    {
        private const double Epsilon = 1e-6;

        private readonly QuantityMeasurementService _service =
            new QuantityMeasurementService();

        // ---------------- SAME UNIT ADDITION ----------------

        [TestMethod]
        [DataRow(1.0, LengthUnit.Feet, 2.0, LengthUnit.Feet, 3.0, LengthUnit.Feet)]
        [DataRow(6.0, LengthUnit.Inches, 6.0, LengthUnit.Inches, 12.0, LengthUnit.Inches)]
        public void testAddition_SameUnit(
            double v1, LengthUnit u1,
            double v2, LengthUnit u2,
            double expectedValue, LengthUnit expectedUnit)
        {
            var result = _service.Add(
                new QuantityLength(v1, u1),
                new QuantityLength(v2, u2));

            Assert.AreEqual(expectedValue, result.Value, Epsilon);
            Assert.AreEqual(expectedUnit, result.Unit);
        }

        // ---------------- CROSS UNIT ADDITION ----------------

        [TestMethod]
        [DataRow(1.0, LengthUnit.Feet, 12.0, LengthUnit.Inches, 2.0, LengthUnit.Feet)]
        [DataRow(12.0, LengthUnit.Inches, 1.0, LengthUnit.Feet, 24.0, LengthUnit.Inches)]
        [DataRow(1.0, LengthUnit.Yards, 3.0, LengthUnit.Feet, 2.0, LengthUnit.Yards)]
        [DataRow(2.54, LengthUnit.Centimeters, 1.0, LengthUnit.Inches, 5.08, LengthUnit.Centimeters)]
        public void testAddition_CrossUnit(
            double v1, LengthUnit u1,
            double v2, LengthUnit u2,
            double expectedValue, LengthUnit expectedUnit)
        {
            var result = _service.Add(
                new QuantityLength(v1, u1),
                new QuantityLength(v2, u2));

            Assert.AreEqual(expectedValue, result.Value, 1e-2);
            Assert.AreEqual(expectedUnit, result.Unit);
        }

        // ---------------- COMMUTATIVITY ----------------

        [TestMethod]
        public void testAddition_Commutativity()
        {
            var a = new QuantityLength(1.0, LengthUnit.Feet);
            var b = new QuantityLength(12.0, LengthUnit.Inches);

            var result1 = _service.Add(a, b);
            var result2 = _service.Add(b, a);

            Assert.IsTrue(result1.Equals(result2));
        }

        // ---------------- IDENTITY (ZERO) ----------------

        [TestMethod]
        [DataRow(5.0, LengthUnit.Feet, 0.0, LengthUnit.Inches)]
        [DataRow(10.0, LengthUnit.Yards, 0.0, LengthUnit.Feet)]
        public void testAddition_WithZero(
            double v1, LengthUnit u1,
            double v2, LengthUnit u2)
        {
            var result = _service.Add(
                new QuantityLength(v1, u1),
                new QuantityLength(v2, u2));

            Assert.AreEqual(v1, result.Value, Epsilon);
        }

        // ---------------- NEGATIVE VALUES ----------------

        [TestMethod]
        [DataRow(5.0, LengthUnit.Feet, -2.0, LengthUnit.Feet, 3.0)]
        [DataRow(10.0, LengthUnit.Inches, -5.0, LengthUnit.Inches, 5.0)]
        public void testAddition_NegativeValues(
            double v1, LengthUnit u1,
            double v2, LengthUnit u2,
            double expected)
        {
            var result = _service.Add(
                new QuantityLength(v1, u1),
                new QuantityLength(v2, u2));

            Assert.AreEqual(expected, result.Value, Epsilon);
        }

        // ---------------- NULL HANDLING ----------------

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void testAddition_NullSecondOperand()
        {
            _service.Add(
                new QuantityLength(1.0, LengthUnit.Feet),
                null);
        }

        // ---------------- LARGE VALUES ----------------

        [TestMethod]
        [DataRow(1e6, 1e6, 2e6)]
        [DataRow(5e7, 5e7, 1e8)]
        public void testAddition_LargeValues(
            double v1, double v2, double expected)
        {
            var result = _service.Add(
                new QuantityLength(v1, LengthUnit.Feet),
                new QuantityLength(v2, LengthUnit.Feet));

            Assert.AreEqual(expected, result.Value, Epsilon);
        }

        // ---------------- SMALL VALUES ----------------

        [TestMethod]
        [DataRow(0.001, 0.002, 0.003)]
        [DataRow(0.0001, 0.0002, 0.0003)]
        public void testAddition_SmallValues(
            double v1, double v2, double expected)
        {
            var result = _service.Add(
                new QuantityLength(v1, LengthUnit.Feet),
                new QuantityLength(v2, LengthUnit.Feet));

            Assert.AreEqual(expected, result.Value, Epsilon);
        }

        // ---------------- RESULT UNIT CONSISTENCY ----------------

        [TestMethod]
        public void testAddition_ResultInFirstOperandUnit()
        {
            var result = _service.Add(
                new QuantityLength(12.0, LengthUnit.Inches),
                new QuantityLength(1.0, LengthUnit.Feet));

            Assert.AreEqual(LengthUnit.Inches, result.Unit);
        }
    }
}