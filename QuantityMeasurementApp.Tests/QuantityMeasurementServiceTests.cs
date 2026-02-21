using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Models;
using QuantityMeasurementApp.Services;

namespace QuantityMeasurementApp.Tests
{
    /// <summary>
    /// Contains unit tests validating Feet and Inches equality behavior for UC2.
    /// </summary>
    [TestClass]
    public class QuantityMeasurementServiceTests
    {
        // Service instance used across tests
        private QuantityMeasurementService _service;

        // Runs before each test method
        [TestInitialize]
        public void Setup()
        {
            // Initialize service
            _service = new QuantityMeasurementService();
        }

        // -------- FEET TESTS --------

        [TestMethod]
        public void testFeetEquality_SameValue()
        {
            // Compare identical feet values
            bool result = _service.ValidateFeetEquality(1.0, 1.0);

            // Verify equality returns true
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void testFeetEquality_DifferentValue()
        {
            // Compare different feet values
            bool result = _service.ValidateFeetEquality(1.0, 2.0);

            // Verify equality returns false
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void testFeetEquality_NullComparison()
        {
            // Create feet instance
            var feet = new Feet(1.0);

            // Verify comparison with null returns false
            Assert.IsFalse(feet.Equals(null));
        }

        [TestMethod]
        public void testFeetEquality_NonNumericInput()
        {
            // Create feet instance
            var feet = new Feet(1.0);

            // Verify comparison with different type returns false
            Assert.IsFalse(feet.Equals("invalid"));
        }

        [TestMethod]
        public void testFeetEquality_SameReference()
        {
            // Create feet instance
            var feet = new Feet(1.0);

            // Verify reflexive property
            Assert.IsTrue(feet.Equals(feet));
        }

        // -------- INCHES TESTS --------

        [TestMethod]
        public void testInchesEquality_SameValue()
        {
            // Compare identical inches values
            bool result = _service.ValidateInchesEquality(1.0, 1.0);

            // Verify equality returns true
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void testInchesEquality_DifferentValue()
        {
            // Compare different inches values
            bool result = _service.ValidateInchesEquality(1.0, 2.0);

            // Verify equality returns false
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void testInchesEquality_NullComparison()
        {
            // Create inches instance
            var inches = new Inches(1.0);

            // Verify comparison with null returns false
            Assert.IsFalse(inches.Equals(null));
        }

        [TestMethod]
        public void testInchesEquality_NonNumericInput()
        {
            // Create inches instance
            var inches = new Inches(1.0);

            // Verify comparison with different type returns false
            Assert.IsFalse(inches.Equals("invalid"));
        }

        [TestMethod]
        public void testInchesEquality_SameReference()
        {
            // Create inches instance
            var inches = new Inches(1.0);

            // Verify reflexive property
            Assert.IsTrue(inches.Equals(inches));
        }
    }
}