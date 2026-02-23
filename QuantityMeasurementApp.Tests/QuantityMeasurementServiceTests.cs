using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Models;

namespace QuantityMeasurementApp.Tests
{
    [TestClass]
    public class SubtractionDivisionTests
    {
        // ================= SUBTRACTION =================

        [TestMethod]
        public void Subtract_SameUnit()
        {
            var q1 = new Quantity<LengthUnit>(10, LengthUnit.Feet);
            var q2 = new Quantity<LengthUnit>(5, LengthUnit.Feet);

            var result = q1.Subtract(q2);

            Assert.AreEqual(5, result.Value);
        }

        [TestMethod]
        public void Subtract_CrossUnit()
        {
            var q1 = new Quantity<LengthUnit>(10, LengthUnit.Feet);
            var q2 = new Quantity<LengthUnit>(6, LengthUnit.Inches);

            var result = q1.Subtract(q2);

            Assert.AreEqual(9.5, result.Value);
        }

        [TestMethod]
        public void Subtract_ExplicitTargetUnit()
        {
            var q1 = new Quantity<LengthUnit>(10, LengthUnit.Feet);
            var q2 = new Quantity<LengthUnit>(6, LengthUnit.Inches);

            var result = q1.Subtract(q2, LengthUnit.Inches);

            Assert.AreEqual(114, result.Value);
        }

        [TestMethod]
        public void Subtract_ResultNegative()
        {
            var q1 = new Quantity<LengthUnit>(5, LengthUnit.Feet);
            var q2 = new Quantity<LengthUnit>(10, LengthUnit.Feet);

            var result = q1.Subtract(q2);

            Assert.AreEqual(-5, result.Value);
        }

        [TestMethod]
        public void Subtract_ResultZero()
        {
            var q1 = new Quantity<LengthUnit>(10, LengthUnit.Feet);
            var q2 = new Quantity<LengthUnit>(120, LengthUnit.Inches);

            var result = q1.Subtract(q2);

            Assert.AreEqual(0, result.Value);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Subtract_NullOperand()
        {
            var q1 = new Quantity<LengthUnit>(10, LengthUnit.Feet);
            q1.Subtract(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Subtract_CrossCategory()
        {
            var length = new Quantity<LengthUnit>(10, LengthUnit.Feet);
            var weight = new Quantity<WeightUnit>(5, WeightUnit.Kilogram);

            length.Subtract((dynamic)weight);
        }

        // ================= DIVISION =================

        [TestMethod]
        public void Divide_SameUnit()
        {
            var q1 = new Quantity<LengthUnit>(10, LengthUnit.Feet);
            var q2 = new Quantity<LengthUnit>(2, LengthUnit.Feet);

            var result = q1.Divide(q2);

            Assert.AreEqual(5, result);
        }

        [TestMethod]
        public void Divide_CrossUnit()
        {
            var q1 = new Quantity<LengthUnit>(24, LengthUnit.Inches);
            var q2 = new Quantity<LengthUnit>(2, LengthUnit.Feet);

            var result = q1.Divide(q2);

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void Divide_RatioLessThanOne()
        {
            var q1 = new Quantity<LengthUnit>(5, LengthUnit.Feet);
            var q2 = new Quantity<LengthUnit>(10, LengthUnit.Feet);

            var result = q1.Divide(q2);

            Assert.AreEqual(0.5, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArithmeticException))]
        public void Divide_ByZero()
        {
            var q1 = new Quantity<LengthUnit>(10, LengthUnit.Feet);
            var q2 = new Quantity<LengthUnit>(0, LengthUnit.Feet);

            q1.Divide(q2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Divide_NullOperand()
        {
            var q1 = new Quantity<LengthUnit>(10, LengthUnit.Feet);
            q1.Divide(null);
        }

        [TestMethod]
        public void Subtract_Immutability()
        {
            var q1 = new Quantity<LengthUnit>(10, LengthUnit.Feet);
            var q2 = new Quantity<LengthUnit>(5, LengthUnit.Feet);

            var result = q1.Subtract(q2);

            Assert.AreEqual(10, q1.Value);
            Assert.AreEqual(5, result.Value);
        }

        [TestMethod]
        public void Divide_Immutability()
        {
            var q1 = new Quantity<LengthUnit>(10, LengthUnit.Feet);
            var q2 = new Quantity<LengthUnit>(2, LengthUnit.Feet);

            q1.Divide(q2);

            Assert.AreEqual(10, q1.Value);
        }
    }
}