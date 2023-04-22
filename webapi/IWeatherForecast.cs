using webapi;

public interface IWeatherForecast
{
    Task<List<WeatherForecast>> GetAll();
}