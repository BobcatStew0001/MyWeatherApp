using Newtonsoft.Json.Linq;

namespace MyWeatherApp.Models;

public class RonSwanson
{
    public string GetRon()
    {
        var client = new HttpClient();
        var ronUrl = "https://ron-swanson-quotes.herokuapp.com/v2/quotes";
        var ronResponse = client.GetStringAsync(ronUrl).Result;
        var ron = JArray.Parse(ronResponse)[0];
        return ron.ToString();
    }
}