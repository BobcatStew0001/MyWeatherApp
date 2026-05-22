using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Newtonsoft.Json.Linq;
using System;

namespace MyWeatherApp.Models;

public class WeatherService
{
    protected string myCity;
    protected string myLat;
    protected string myLon;
    protected string geoResponse;
    protected string apiKey;

    public WeatherService(string zip)
    {
        IConfigurationRoot config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        apiKey = config["key"];  
        myCity = zip;
        geoResponse = GetGeoData();
        myLat = JObject.Parse(geoResponse).GetValue("lat").ToString();
        myLon = JObject.Parse(geoResponse).GetValue("lon").ToString();
          
    }
    public string GetGeoData()//Make the API call
    {
        var clientHttp = new HttpClient(); 
        var clientResponse = clientHttp.GetAsync($"https://api.openweathermap.org/geo/1.0/zip?zip={myCity},US&appid={apiKey}&units=imperial").Result;
        return clientResponse.Content.ReadAsStringAsync().Result;
    }
   

    public CurrentWeatherData GetWeatherData()
    {
        var clientHttp = new HttpClient(); 
        var clientResponse = clientHttp.GetAsync($"https://api.openweathermap.org/data/2.5/weather?lat={myLat}&lon={myLon}&appid={apiKey}&units=imperial").Result;
        string weatherDataResponse = clientResponse.Content.ReadAsStringAsync().Result;
        
        string? cityName = JObject.Parse(geoResponse).GetValue("name").ToString();
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
        var icon = weatherObject.Value<string>("icon");
        var weatherJson = JObject.Parse(weatherDataResponse);
        decimal? rain = weatherJson["rain"]?["1h"]?.Value<decimal>();
        return new CurrentWeatherData
        {
            CityName = cityName,
            Temp = tempObject,
            Humidity = humidityObject,
            FeelsLike = feelsLikeObject,
            Forecast = forecast,
            Description = description,
            Icon = icon,
            Rain = rain
        };

    }
    public JArray GetForecastData()
    {
        var clientHttp = new HttpClient(); 
        var clientResponse = clientHttp.GetAsync($"https://api.openweathermap.org/data/2.5/forecast?lat={myLat}&lon={myLon}&appid={apiKey}&units=imperial").Result;
        string forecastResponse = clientResponse.Content.ReadAsStringAsync().Result;
        return (JArray)JObject.Parse(forecastResponse).GetValue("list");
    }
   
    
}