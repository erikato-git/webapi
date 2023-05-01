using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi;

namespace webapi_test.utils
{
    public class DbContextMoq
    {
        public async Task<DataContext> Instance()
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
    }

}