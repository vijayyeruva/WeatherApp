using System.Text.Json;
using WeatherApp.Model.Response;

namespace WeatherApp.Data
{
    public class WeatherDataProvider : IWeatherDataProvider
    {
        private readonly string _weatherApiBaseUrl ;
        private readonly string _apiKey;

        public WeatherDataProvider(string apiKey,string weatherApiBaseUrl)
        {
            _apiKey = apiKey;
            _weatherApiBaseUrl = weatherApiBaseUrl;
        }
        public async Task<WeatherData> GetWeatherData(string zipcode)
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    string url = $"{_weatherApiBaseUrl}?access_key={_apiKey}&query={zipcode}";
                    var response = await httpClient.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonString = await response.Content.ReadAsStringAsync();
                        var weatherData = JsonSerializer.Deserialize<WeatherData>(jsonString);
                        return weatherData;
                    }
                    else
                    {
                        Console.WriteLine($"Failed to retrieve weather data. Status Code: {response.StatusCode}");
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while retrieving weather data: {ex.Message}");
                    return null;
                }
            }
        }
    }
}
