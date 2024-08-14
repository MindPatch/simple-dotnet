using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace LeakedApiExample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        // Deliberately exposed endpoint
        [HttpGet("leaked")]
        public IActionResult GetLeakedData()
        {
            // This method is a "leaked" API that exposes potentially sensitive information.
            var leakedData = new
            {
                SecretInfo = "This is sensitive information that should not be exposed.",
                DatabaseConnectionString = "Server=myServerAddress;Database=myDataBase;User Id=myUsername;Password=myPassword;"
            };

            return Ok(leakedData);
        }
    }

    public class WeatherForecast
    {
        public DateTime Date { get; set; }
        public int TemperatureC { get; set; }
        public string Summary { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }
}
