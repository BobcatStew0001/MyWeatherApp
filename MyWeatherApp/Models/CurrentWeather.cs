namespace MyWeatherApp.Models;

public class CurrentWeather
{
   private CurrentWeatherData _weatherData;
   public CurrentWeather(CurrentWeatherData weatherData)
   {
      _weatherData = weatherData; 
   }
   
   public string GetFormattedWeather()
   {
      string rainInfo = _weatherData.Rain != null ? 
         $"It is raining with {_weatherData.Rain} inches of rain." 
         : "Currently there is no rain in sight.";
      return $"The weather forecast for {_weatherData.CityName} is {_weatherData.Description} with a temperature of {_weatherData.Temp} \n" +
             $"and a humidity of {_weatherData.Humidity}.The feels like temperature is {_weatherData.FeelsLike}.\n"
             + $"The wind speed is {_weatherData.WindSpeed}. {rainInfo}";
   }
}