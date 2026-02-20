using QuantityMeasurementApp.Models;
using QuantityMeasurementApp.Services;
using Xunit;

namespace QuantityMeasurementApp.Tests
{
    public class QuantityMeasurementServiceTests
    {
        private readonly QuantityMeasurementService _service = new();

        [Fact]
        public void Given_SameFeetValue_When_Compared_Then_ShouldReturnTrue()
        {
            var firstMeasurement = new Feet(1.0);
            var secondMeasurement = new Feet(1.0);

            bool result = _service.ConvertUnits(firstMeasurement, secondMeasurement);

            Assert.True(result);
        }

        [Fact]
        public void Given_DifferentFeetValue_When_Compared_Then_ShouldReturnFalse()
        {
            var firstMeasurement = new Feet(1.0);
            var secondMeasurement = new Feet(2.0);

            bool result = _service.ConvertUnits(firstMeasurement, secondMeasurement);

            Assert.False(result);
        }

        [Fact]
        public void Given_NullMeasurement_When_Compared_Then_ShouldReturnFalse()
        {
            var firstMeasurement = new Feet(1.0);

            bool result = _service.ConvertUnits(firstMeasurement, null);

            Assert.False(result);
        }
    }
}