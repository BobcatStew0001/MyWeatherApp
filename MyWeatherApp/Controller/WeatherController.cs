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
        FiveDay fiveDay = new FiveDay(zip);
        CurrentWeather current = new CurrentWeather(zip);
        RonSwanson ron = new RonSwanson();
        ViewBag.CurrentWeather = current.GetFormattedWeather();
        ViewBag.fiveDay = fiveDay.GetFormattedFiveDay();
        ViewBag.Ron = ron.GetRon();
        return View();
    }
}