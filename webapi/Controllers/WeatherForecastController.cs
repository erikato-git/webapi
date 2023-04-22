using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherForecast _weatherForecastRepository;
        public WeatherForecastController(IWeatherForecast weatherForecastRepository)
        {
            _weatherForecastRepository = weatherForecastRepository;
        }

        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.UtcNow,
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("/test")]
        public string Test() 
        {
            return "test-string";
        }

        [HttpGet("/db_test")]
        public async Task<ActionResult<IEnumerable<WeatherForecast>>> DbTest(){
            try
            {
                var result = await _weatherForecastRepository.GetAll();
                if(result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        } 
    }
}