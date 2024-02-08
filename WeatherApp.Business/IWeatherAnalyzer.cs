using WeatherApp.Model.Response;

namespace WeatherApp.Business
{
    public interface IWeatherAnalyzer
    {

        Task AnalyzeWeather(string zipcode);
        //string CanIGoOutside(CurrentWeather currentWeather);

        //string ShouldIWearSunscreen(CurrentWeather currentWeather);

        //string CanIFlyKite(CurrentWeather currentWeather);
    }
}
