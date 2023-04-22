
using Microsoft.EntityFrameworkCore;
using webapi;

public class WeatherForecastRepository : IWeatherForecast
{
    private readonly DataContext _dataContex;
    public WeatherForecastRepository(DataContext dataContext)
    {
        _dataContex = dataContext;
    }

    public async Task<List<WeatherForecast>> GetAll()
    {
        var all = await _dataContex.WeatherForecasts.ToListAsync();
        return all;
    }
}