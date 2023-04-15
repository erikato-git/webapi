using Microsoft.EntityFrameworkCore;
using webapi;

public class DataContext: DbContext
{
    public DataContext(DbContextOptions<DataContext> options): base(options)
    {
    }

    public DbSet<WeatherForecast> WeatherForecasts { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var WeatherForecastId1 = "AAAAAAAA-AAAA-AAAA-AAAA-AAAAAAAAAAA1";

        modelBuilder.Entity<WeatherForecast>().HasData(
            new WeatherForecast{
                Id = Guid.Parse(WeatherForecastId1),
                TemperatureC = 25,
                Summary = "Test-object"
            }
        );
    }
}
