using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Newtonsoft.Json.Linq;
using System;

namespace MyWeatherApp;

public class Weather
{
    protected string myCity;
    protected string myLat;
    protected string myLon;
    protected string geoResponse;
    protected string apiKey;

    public Weather(string zip)
    {
        IConfigurationRoot config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        apiKey = config["key"];  
        myCity = zip;
        geoResponse = GetWeather();
        myLat = JObject.Parse(geoResponse).GetValue("lat").ToString();
        myLon = JObject.Parse(geoResponse).GetValue("lon").ToString();
          
    }

     
    public static string GetCity()
    {
        Console.WriteLine("Enter your zip code:");
        return Console.ReadLine();
    }

    

  


    public string GetWeather()//Make the API call
    {
        var clientHttp = new HttpClient(); 
        var clientResponse = clientHttp.GetAsync($"https://api.openweathermap.org/geo/1.0/zip?zip={myCity},US&appid={apiKey}&units=imperial").Result;
        return clientResponse.Content.ReadAsStringAsync().Result;
    }

}