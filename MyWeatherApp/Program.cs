using System; 
namespace MyWeatherApp;
public class Program
{
    public static void Main(string[] args)
    {
        string zip = Weather.GetCity();
        CurrentWeather currentWeather = new CurrentWeather(zip);
        FiveDay fiveDay = new FiveDay(zip);
        SixteenForecast sixteenForecast = new SixteenForecast(zip);
        //Console.WriteLine(fiveDay.GetFormattedFiveDay());
       Console.WriteLine(sixteenForecast.GetFormattedSevenDay());
       
    }
}