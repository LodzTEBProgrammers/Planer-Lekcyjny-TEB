using Microsoft.AspNetCore.Mvc;
using System;

namespace Planer_Lekcyjny_TEB.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get(int temperature)
        {
            yield return new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.UtcNow),
                TemperatureC = temperature,
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            };

        }


    }
}
