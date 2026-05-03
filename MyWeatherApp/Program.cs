using System; 
namespace MyWeatherApp;
public class Program
{
    public static void Main(string[] args)
    {
        string zip = Weather.GetCity();
        CurrentWeather currentWeather = new CurrentWeather(zip);
        FiveDay fiveDay = new FiveDay(zip);
        Console.WriteLine(currentWeather.GetFormattedWeather());
        Console.WriteLine(fiveDay.GetFormattedFiveDay());
    }
}