using Newtonsoft.Json.Linq;

namespace MyWeatherApp;

public class SevenDay:Weather
{
    public SevenDay(string zip) : base(zip)
    {
        // api.openweathermap.org/data/2.5/forecast/daily?lat={lat}&lon={lon}&cnt={cnt}&appid={API key}
        
    }
    protected int cnt = 7;

    public string GetSevenDayData()
    {
        var clientHttp = new HttpClient();
        var clientResponse = clientHttp.GetAsync($"http://api.openweathermap.org/data/2.5/forecast/daily?lat={myLat}&lon={myLon}&cnt={cnt}&appid={apiKey}").Result;
        return clientResponse.Content.ReadAsStringAsync().Result;
    }

    public string GetFormattedSevenDay()
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("Seven Day Forecast:");
        Console.ResetColor();

        string sevenDayData = GetSevenDayData();
        Console.WriteLine(sevenDayData);
        var sevenDayForecast = JObject.Parse(sevenDayData).GetValue("list");
        var data = new List<string>();

        foreach (var item in sevenDayForecast)
        {
            JObject listItem = (JObject)item;
            JObject listObject = (JObject)(listItem).GetValue("temp");
            JObject weatherObject = (JObject)(listItem).GetValue("weather")[0];
            string date = listItem.GetValue("dt").ToString();
            string dayTemp = listObject.GetValue("day").ToString();
            string forecast = weatherObject.Value<string>("main");
            string mornTemp = listObject.GetValue("morn").ToString();
            string eveTemp = listObject.GetValue("eve").ToString();
            string nightTemp = listObject.GetValue("night").ToString();
            data.Add($"The 7 day forecast for {date} in {myCity} will be as follows Day: {dayTemp} \n {forecast},\n Morning: {mornTemp},\n Noon:{eveTemp} \n Night: {nightTemp}.");
        }
        return string.Join( "\n", data);
    }
}