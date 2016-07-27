using MeteoClients.OpenWeatherMap.Settings;

namespace MeteoClients.OpenWeatherMap
{
    public class OpenWeatherMapOptions
    {
        public string BaseUrl { get; set; } = "http://api.openweathermap.org/data/2.5/weather";
        public string ApiKey { get; set; } = null;
        public SearchAccuracy SearchAccuracy { get; set; } = SearchAccuracy.Like;
        public UnitsFormat UnitsFormat { get; set; } = UnitsFormat.Kelvin;
        public SupportedLanguage Language { get; set; } = SupportedLanguage.English;
    }
}