using System; 
namespace MyWeatherApp;
public class Program
{
    public static void Main(string[] args)
    {
        string zip = Weather.GetCity();
        FiveDay fiveDay = new FiveDay(zip);
        //SevenDay sevenDay = new SevenDay(zip);
        CurrentWeather current = new CurrentWeather(zip);
        Console.WriteLine(current.GetFormattedWeather());
        Console.WriteLine(fiveDay.GetFormattedFiveDay());
       //Console.WriteLine(sevenDay.GetFormattedSevenDay());
             
    }
}