using Newtonsoft.Json.Linq;

namespace MyWeatherApp.Models;

public class HourlyDay:Weather
{
    public HourlyDay(string zip) : base(zip)
    {
    }
    public string GetHourlyData()//Second API call to get the weather data
    {
        var clientHttp = new HttpClient(); 
        var clientResponse = clientHttp.GetAsync($"https://pro.openweathermap.org/data/2.5/forecast/hourly?lat={myLat}&lon={myLon}&appid={apiKey}&units=imperial").Result;
        return clientResponse.Content.ReadAsStringAsync().Result;
    }
    
    public List<HourlyItem> GetFormattedHourly()
    {
       
            string hourlyData = GetHourlyData();
            var HourlyDay = JObject.Parse(hourlyData).GetValue("list");
            var data = new List<HourlyItem>();
            foreach (var item in HourlyDay)
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
                    {DateHourly = forecastTime, Temp = temp, FeelsLike = feelsLike, Forecast = forecast,
                        Description = description, Humidity = humidity, WindSpeed = windSpeed, Icon = icon,
                        RainHourly = rainHourly, ChanceOfRain = chanceOfRain 
                    });
                }



            }

            return data; 

    }
    
}