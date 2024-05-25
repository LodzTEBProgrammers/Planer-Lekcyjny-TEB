using Microsoft.AspNetCore.Mvc;

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
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        // Wysyla ID do naszego serwera baz danych,
        // ktory zwraca nam osobe o danym ID
        [HttpGet("user/{id}")]
        public string GetCos(int id)
        {
            string[] users = ["Jan", "Krzysztof", "Marek", "Kamil", "Kacper"];

            if (id > 0 && id < users.Length + 1)
            {
                return $"Wywolano osobe o imieniu: {users[id - 1]}";

            }
            return "Nie ma takiej osoby";
        }

        [HttpPost("/set/{name}")]
        public string SetName(string name)
        {
            _logger.LogInformation($"Ustawiono imie na: {name}");

            return $"Ustawiono imie na: {name}";
        }
    }
}
