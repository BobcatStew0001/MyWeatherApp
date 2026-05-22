using Microsoft.AspNetCore.Mvc;
using MyWeatherApp.Models;

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
        CurrentWeather current = new CurrentWeather(zip);
        RonSwanson ron = new RonSwanson();
        HourlyDay hourly = new HourlyDay(zip);
        ViewBag.CurrentWeather = current.GetFormattedWeather();
        ViewBag.hourly = hourly.GetFormattedHourly();
        ViewBag.Ron = ron.GetRon();
        return View();
    }
    
    [HttpGet]
    public IActionResult Map()
    {
        string zip = HttpContext.Session.GetString("zip");
        if (zip == null)
        {
            return RedirectToAction("Index");
        }
        WeatherMap map = new WeatherMap(zip);
        ViewBag.Map = map.GetFormattedMap();
        return View();
    }
    
    
    [HttpGet]
    public IActionResult FiveDayForecast()
    {
        string zip = HttpContext.Session.GetString("zip");
        if (zip == null)
        {
            return RedirectToAction("Index");
        }
        FiveDay fiveDay = new FiveDay(zip);
        ViewBag.FiveDay = fiveDay.GetFormattedFiveDay();
        return View();
    }

    
}