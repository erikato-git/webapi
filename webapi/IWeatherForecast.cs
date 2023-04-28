using webapi;

public interface IWeatherForecast
{
    Task<List<WeatherForecast>> GetAll();
    Task<bool> AddWeatherForecast(WeatherForecast w);
}