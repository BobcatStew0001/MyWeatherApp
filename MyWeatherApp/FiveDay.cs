using Newtonsoft.Json.Linq;

namespace MyWeatherApp;

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

    public string GetFormattedFiveDay()
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("Five Day Forecast:");
        Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine($"Here is your 5 day forecast for {myCity}:");
        Console.ResetColor();
        string fiveDayData = GetFiveDayData();
        var fiveDayForecast = JObject.Parse(fiveDayData).GetValue("list");
        var data = new List<string>();
        foreach (var item in fiveDayForecast)
        {
            JObject mainItem = (JObject)item;
            JObject mainObject = (JObject)(mainItem).GetValue("main");
            string date = mainItem.GetValue("dt_txt").ToString();
            string temp = mainObject.GetValue("day").ToString();
            string feelsLike = mainObject.GetValue("feels_like").ToString();
            string forecast = mainItem.GetValue("weather")[0].Value<string>("main");
            string description = mainItem.GetValue("weather")[0].Value<string>("description");
            string humidity = mainObject.GetValue("humidity").ToString();
            JObject windObject = (JObject)(mainItem).GetValue("wind");
            string windSpeed = windObject.GetValue("speed").ToString();
            data.Add($"The 5 day forecast for {date} in {myCity} will be as follows {temp} \n {feelsLike},\n {forecast},\n {humidity},\n {windSpeed}.");
             
        }
        
        return string.Join( "\n", data);
            
    }
}