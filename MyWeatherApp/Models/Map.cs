namespace MyWeatherApp.Models;

public class Map:WeatherService 
{
    protected string layer = "precipitation";
    protected int z = 8;
    protected int x = 400;
    protected int y = 300;
    public Map(string zip) : base(zip)
    {
    }
    public string GetLat()
    {
        return myLat;
    }
    public string GetLon()
    {
        return myLon;
    }
    public string GetApiKey()
    {
        return apiKey;
    }
}