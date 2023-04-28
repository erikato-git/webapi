using Microsoft.EntityFrameworkCore;
using webapi.Controllers;
using webapi;

namespace webapi_test
{
    public class TestSetups
    {
        // NB: Consider to use a real database for testing - https://www.youtube.com/watch?v=MhEa8fENJqM&list=TLPQMjcwNDIwMjM7KrZe6nBXHw&index=5 (22:00)
        public async Task<DataContext> DbContextMoqAsync()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var fakeDb = new DataContext(options);
            if (await fakeDb.WeatherForecasts.CountAsync() == 0)
            {
                var tmpWeatherForecasts = new List<WeatherForecast>()
                {
                    new WeatherForecast()
                    {
                        Id = new Guid(),
                        Date = new DateTime().AddDays(0),
                        TemperatureC = 20,
                        Summary = "Weatherforecast 1"
                    },
                    new WeatherForecast()
                    {
                        Id = new Guid(),
                        Date = new DateTime().AddDays(1),
                        TemperatureC = 25,
                        Summary = "Weatherforecast 2"
                    }

                };

                foreach (var i in tmpWeatherForecasts)
                {
                    fakeDb.WeatherForecasts.Add(i);
                }
            }

            await fakeDb.SaveChangesAsync();

            return fakeDb;
        }

        public async Task<WeatherForecastController> WeatherForecastControllerMoqAsync()
        {
            var fakeDb = await DbContextMoqAsync();

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
