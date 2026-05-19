namespace MyWeatherApp.Models;

public class ForecastItem
{
    public string Date{get;set;}
    public string Temp { get; set; }
    public string FeelsLike { get; set; }
    public string Forecast { get; set; }
    public string Description { get; set; }
    public string WindSpeed { get; set; }
    public string Humidity { get; set; }
    public float ChanceOfRain { get; set; }
    
}