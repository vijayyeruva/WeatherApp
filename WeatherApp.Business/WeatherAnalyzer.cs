using WeatherApp.Data;
using WeatherApp.Model.Response;

namespace WeatherApp.Business
{
    public class WeatherAnalyzer : IWeatherAnalyzer
    {
        private readonly IWeatherDataProvider _weatherDataProvider;

        public WeatherAnalyzer(IWeatherDataProvider weatherDataProvider)
        {
            _weatherDataProvider = weatherDataProvider;
        }

        public async Task AnalyzeWeather(string zipcode)
        {
            try
            {
                var weatherData = await _weatherDataProvider.GetWeatherData(zipcode);

                if (weatherData != null)
                {
                    // Process weather data and provide answers to questions
                    Console.WriteLine("Should I go outside?");
                    Console.WriteLine(weatherData.current.weather_descriptions[0].Contains("rain") ? "No" : "Yes");

                    Console.WriteLine("Should I wear sunscreen?");
                    Console.WriteLine(weatherData.current.uv_index > 3 ? "Yes" : "No");

                    Console.WriteLine("Can I fly my kite?");
                    Console.WriteLine(!weatherData.current.weather_descriptions[0].Contains("rain") && weatherData.current.wind_speed > 15 ? "Yes" : "No");
                }
                else
                {
                    Console.WriteLine("Failed to retrieve weather data for the provided zipcode.");
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error: Unable to connect to the weather data provider. {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }
    }
}
