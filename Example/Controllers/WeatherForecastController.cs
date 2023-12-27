using Example.Entity;
using Microsoft.AspNetCore.Mvc;
using MongoDbAuditLog.AuditLogRepository.Abstract;

namespace Example.Controllers
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
        private readonly IAuditLogRepository<Audit> _aLog;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IAuditLogRepository<Audit> aLog)
        {
            _logger = logger;
            _aLog = aLog;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            await _aLog.Add(new Audit()
            {
                Descr = "Test Descr",
                Title = "Test Title"
            });
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
