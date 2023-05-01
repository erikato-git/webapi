using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using webapi;
using webapi.Controllers;
using webapi_test.controllers;

namespace webapi_test
{
    public class WeatherForecastControllerTests
    {

        [Fact]
        public async Task DbTestOk()
        {
            // Arrange
            var controller = await new WeatherForecastControllerMoq().Instance();

            // Act
            var actionResult = await controller.DbTest();
            var result = actionResult.Result as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.IsType<List<WeatherForecast>>(result.Value);
        }


    }
}