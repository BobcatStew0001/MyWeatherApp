using MyWeatherApp.Models;
using Newtonsoft.Json.Linq;

namespace MyWeatherApp.Tests;

public class HourlyDayTests
{
    private static JObject MakeHourlyItem(string dtTxt, bool includeRain = false, double pop = 0.5)
    {
        var item = new JObject
        {
            ["dt_txt"] = dtTxt,
            ["main"] = new JObject
            {
                ["temp"] = 68.5,
                ["feels_like"] = 67.5,
                ["humidity"] = 70
            },
            ["weather"] = new JArray
            {
                new JObject
                {
                    ["main"] = "Clouds",
                    ["description"] = "partly cloudy",
                    ["icon"] = "02d"
                }
            },
            ["wind"] = new JObject { ["speed"] = 7.2 },
            ["pop"] = pop
        };

        if (includeRain)
            item["rain"] = new JObject { ["1h"] = 0.25m };

        return item;
    }

    private static string Dt(DateTime dt) => dt.ToString("yyyy-MM-dd HH:mm:ss");

    [Fact]
    public void GetFormattedHourly_IncludesItemsWithinNext24Hours()
    {
        var data = new JArray { MakeHourlyItem(Dt(DateTime.Now.AddHours(2))) };

        var result = new HourlyDay(data).GetFormattedHourly();

        Assert.Single(result);
    }

    [Fact]
    public void GetFormattedHourly_ExcludesPastItems()
    {
        var data = new JArray { MakeHourlyItem(Dt(DateTime.Now.AddHours(-2))) };

        var result = new HourlyDay(data).GetFormattedHourly();

        Assert.Empty(result);
    }

    [Fact]
    public void GetFormattedHourly_ExcludesItemsBeyond24Hours()
    {
        var data = new JArray { MakeHourlyItem(Dt(DateTime.Now.AddHours(26))) };

        var result = new HourlyDay(data).GetFormattedHourly();

        Assert.Empty(result);
    }

    [Fact]
    public void GetFormattedHourly_ParsesAllFields()
    {
        var data = new JArray { MakeHourlyItem(Dt(DateTime.Now.AddHours(3)), pop: 0.5) };

        var item = new HourlyDay(data).GetFormattedHourly()[0];

        Assert.Equal("68.5", item.Temp);
        Assert.Equal("67.5", item.FeelsLike);
        Assert.Equal("70", item.Humidity);
        Assert.Equal("Clouds", item.Forecast);
        Assert.Equal("partly cloudy", item.Description);
        Assert.Equal("02d", item.Icon);
        Assert.Equal("7.2", item.WindSpeed);
        Assert.Equal(50f, item.ChanceOfRain);
        Assert.Null(item.RainHourly);
    }

    [Fact]
    public void GetFormattedHourly_ParsesOptionalRainField()
    {
        var data = new JArray { MakeHourlyItem(Dt(DateTime.Now.AddHours(3)), includeRain: true) };

        var result = new HourlyDay(data).GetFormattedHourly();

        Assert.Equal(0.25m, result[0].RainHourly);
    }

    [Fact]
    public void GetFormattedHourly_MixedItems_ReturnsOnlyWindowItems()
    {
        var data = new JArray
        {
            MakeHourlyItem(Dt(DateTime.Now.AddHours(-3))),  // past — excluded
            MakeHourlyItem(Dt(DateTime.Now.AddHours(2))),   // in window
            MakeHourlyItem(Dt(DateTime.Now.AddHours(10))),  // in window
            MakeHourlyItem(Dt(DateTime.Now.AddHours(30))),  // too far ahead — excluded
        };

        var result = new HourlyDay(data).GetFormattedHourly();

        Assert.Equal(2, result.Count);
    }
}