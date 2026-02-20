using QuantityMeasurementApp.Models;
using QuantityMeasurementApp.Services;
using Xunit;

namespace QuantityMeasurementApp.Tests
{
    public class QuantityMeasurementServiceTests
    {
        private readonly QuantityMeasurementService _service = new();

        [Fact]
        public void testEquality_FeetToFeet_SameValue()
        {
            Assert.True(_service.ValidateLengthEquality(1.0, LengthUnit.Feet,
                                                        1.0, LengthUnit.Feet));
        }

        [Fact]
        public void testEquality_InchToInch_SameValue()
        {
            Assert.True(_service.ValidateLengthEquality(1.0, LengthUnit.Inches,
                                                        1.0, LengthUnit.Inches));
        }

        [Fact]
        public void testEquality_FeetToInch_EquivalentValue()
        {
            Assert.True(_service.ValidateLengthEquality(1.0, LengthUnit.Feet,
                                                        12.0, LengthUnit.Inches));
        }

        [Fact]
        public void testEquality_InchToFeet_EquivalentValue()
        {
            Assert.True(_service.ValidateLengthEquality(12.0, LengthUnit.Inches,
                                                        1.0, LengthUnit.Feet));
        }

        [Fact]
        public void testEquality_DifferentValues()
        {
            Assert.False(_service.ValidateLengthEquality(1.0, LengthUnit.Feet,
                                                         2.0, LengthUnit.Feet));
        }
        [Fact]
        public void testEquality_YardToFeet_EquivalentValue()
        {
            Assert.True(_service.ValidateLengthEquality(1.0, LengthUnit.Yards,
                                                3.0, LengthUnit.Feet));
        }

        [Fact]
        public void testEquality_YardToInches_EquivalentValue()
        {
            Assert.True(_service.ValidateLengthEquality(1.0, LengthUnit.Yards,
                                                36.0, LengthUnit.Inches));
        }

        [Fact]
        public void testEquality_CentimeterToInches_EquivalentValue()
        {
            Assert.True(_service.ValidateLengthEquality(1.0, LengthUnit.Centimeters,
                                                0.393701, LengthUnit.Inches));
        }

        [Fact]
        public void testEquality_YardToFeet_NonEquivalent()
        {
            Assert.False(_service.ValidateLengthEquality(1.0, LengthUnit.Yards,
                                                 2.0, LengthUnit.Feet));
        }

        [Fact]
        public void testEquality_NullComparison()
        {
            var quantity = new QuantityLength(1.0, LengthUnit.Feet);
            Assert.False(quantity.Equals(null));
        }

        [Fact]
        public void testEquality_SameReference()
        {
            var quantity = new QuantityLength(1.0, LengthUnit.Feet);
            Assert.True(quantity.Equals(quantity));
        }
    }
}