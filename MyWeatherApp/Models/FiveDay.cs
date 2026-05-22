using Newtonsoft.Json.Linq;

namespace MyWeatherApp.Models;

public class FiveDay
{
   private JArray _forecastdata;

   public FiveDay(JArray forecastData)
   {
      _forecastdata = forecastData;
   }

   public List<ForecastItem> GetFormattedFiveDay()
   {
      var data = new List<ForecastItem>();
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
         string[] splitDate = date.Split(' ');

         if (splitDate[1] == "09:00:00" || splitDate[1] == "18:00:00")
         {
            DateTime parsedDate = DateTime.Parse(date);
            string timeOfDay = splitDate[1] == "09:00:00" ? "Morning" : "Evening";
            string formattedDate = parsedDate.ToString("dddd, MMMM dd") + " " + timeOfDay;
            data.Add(new ForecastItem
            {
               Date = formattedDate, Temp = temp, FeelsLike = feelsLike, Forecast = forecast, Description = description,
               Humidity = humidity, WindSpeed = windSpeed, ChanceOfRain = chanceOfRain, Icon = icon
            });
         }
      }

      return data;
   }
}