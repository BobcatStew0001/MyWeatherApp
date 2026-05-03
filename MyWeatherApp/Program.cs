using System; 
namespace MyWeatherApp;
public class Program
{
    public static void Main(string[] args)
    {
        string zip = Weather.GetCity();
        CurrentWeather currentWeather = new CurrentWeather(zip);
        FiveDay fiveDay = new FiveDay(zip);
        SevenDay sevenDay = new SevenDay(zip);
        //Console.WriteLine(fiveDay.GetFormattedFiveDay());
       Console.WriteLine(sevenDay.GetFormattedSevenDay());
       
    }
}