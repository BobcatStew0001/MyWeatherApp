using Newtonsoft.Json.Linq;

namespace MyWeatherApp.Models;

public class HourlyDay
{
    private JArray _forecastdata;

    public HourlyDay(JArray forecastData)
    {
        _forecastdata = forecastData;
    }

    public List<HourlyItem> GetFormattedHourly()
    {
        var data = new List<HourlyItem>();
        foreach (var item in _forecastdata)
        {
            JObject mainItem = (JObject)item;
            JObject mainObject = (JObject)(mainItem).GetValue("main");
            string date = mainItem.GetValue("dt_txt").ToString();
            string temp = mainObject.GetValue("temp").ToString();
            string feelsLike = mainObject.GetValue("feels_like").ToString();
            string forecast = mainItem.GetValue("weather")[0].Value<string>("main");
            string description = mainItem.GetValue("weather")[0].Value<string>("description");
            string humidity = mainObject.GetValue("humidity").ToString();
            JObject windObject = (JObject)(mainItem).GetValue("wind");
            string windSpeed = windObject.GetValue("speed").ToString();
            float chanceOfRain = (float)mainItem.GetValue("pop") * 100;
            string icon = mainItem.GetValue("weather")[0].Value<string>("icon");
            decimal? rainHourly = mainItem["rain"]?["1h"]?.Value<decimal>();
            DateTime forecastTime = DateTime.Parse(date);
            if (forecastTime >= DateTime.Now && forecastTime <= DateTime.Now.AddHours(24))
            {
                data.Add(new HourlyItem
                {
                    DateHourly = forecastTime, Temp = temp, FeelsLike = feelsLike, Forecast = forecast,
                    Description = description, Humidity = humidity, WindSpeed = windSpeed, Icon = icon,
                    RainHourly = rainHourly, ChanceOfRain = chanceOfRain
                });
            }
        }
        return data;
    }
}