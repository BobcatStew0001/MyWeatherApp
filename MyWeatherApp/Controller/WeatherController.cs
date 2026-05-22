using Microsoft.AspNetCore.Mvc;
using MyWeatherApp.Models;
using Newtonsoft.Json.Linq;

namespace MyWeatherApp.Controllers;

public class WeatherController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult Index(string zip)
    {
        HttpContext.Session.SetString("zip", zip);
        WeatherService service = new WeatherService(zip);
        CurrentWeatherData weatherData = service.GetWeatherData();
        JArray forecastData = service.GetForecastData();
        CurrentWeather currentWeather = new CurrentWeather(weatherData);
        string ron = new RonSwanson().GetRon();
        ViewBag.Ron = ron;
        HourlyDay hourlyDay = new HourlyDay(forecastData);
        ViewBag.CurrentWeather = currentWeather.GetFormattedWeather();
        ViewBag.Hourly = hourlyDay.GetFormattedHourly();
        return View();
    }
    
    [HttpGet]
    public IActionResult Map()
    {
        string zip = HttpContext.Session.GetString("zip");
        if (zip == null) return RedirectToAction("Index");
        Map serviceMap = new Map(zip);
        ViewBag.Lat = serviceMap.GetLat();
        ViewBag.Lon = serviceMap.GetLon();
        ViewBag.ApiKey = serviceMap.GetApiKey();
        return View();
    }
    
    [HttpGet]
    public IActionResult FiveDayForecast()
    {
        string zip = HttpContext.Session.GetString("zip");
        if (zip == null) return RedirectToAction("Index");
        WeatherService service = new WeatherService(zip);
        JArray forecastData = service.GetForecastData();
        FiveDay fiveDay = new FiveDay(forecastData);
        ViewBag.FiveDay = fiveDay.GetFormattedFiveDay();
        return View();
    }
    

    
}