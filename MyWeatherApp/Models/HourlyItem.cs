namespace MyWeatherApp.Models;

public class HourlyItem
{
    
    public DateTime DateHourly { get; set; }
    public string Temp { get; set; } = string.Empty;
    public string FeelsLike { get; set; }
    public string Forecast { get; set; }
    public string Description { get; set; }
    public string WindSpeed { get; set; }
    public string Humidity { get; set; }
    public float? ChanceOfRain { get; set; }
    public string Icon { get; set; }
    public decimal? RainHourly { get; set; }
}