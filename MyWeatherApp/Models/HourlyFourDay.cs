namespace MyWeatherApp.Models;

public class HourlyFourDay:Weather
{
    public HourlyFourDay(string zip) : base(zip)
    {
    }
    public string GetWeatherData()//Second API call to get the weather data
    {
        var clientHttp = new HttpClient(); 
        var clientResponse = clientHttp.GetAsync($"https://pro.openweathermap.org/data/2.5/forecast/hourly?lat={myLat}&lon={myLon}&appid={apiKey}").Result;
        return clientResponse.Content.ReadAsStringAsync().Result;
    }
    
    public string GetFormattedHourly()
    {
        string hourlyData = GetWeatherData();
        return hourlyData;
    }
}