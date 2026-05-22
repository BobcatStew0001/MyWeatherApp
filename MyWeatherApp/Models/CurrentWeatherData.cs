namespace MyWeatherApp.Models;

public class CurrentWeatherData
{
    public string CityName { get; set; }
    public string Temp { get; set; }
    public string FeelsLike { get; set; }
    public string Description { get; set; }
    public string WindSpeed { get; set; }
    public string Humidity { get; set; }
    public string Icon { get; set; }
    public string Forecast { get; set; }
    public decimal? Rain { get; set; }
}