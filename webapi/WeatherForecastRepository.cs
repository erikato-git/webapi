
using Microsoft.EntityFrameworkCore;
using webapi;

public class WeatherForecastRepository : IWeatherForecast
{
    private readonly DataContext _dataContex;
    public WeatherForecastRepository(DataContext dataContext)
    {
        _dataContex = dataContext;
    }

    public async Task<bool> AddWeatherForecast(WeatherForecast w)
    {
        await _dataContex.WeatherForecasts.AddAsync(w);
        return await _dataContex.SaveChangesAsync() > 0;
    }

    public async Task<List<WeatherForecast>> GetAll()
    {
        var all = await _dataContex.WeatherForecasts.ToListAsync();
        return all;
    }
}