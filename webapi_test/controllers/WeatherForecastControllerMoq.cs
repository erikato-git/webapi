
using webapi.Controllers;
using webapi_test.utils;

namespace webapi_test.controllers
{
    public class WeatherForecastControllerMoq
    {
        public async Task<WeatherForecastController> Instance()
        {
            var fakeDb = await new DbContextMoq().Instance();

            // --- Automapper ---
            // var config = new MapperConfiguration(cfg =>
            // {
            //     cfg.AddProfile(new MapperService());    // in Webapi, MapperService responsible for mapping DTOs and model-classes
            // });
            // var mapper = config.CreateMapper();
            // var weatherForecastRepository = new WeatherForecastRepository(fakeDb,mapper);

            var weatherForecastRepository = new WeatherForecastRepository(fakeDb);
            var controller = new WeatherForecastController(weatherForecastRepository);
            return controller;
        }
    }
}