using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Newtonsoft.Json.Linq;
using System;
namespace MyWeatherApp;

public class CurrentWeather:Weather
{
    public CurrentWeather(string zip) : base(zip)
    {
    }

    public string GetWeatherData()//Second API call to get the weather data
    {
        var clientHttp = new HttpClient(); 
        var clientResponse = clientHttp.GetAsync($"https://api.openweathermap.org/data/2.5/weather?lat={myLat}&lon={myLon}&appid={apiKey}&units=imperial").Result;
        return clientResponse.Content.ReadAsStringAsync().Result;
    }
    
    public string GetFormattedWeather()
    {
        var client = new HttpClient();
        var ronUrl = "https://ron-swanson-quotes.herokuapp.com/v2/quotes";
        var ronResponse = client.GetStringAsync(ronUrl).Result;
        var ron = JArray.Parse(ronResponse)[0];
        
        Console.WriteLine("Welcome to Zach's Weather App");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"Quote of the Day: {ron}");
        Console.ResetColor();
        string? cityName = JObject.Parse(geoResponse).GetValue("name").ToString();
        string weatherDataResponse =
            GetWeatherData();
        JObject mainObject =
            (JObject)JObject.Parse(weatherDataResponse).GetValue("main");
        
        string? tempObject =
            mainObject.GetValue("temp").ToString();
        
        string? humidityObject =
            mainObject.GetValue("humidity").ToString();
        
        string? feelsLikeObject =
            mainObject.GetValue("feels_like").ToString();

        var weatherObject = JObject.Parse(weatherDataResponse).GetValue("weather")[0];
        var forecast = weatherObject.Value<string>("main");
        var description = weatherObject.Value<string>("description");

        
        
        return $"The weather forecast for {cityName} today is {forecast}. \n {cityName} will see {description} with a temperature of {tempObject} degrees and a feels like temperature of {feelsLikeObject} degrees,\n with a humidity of {humidityObject}%.";
    }
}