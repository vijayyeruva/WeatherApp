using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WeatherApp.Business;
using WeatherApp.Data;

namespace WeatherApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {

            // Setup configuration
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();


            // Setup DI container
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IWeatherDataProvider>(provider =>
                {
                    // Retrieve API key from configuration or any other source
                    string apiKey = configuration["WeatherStackApiKey"];
                    string endPoint = configuration["WeatherEndPoint"];
                    return new WeatherDataProvider(apiKey,endPoint);
                })
                .AddSingleton<IWeatherAnalyzer,WeatherAnalyzer>()
                .BuildServiceProvider();

            Console.WriteLine("Welcome to the Weather App!");

            // Prompt user for zipcode
            Console.Write("Enter your zipcode: ");
            string zipcode = Console.ReadLine();

            // Resolve the WeatherAnalyzer service from the DI container
            var weatherAnalyzer = serviceProvider.GetRequiredService<IWeatherAnalyzer>();


            
            // Perform weather analysis
            await weatherAnalyzer.AnalyzeWeather(zipcode);

            Console.Read();
        }
    }
}
