using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Models;
using QuantityMeasurementApp.Services;

namespace QuantityMeasurementApp.Tests
{
    // Marks this class as a test class
    [TestClass]
    public class QuantityMeasurementServiceTests
    {
        // Service instance used in tests
        private QuantityMeasurementService _service;

        // Setup method executed before each test
        [TestInitialize]
        public void Setup()
        {
            // Initialize service
            _service = new QuantityMeasurementService();
        }

        // testEquality_SameValue()
        [TestMethod]
        public void testEquality_SameValue()
        {
            // Compare two identical values
            bool result = _service.ValidateFeetEquality(1.0, 1.0);

            // Assert that equality returns true
            Assert.IsTrue(result);
        }

        // testEquality_DifferentValue()
        [TestMethod]
        public void testEquality_DifferentValue()
        {
            // Compare two different values
            bool result = _service.ValidateFeetEquality(1.0, 2.0);

            // Assert that equality returns false
            Assert.IsFalse(result);
        }

        // testEquality_NullComparison()
        [TestMethod]
        public void testEquality_NullComparison()
        {
            // Create Feet object
            var feet = new Feet(1.0);

            // Assert comparison with null returns false
            Assert.IsFalse(feet.Equals(null));
        }

        // testEquality_NonNumericInput()
        [TestMethod]
        public void testEquality_NonNumericInput()
        {
            // Create Feet object
            var feet = new Feet(1.0);

            // Assert comparison with different type returns false
            Assert.IsFalse(feet.Equals("1.0"));
        }

        // testEquality_SameReference()
        [TestMethod]
        public void testEquality_SameReference()
        {
            // Create Feet object
            var feet = new Feet(1.0);

            // Assert reflexive property
            Assert.IsTrue(feet.Equals(feet));
        }
    }
}