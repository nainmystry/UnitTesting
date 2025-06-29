using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using RoomBooking.API.Controllers;

namespace RoomBooking.API.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Should_Return_Forecasts_Result()
        {

            var logger = new Mock<ILogger<WeatherForecastController>>();
            var controller = new WeatherForecastController(logger.Object);

            var result = controller.Get();


            result.Should().NotBeNull();

        }
    }
}