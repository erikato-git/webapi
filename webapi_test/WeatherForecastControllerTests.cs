using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using webapi;
using webapi.Controllers;

namespace webapi_test
{
    public class WeatherForecastControllerTests
    {
        private TestSetups testSetups = new TestSetups();

        [Fact]
        public async Task DbTestOk()
        {
            // Arrange
            var controller = await testSetups.WeatherForecastControllerMoqAsync();

            // Act
            var actionResult = await controller.DbTest();
            var result = actionResult.Result as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
        }


    }
}