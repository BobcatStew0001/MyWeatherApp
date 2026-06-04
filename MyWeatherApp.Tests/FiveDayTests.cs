using MyWeatherApp.Models;
using Newtonsoft.Json.Linq;

namespace MyWeatherApp.Tests;

public class FiveDayTests
{
    
    private static JObject MakeForecastItem(string dtTxt, double pop = 0.25)
    {
        return new JObject
        {
            ["dt_txt"] = dtTxt,
            ["main"] = new JObject
            {
                ["temp"] = 75.3,
                ["feels_like"] = 74.1,
                ["humidity"] = 60
            },
            ["weather"] = new JArray
            {
                new JObject
                {
                    ["main"] = "Clear",
                    ["description"] = "clear sky",
                    ["icon"] = "01d"
                }
            },
            ["wind"] = new JObject { ["speed"] = 5.5 },
            ["pop"] = pop
        };
    }

    [Fact]
    public void GetFormattedFiveDay_ExcludesNonMorningOrEveningTimes()
    {
        var data = new JArray
        {
            MakeForecastItem("2026-06-10 00:00:00"),
            MakeForecastItem("2026-06-10 06:00:00"),
            MakeForecastItem("2026-06-10 12:00:00"),
            MakeForecastItem("2026-06-10 15:00:00"),
        };

        var result = new FiveDay(data).GetFormattedFiveDay();

        Assert.Empty(result);
    }

    [Fact]
    public void GetFormattedFiveDay_Includes9amEntries()
    {
        var data = new JArray { MakeForecastItem("2026-06-10 09:00:00") };

        var result = new FiveDay(data).GetFormattedFiveDay();

        Assert.Single(result);
    }

    [Fact]
    public void GetFormattedFiveDay_Includes6pmEntries()
    {
        var data = new JArray { MakeForecastItem("2026-06-10 18:00:00") };

        var result = new FiveDay(data).GetFormattedFiveDay();

        Assert.Single(result);
    }

    [Fact]
    public void GetFormattedFiveDay_LabelsMorningFor9am()
    {
        var data = new JArray { MakeForecastItem("2026-06-10 09:00:00") };

        var result = new FiveDay(data).GetFormattedFiveDay();

        Assert.Contains("Morning", result[0].Date);
    }

    [Fact]
    public void GetFormattedFiveDay_LabelsEveningFor6pm()
    {
        var data = new JArray { MakeForecastItem("2026-06-10 18:00:00") };

        var result = new FiveDay(data).GetFormattedFiveDay();

        Assert.Contains("Evening", result[0].Date);
    }

    [Fact]
    public void GetFormattedFiveDay_DateIncludesMonthAndDay()
    {
        var data = new JArray { MakeForecastItem("2026-06-10 09:00:00") };

        var result = new FiveDay(data).GetFormattedFiveDay();

        Assert.Contains("June 10", result[0].Date);
    }

    [Fact]
    public void GetFormattedFiveDay_CalculatesChanceOfRain()
    {
        var data = new JArray { MakeForecastItem("2026-06-10 09:00:00", pop: 0.25) };

        var result = new FiveDay(data).GetFormattedFiveDay();

        Assert.Equal(25f, result[0].ChanceOfRain);
    }

    [Fact]
    public void GetFormattedFiveDay_ParsesAllFields()
    {
        var data = new JArray { MakeForecastItem("2026-06-10 09:00:00") };

        var item = new FiveDay(data).GetFormattedFiveDay()[0];

        Assert.Equal("75.3", item.Temp);
        Assert.Equal("74.1", item.FeelsLike);
        Assert.Equal("60", item.Humidity);
        Assert.Equal("Clear", item.Forecast);
        Assert.Equal("clear sky", item.Description);
        Assert.Equal("01d", item.Icon);
        Assert.Equal("5.5", item.WindSpeed);
    }
}