using Newtonsoft.Json.Linq;

namespace MyWeatherApp.Models;

public class RonSwanson
{
    public string GetRon()
    {
        try
        {
            var client = new HttpClient();
            var ronUrl = "https://ron-swanson-quotes.herokuapp.com/v2/quotes";
            var ronResponse = client.GetStringAsync(ronUrl).Result;
            var ron = JArray.Parse(ronResponse)[0];
            return ron.ToString();
        }
        catch
        {
            return "Give me all the bacon and eggs you have."; 
        }
    }
}