using System.Threading.Tasks;

namespace MeteoClients.OpenWeatherMap
{
    public interface IOpenWeatherMapApiClient
    {
        Task<OpenWeatherMapResponse> GetByCityAsync(string city, string countryCode = null);
        Task<string> GetByCityAsHtmlAsync(string city, string countryCode = null);
        Task<OpenWeatherMapResponse> GetByCityIdAsync(int cityId);
        Task<string> GetByCityIdAsHtmlAsync(int cityId);
        Task<OpenWeatherMapResponse> GetByCityCoordinatesAsync(float latitude, float longitude);
        Task<string> GetByCityCoordinatesAsHtmlAsync(float latitude, float longitude);
        Task<OpenWeatherMapResponse> GetByZipCodeAsync(int zipCode, string countryCode);
        Task<string> GetByZipCodeAsHtmlAsync(int zipCode, string countryCode);
    }
}