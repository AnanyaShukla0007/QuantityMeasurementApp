using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuantityMeasurementApp.Models;
using QuantityMeasurementApp.Services;

namespace QuantityMeasurementApp.Tests
{
    /// <summary>
    /// Contains comprehensive unit tests validating UC1, UC2, UC3, and UC4 length equality behavior.
    /// </summary>
    [TestClass]
    public class QuantityMeasurementServiceTests
    {
        // Service instance used for all tests
        private QuantityMeasurementService _service;

        // Runs before each test
        [TestInitialize]
        public void Setup()
        {
            // Initialize service
            _service = new QuantityMeasurementService();
        }

        // -------------------- UC1 TESTS (Feet) --------------------

        [TestMethod]
        public void testEquality_FeetToFeet_SameValue()
        {
            Assert.IsTrue(_service.ValidateFeetEquality(1.0, 1.0));
        }

        [TestMethod]
        public void testEquality_FeetToFeet_DifferentValue()
        {
            Assert.IsFalse(_service.ValidateFeetEquality(1.0, 2.0));
        }

        [TestMethod]
        public void testEquality_Feet_SameReference()
        {
            var quantity = new QuantityLength(1.0, LengthUnit.Feet);

            Assert.IsTrue(quantity.Equals(quantity));
        }

        // -------------------- UC2 TESTS (Inches) --------------------

        [TestMethod]
        public void testEquality_InchToInch_SameValue()
        {
            Assert.IsTrue(_service.ValidateInchesEquality(1.0, 1.0));
        }

        [TestMethod]
        public void testEquality_InchToInch_DifferentValue()
        {
            Assert.IsFalse(_service.ValidateInchesEquality(1.0, 2.0));
        }

        // -------------------- UC3 TESTS (Cross Unit Feet & Inches) --------------------

        [TestMethod]
        public void testEquality_InchToFeet_EquivalentValue()
        {
            Assert.IsTrue(_service.ValidateLengthEquality(
                12.0, LengthUnit.Inches,
                1.0, LengthUnit.Feet));
        }

        [TestMethod]
        public void testEquality_FeetToInch_EquivalentValue()
        {
            Assert.IsTrue(_service.ValidateLengthEquality(
                1.0, LengthUnit.Feet,
                12.0, LengthUnit.Inches));
        }

        [TestMethod]
        public void testEquality_InvalidUnitComparison()
        {
            var quantity = new QuantityLength(1.0, LengthUnit.Feet);

            Assert.IsFalse(quantity.Equals("invalid"));
        }

        [TestMethod]
        public void testEquality_NullComparison()
        {
            var quantity = new QuantityLength(1.0, LengthUnit.Feet);

            Assert.IsFalse(quantity.Equals(null));
        }

        // -------------------- UC4 TESTS (Yards) --------------------

        [TestMethod]
        public void testEquality_YardToYard_SameValue()
        {
            Assert.IsTrue(_service.ValidateLengthEquality(
                1.0, LengthUnit.Yards,
                1.0, LengthUnit.Yards));
        }

        [TestMethod]
        public void testEquality_YardToYard_DifferentValue()
        {
            Assert.IsFalse(_service.ValidateLengthEquality(
                1.0, LengthUnit.Yards,
                2.0, LengthUnit.Yards));
        }

        [TestMethod]
        public void testEquality_YardToFeet_EquivalentValue()
        {
            Assert.IsTrue(_service.ValidateLengthEquality(
                1.0, LengthUnit.Yards,
                3.0, LengthUnit.Feet));
        }

        [TestMethod]
        public void testEquality_FeetToYard_EquivalentValue()
        {
            Assert.IsTrue(_service.ValidateLengthEquality(
                3.0, LengthUnit.Feet,
                1.0, LengthUnit.Yards));
        }

        [TestMethod]
        public void testEquality_YardToInches_EquivalentValue()
        {
            Assert.IsTrue(_service.ValidateLengthEquality(
                1.0, LengthUnit.Yards,
                36.0, LengthUnit.Inches));
        }

        [TestMethod]
        public void testEquality_InchesToYard_EquivalentValue()
        {
            Assert.IsTrue(_service.ValidateLengthEquality(
                36.0, LengthUnit.Inches,
                1.0, LengthUnit.Yards));
        }

        [TestMethod]
        public void testEquality_YardToFeet_NonEquivalentValue()
        {
            Assert.IsFalse(_service.ValidateLengthEquality(
                1.0, LengthUnit.Yards,
                2.0, LengthUnit.Feet));
        }

        // -------------------- UC4 TESTS (Centimeters) --------------------

        [TestMethod]
        public void testEquality_CentimeterToCentimeter_SameValue()
        {
            Assert.IsTrue(_service.ValidateLengthEquality(
                2.0, LengthUnit.Centimeters,
                2.0, LengthUnit.Centimeters));
        }

        [TestMethod]
        public void testEquality_CentimeterToInches_EquivalentValue()
        {
            Assert.IsTrue(_service.ValidateLengthEquality(
                1.0, LengthUnit.Centimeters,
                0.393701, LengthUnit.Inches));
        }

        [TestMethod]
        public void testEquality_CentimeterToFeet_NonEquivalentValue()
        {
            Assert.IsFalse(_service.ValidateLengthEquality(
                1.0, LengthUnit.Centimeters,
                1.0, LengthUnit.Feet));
        }

        // -------------------- TRANSITIVE PROPERTY --------------------

        [TestMethod]
        public void testEquality_MultiUnit_TransitiveProperty()
        {
            bool yardToFeet = _service.ValidateLengthEquality(
                1.0, LengthUnit.Yards,
                3.0, LengthUnit.Feet);

            bool feetToInches = _service.ValidateLengthEquality(
                3.0, LengthUnit.Feet,
                36.0, LengthUnit.Inches);

            bool yardToInches = _service.ValidateLengthEquality(
                1.0, LengthUnit.Yards,
                36.0, LengthUnit.Inches);

            Assert.IsTrue(yardToFeet && feetToInches && yardToInches);
        }

        // -------------------- PRECISION / COMPLEX SCENARIO --------------------

        [TestMethod]
        public void testEquality_AllUnits_ComplexScenario()
        {
            Assert.IsTrue(_service.ValidateLengthEquality(
                2.0, LengthUnit.Yards,
                72.0, LengthUnit.Inches));
        }
    }
}