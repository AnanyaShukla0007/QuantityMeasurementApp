using QuantityMeasurementApp.Services;
using Xunit;

namespace QuantityMeasurementApp.Tests
{
    public class QuantityMeasurementServiceTests
    {
        private readonly QuantityMeasurementService _service = new();

        [Fact]
        public void Given_SameFeetValue_When_Compared_Then_ReturnsTrue()
        {
            Assert.True(_service.ValidateFeetEquality(1.0, 1.0));
        }

        [Fact]
        public void Given_DifferentFeetValue_When_Compared_Then_ReturnsFalse()
        {
            Assert.False(_service.ValidateFeetEquality(1.0, 2.0));
        }

        [Fact]
        public void Given_SameInchesValue_When_Compared_Then_ReturnsTrue()
        {
            Assert.True(_service.ValidateInchesEquality(1.0, 1.0));
        }

        [Fact]
        public void Given_DifferentInchesValue_When_Compared_Then_ReturnsFalse()
        {
            Assert.False(_service.ValidateInchesEquality(1.0, 2.0));
        }

        [Fact]
        public void Given_NullObject_When_Compared_Then_ReturnsFalse()
        {
            Assert.False(new QuantityMeasurementService()
                .ValidateFeetEquality(double.NaN, 1.0));
        }
    }
}