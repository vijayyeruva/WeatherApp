namespace WeatherApp.Model.Response
{
    public class WeatherData
    {
        public Request request { get; set; }
        public Location location { get; set; }
        public CurrentWeather current { get; set; }
    }
}
