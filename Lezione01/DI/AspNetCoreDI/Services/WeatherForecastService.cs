using AspNetCoreDI.Models;

namespace AspNetCoreDI.Services;

public interface IWeatherForecastService
{
    IEnumerable<WeatherForecast> GetWeatherForecast();
}

public class WeatherForecastService : IWeatherForecastService
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastService> _logger;
    private readonly IApplicationService _applicationService;

    public WeatherForecastService(ILogger<WeatherForecastService> logger, IApplicationService applicationService)
    {
        _logger = logger;
        _applicationService = applicationService;
    }

    public IEnumerable<WeatherForecast> GetWeatherForecast()
    {
        _logger.LogInformation("Application ID: {appGuid}", _applicationService.GetApplicationGuid()); // Always the same because Singleton

        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}
