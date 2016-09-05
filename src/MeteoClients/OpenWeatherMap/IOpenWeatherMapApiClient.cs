using System.Collections.Generic;
using System.Threading.Tasks;
using MeteoClients.OpenWeatherMap.Contracts;
using MeteoClients.OpenWeatherMap.Settings;

namespace MeteoClients.OpenWeatherMap
{
    public interface IOpenWeatherMapApiClient
    {
        string BaseUrl { get; }
        string ApiKey { get; }
        SearchAccuracy SearchAccuracy { get; set; }
        UnitsFormat UnitsFormat { get; set; }
        SupportedLanguage Language { get; set; }

        Task<OpenWeatherMapResponse> GetByCityAsync(string city, string countryCode = null);
        Task<string> GetByCityAsHtmlAsync(string city, string countryCode = null);
        Task<OpenWeatherMapResponse> GetByCityIdAsync(int cityId);
        Task<MultipleCitiesResponse> GetByCitiesIdsAsync(params int[] cityIds);
        Task<string> GetByCityIdAsHtmlAsync(int cityId);
        Task<OpenWeatherMapResponse> GetByCityCoordinatesAsync(float latitude, float longitude);
        Task<string> GetByCityCoordinatesAsHtmlAsync(float latitude, float longitude);
        Task<OpenWeatherMapResponse> GetByZipCodeAsync(int zipCode, string countryCode);
        Task<string> GetByZipCodeAsHtmlAsync(int zipCode, string countryCode);
        Task<CitiesInRectangleResponse> GetByRectangleAsync(float longitudeLeft, float latitudeBottom, float longitudeRight, float latitudeTop, bool useServerClustering, int count);
        Task<CitiesInCircleResponse> GetByCircleAsync(float centerLongitude, float centerLatitude, bool useServerClustering, int count);
    }
}