using MyWeatherApp.Models;

namespace MyWeatherApp.Tests;

public class CurrentWeatherDataTests
{
    [Fact]
    public void Properties_SetAndGet()
    {
        var data = new CurrentWeatherData
        {
            CityName = "Springfield",
            Temp = "80",
            FeelsLike = "78",
            Humidity = "50",
            WindSpeed = "10",
            Forecast = "Sunny",
            Description = "sunny sky",
            Icon = "01d",
            Rain = 1.5m
        };

        Assert.Equal("Springfield", data.CityName);
        Assert.Equal("80", data.Temp);
        Assert.Equal("78", data.FeelsLike);
        Assert.Equal("50", data.Humidity);
        Assert.Equal("10", data.WindSpeed);
        Assert.Equal("Sunny", data.Forecast);
        Assert.Equal("sunny sky", data.Description);
        Assert.Equal("01d", data.Icon);
        Assert.Equal(1.5m, data.Rain);
    }

    [Fact]
    public void Rain_DefaultsToNull()
    {
        var data = new CurrentWeatherData();
        Assert.Null(data.Rain);
    }
}

public class ForecastItemTests
{
    [Fact]
    public void Properties_SetAndGet()
    {
        var item = new ForecastItem
        {
            Date = "Wednesday, June 10 Morning",
            Temp = "75",
            FeelsLike = "73",
            Forecast = "Rain",
            Description = "light rain",
            WindSpeed = "6.1",
            Humidity = "80",
            Icon = "10d",
            ChanceOfRain = 75f
        };

        Assert.Equal("Wednesday, June 10 Morning", item.Date);
        Assert.Equal("75", item.Temp);
        Assert.Equal("73", item.FeelsLike);
        Assert.Equal("Rain", item.Forecast);
        Assert.Equal("light rain", item.Description);
        Assert.Equal("6.1", item.WindSpeed);
        Assert.Equal("80", item.Humidity);
        Assert.Equal("10d", item.Icon);
        Assert.Equal(75f, item.ChanceOfRain);
    }
}

public class HourlyItemTests
{
    [Fact]
    public void Temp_DefaultsToEmptyString()
    {
        var item = new HourlyItem();
        Assert.Equal(string.Empty, item.Temp);
    }

    [Fact]
    public void RainHourly_IsNullable()
    {
        var item = new HourlyItem { RainHourly = 0.5m };
        Assert.Equal(0.5m, item.RainHourly);

        item.RainHourly = null;
        Assert.Null(item.RainHourly);
    }

    [Fact]
    public void ChanceOfRain_IsNullable()
    {
        var item = new HourlyItem { ChanceOfRain = 50f };
        Assert.Equal(50f, item.ChanceOfRain);

        item.ChanceOfRain = null;
        Assert.Null(item.ChanceOfRain);
    }

    [Fact]
    public void Properties_SetAndGet()
    {
        var now = DateTime.Now;
        var item = new HourlyItem
        {
            DateHourly = now,
            Temp = "65",
            FeelsLike = "63",
            Forecast = "Cloudy",
            Description = "overcast clouds",
            WindSpeed = "4.0",
            Humidity = "75",
            Icon = "04d",
            ChanceOfRain = 25f,
            RainHourly = null
        };

        Assert.Equal(now, item.DateHourly);
        Assert.Equal("65", item.Temp);
        Assert.Equal("63", item.FeelsLike);
        Assert.Equal("Cloudy", item.Forecast);
        Assert.Equal("overcast clouds", item.Description);
        Assert.Equal("4.0", item.WindSpeed);
        Assert.Equal("75", item.Humidity);
        Assert.Equal("04d", item.Icon);
        Assert.Equal(25f, item.ChanceOfRain);
        Assert.Null(item.RainHourly);
    }
}