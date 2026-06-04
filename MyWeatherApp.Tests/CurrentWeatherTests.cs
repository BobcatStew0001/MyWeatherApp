using MyWeatherApp.Models;

namespace MyWeatherApp.Tests;

public class CurrentWeatherTests
{
    private static CurrentWeatherData MakeData(decimal? rain = null) => new()
    {
        CityName = "Pawnee",
        Temp = "72",
        FeelsLike = "70",
        Humidity = "55",
        WindSpeed = "8.5",
        Forecast = "Clear",
        Description = "clear sky",
        Icon = "01d",
        Rain = rain
    };

    [Fact]
    public void GetFormattedWeather_ContainsCityName()
    {
        var result = new CurrentWeather(MakeData()).GetFormattedWeather();
        Assert.Contains("Pawnee", result);
    }

    [Fact]
    public void GetFormattedWeather_ContainsTemperature()
    {
        var result = new CurrentWeather(MakeData()).GetFormattedWeather();
        Assert.Contains("72", result);
    }

    [Fact]
    public void GetFormattedWeather_ContainsHumidity()
    {
        var result = new CurrentWeather(MakeData()).GetFormattedWeather();
        Assert.Contains("55", result);
    }

    [Fact]
    public void GetFormattedWeather_ContainsFeelsLike()
    {
        var result = new CurrentWeather(MakeData()).GetFormattedWeather();
        Assert.Contains("70", result);
    }

    [Fact]
    public void GetFormattedWeather_ContainsDescription()
    {
        var result = new CurrentWeather(MakeData()).GetFormattedWeather();
        Assert.Contains("clear sky", result);
    }

    [Fact]
    public void GetFormattedWeather_WithRain_ShowsRainAmount()
    {
        var result = new CurrentWeather(MakeData(rain: 0.5m)).GetFormattedWeather();
        Assert.Contains("raining", result);
        Assert.Contains("0.5", result);
    }

    [Fact]
    public void GetFormattedWeather_WithoutRain_ShowsNoRainMessage()
    {
        var result = new CurrentWeather(MakeData(rain: null)).GetFormattedWeather();
        Assert.Contains("no rain", result);
    }
}
