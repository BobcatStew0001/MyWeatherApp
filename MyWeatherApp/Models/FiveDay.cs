using Newtonsoft.Json.Linq;

namespace MyWeatherApp.Models;

public class FiveDay:Weather
{
    public FiveDay(string zip) : base(zip)
    {
    }

    public string GetFiveDayData()
    {
        var clientHttp = new HttpClient();
        var clientResponse = clientHttp.GetAsync($"https://api.openweathermap.org/data/2.5/forecast?lat={myLat}&lon={myLon}&appid={apiKey}&units=imperial").Result;
        return clientResponse.Content.ReadAsStringAsync().Result;
    }

    public List<ForecastItem> GetFormattedFiveDay()
    {
        string fiveDayData = GetFiveDayData();
        var fiveDayForecast = JObject.Parse(fiveDayData).GetValue("list");
        var data = new List<ForecastItem>();
        foreach (var item in fiveDayForecast)
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
            string[] splitDate = date.Split(' ');
            if (splitDate[1] == "12:00:00")
            {
                data.Add(new ForecastItem
                {
                    Date = date, Temp = temp, FeelsLike = feelsLike, Forecast = forecast, Description = description,
                    Humidity = humidity, WindSpeed = windSpeed, ChanceOfRain = chanceOfRain
                });
            }



        }

        return data; 

    }
}