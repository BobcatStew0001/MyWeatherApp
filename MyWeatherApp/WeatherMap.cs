namespace MyWeatherApp;

public class WeatherMap:Weather //Need a subscription to get this data
{
    protected string layer = "precipitation";
    protected int z = 8;
    protected int x = 400;
    protected int y = 300;
    public WeatherMap(string zip) : base(zip)
    {
    }
    public string GetMap()
    {
        var clientHttp = new HttpClient();
        var clientResponse = clientHttp.GetAsync($"https://tile.openweathermap.org/map/{layer}/{z}/{x}/{y}.png?appid={apiKey}").Result;
        return clientResponse.Content.ReadAsStringAsync().Result;
    }

    public string GetFormattedMap()
    {
        return GetMap(); //Placeholder to prevent errors
    }
}