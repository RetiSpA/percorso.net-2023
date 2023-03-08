using AspNetCoreDI.Models;
using AspNetCoreDI.Services;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreDI.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly IWeatherForecastService _weatherForecastService;
    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IApplicationService _applicationService;

    public WeatherForecastController(
        IWeatherForecastService weatherForecastService,
        ILogger<WeatherForecastController> logger,
        IApplicationService applicationService)
    {
        _weatherForecastService = weatherForecastService;
        _logger = logger;
        _applicationService = applicationService;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        _logger.LogInformation("Application ID: {appGuid}", _applicationService.GetApplicationGuid()); // Always the same because Singleton

        return _weatherForecastService.GetWeatherForecast();
    }
}