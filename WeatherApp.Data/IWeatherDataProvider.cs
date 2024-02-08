using WeatherApp.Model.Response;

namespace WeatherApp.Data
{
    public interface IWeatherDataProvider
    {
        Task<WeatherData> GetWeatherData(string zipcode);
    }
}
