using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MeteoClients.Extensions;
using MeteoClients.OpenWeatherMap.Contracts;
using MeteoClients.OpenWeatherMap.Settings;
using Newtonsoft.Json;

namespace MeteoClients.OpenWeatherMap
{
    public class OpenWeatherMapApiClient : IOpenWeatherMapApiClient
    {
        public OpenWeatherMapApiClient(string apiKey, string baseUrl = "http://api.openweathermap.org/data/2.5")
        {
            if (string.IsNullOrWhiteSpace(apiKey))
            {
                throw new ArgumentNullException(nameof(apiKey));
            }

            if (string.IsNullOrWhiteSpace(baseUrl))
            {
                throw new ArgumentNullException(nameof(baseUrl));
            }

            ApiKey = apiKey;
            BaseUrl = baseUrl;
        }

        public string BaseUrl { get; }
        public string ApiKey { get; }
        public SearchAccuracy SearchAccuracy { get; set; } = SearchAccuracy.Like;
        public UnitsFormat UnitsFormat { get; set; } = UnitsFormat.Kelvin;
        public SupportedLanguage Language { get; set; } = SupportedLanguage.English;

        public async Task<OpenWeatherMapResponse> GetByCityAsync(string city, string countryCode = null)
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                throw new ArgumentNullException(nameof(city));
            }

            var url = ApplyOptionsToQuery(GetWeatherByCityUrl(city, countryCode));
            return await GetWeatherAsync<OpenWeatherMapResponse>(url);
        }

        public async Task<string> GetByCityAsHtmlAsync(string city, string countryCode = null)
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                throw new ArgumentNullException(nameof(city));
            }

            var url = ApplyOptionsToQuery(GetWeatherByCityUrl(city, countryCode));
            return await GetWeatherAsHtmlAsync(url);
        }

        public async Task<OpenWeatherMapResponse> GetByCityIdAsync(int cityId)
        {
            var url = ApplyOptionsToQuery(GetWeatherByCityIdUrl(cityId));
            return await GetWeatherAsync<OpenWeatherMapResponse>(url);
        }

        public async Task<MultipleCitiesResponse> GetByCitiesIdsAsync(params int[] cityIds)
        {
            var url = ApplyOptionsToQuery(GetWeatherByCitiesIdsUrl(cityIds));
            return await GetWeatherAsync<MultipleCitiesResponse>(url, Encoding.UTF8);
        }

        public async Task<string> GetByCityIdAsHtmlAsync(int cityId)
        {
            var url = ApplyOptionsToQuery(GetWeatherByCityIdUrl(cityId));
            return await GetWeatherAsHtmlAsync(url);
        }

        public async Task<OpenWeatherMapResponse> GetByCityCoordinatesAsync(float latitude, float longitude)
        {
            var url = ApplyOptionsToQuery(GetWeatherByCityCoordinatesUrl(latitude, longitude));
            return await GetWeatherAsync<OpenWeatherMapResponse>(url);
        }

        public async Task<string> GetByCityCoordinatesAsHtmlAsync(float latitude, float longitude)
        {
            var url = ApplyOptionsToQuery(GetWeatherByCityCoordinatesUrl(latitude, longitude));
            return await GetWeatherAsHtmlAsync(url);
        }

        public async Task<OpenWeatherMapResponse> GetByZipCodeAsync(int zipCode, string countryCode)
        {
            if (string.IsNullOrWhiteSpace(countryCode))
            {
                throw new ArgumentNullException(nameof(countryCode));
            }

            var url = ApplyOptionsToQuery(GetWeatherByZipCodeUrl(zipCode, countryCode));
            return await GetWeatherAsync<OpenWeatherMapResponse>(url);
        }

        public async Task<string> GetByZipCodeAsHtmlAsync(int zipCode, string countryCode)
        {
            if (string.IsNullOrWhiteSpace(countryCode))
            {
                throw new ArgumentNullException(nameof(countryCode));
            }

            var url = ApplyOptionsToQuery(GetWeatherByZipCodeUrl(zipCode, countryCode));
            return await GetWeatherAsHtmlAsync(url);
        }

        public async Task<CitiesInRectangleResponse> GetByRectangleAsync(float longitudeLeft, float latitudeBottom, float longitudeRight, float latitudeTop, 
            bool useServerClustering, int count = 10)
        {
            throw new NotImplementedException("check long/lat order");
            var url = ApplyOptionsToQuery(GetWeatherGetByRectangleZoneUrl(longitudeLeft, latitudeBottom, longitudeRight, latitudeTop, useServerClustering, count));
            return await GetWeatherAsync<CitiesInRectangleResponse>(url, Encoding.UTF8);
        }

        public async Task<CitiesInCircleResponse> GetByCircleAsync(float centerLongitude, float centerLatitude, bool useServerClustering, int count)
        {
            var url = ApplyOptionsToQuery(GetWeatherGetByCircleUrl(centerLongitude, centerLatitude, useServerClustering, count));
            return await GetWeatherAsync<CitiesInCircleResponse>(url, Encoding.UTF8);
        }

        

        private static async Task<T> GetWeatherAsync<T>(string url, Encoding encoding = null) where T: class
        {

            var message = await new HttpClient().GetAsync(url).ConfigureAwait(false);
            message.EnsureSuccessStatusCode();

            string response = null;
            if (encoding == null)
            {
                response = await message.Content.ReadAsStringAsync().ConfigureAwait(false);
            }
            else
            {
                response = encoding.GetString(await message.Content.ReadAsByteArrayAsync().ConfigureAwait(false));
            }

            return JsonConvert.DeserializeObject<T>(response);
        }

        private static async Task<string> GetWeatherAsHtmlAsync(string url, Encoding encoding = null)
        {
            var message = await new HttpClient().GetAsync($"{url}&mode=html").ConfigureAwait(false);
            message.EnsureSuccessStatusCode();

            if (encoding == null)
            {
                return await message.Content.ReadAsStringAsync().ConfigureAwait(false);
            }

            return encoding.GetString(await message.Content.ReadAsByteArrayAsync().ConfigureAwait(false));
        }

        private string GetWeatherByCityUrl(string city, string countryCode)
        {
            var url = $"{BaseUrl}/weather?q={city}";

            if (!string.IsNullOrWhiteSpace(countryCode))
            {
                url = $"{url},{countryCode}";
            }

            return url;
        }

        private string GetWeatherByCityIdUrl(int cityId)
        {
            var url = $"{BaseUrl}/weather?id={cityId}";
            return url;
        }

        private string GetWeatherByCitiesIdsUrl(params int[] cityIds)
        {
            var ids = string.Empty;
            if (cityIds != null && cityIds.Any())
            {
                ids = cityIds.Select(x => x.ToString()).Aggregate((f, s) => $"{f},{s}");
            }
            var url = $"{BaseUrl}/group?id={ids}";
            return url;
        }

        private string GetWeatherByCityCoordinatesUrl(float latitude, float longitude)
        {
            var url = $"{BaseUrl}/weather?lat={latitude.Invariant()}&lon={longitude.Invariant()}";
            return url;
        }

        private string GetWeatherByZipCodeUrl(int zipCode, string countryCode)
        {
            var url = $"{BaseUrl}/weather?zip={zipCode},{countryCode}";
            return url;
        }

        private string GetWeatherGetByRectangleZoneUrl(float longitudeLeft, float latitudeBottom, float longitudeRight, float latitudeTop,
            bool useServerClustering, int count = 10)
        {
            var cluster = useServerClustering ? "yes" : "no";
            var url = $"{BaseUrl}/box/city?bbox={longitudeLeft.Invariant()},{latitudeBottom.Invariant()},{longitudeRight.Invariant()},{latitudeTop.Invariant()},{count}&cluster={cluster}";
            return url;
        }

        private string GetWeatherGetByCircleUrl(float centerLongitude, float centerLatitude, bool useServerClustering, int count)
        {
            var cluster = useServerClustering ? "yes" : "no";
            var url = $"{BaseUrl}/find?lat={centerLongitude.Invariant()}&lon={centerLatitude.Invariant()}&cnt={count}&cluster={cluster}";
            return url;
        }

        private string ApplyOptionsToQuery(string url)
        {
            var temp = $"{url}&appid={ApiKey}";

            temp = ApplySearchAccuracyOptions(temp);

            temp = ApplyUnitsOptions(temp);

            temp = ApplyLanguageOptions(temp);

            return temp;
        }

        private string ApplySearchAccuracyOptions(string url)
        {
            var temp = url;

            if (SearchAccuracy == SearchAccuracy.Accurate)
            {
                temp = $"{temp}&type=accurate";
            }

            return temp;
        }

        private string ApplyUnitsOptions(string url)
        {
            var temp = url;

            switch (UnitsFormat)
            {
                case UnitsFormat.Kelvin:
                    break;
                case UnitsFormat.Fahrenheit:
                    temp = $"{temp}&units=imperial";
                    break;
                case UnitsFormat.Celsius:
                    temp = $"{temp}&units=metric";
                    break;
            }

            return temp;
        }

        private string ApplyLanguageOptions(string url)
        {
            var temp = url;

            switch (Language)
            {
                case SupportedLanguage.English:
                    break;
                case SupportedLanguage.Russia:
                    temp = $"{temp}&lang=ru";
                    break;
                case SupportedLanguage.Italian:
                    temp = $"{temp}&lang=it";
                    break;
                case SupportedLanguage.Spanish:
                    temp = $"{temp}&lang=es";
                    break;
                case SupportedLanguage.Ukrainian:
                    temp = $"{temp}&lang=ua";
                    break;
                case SupportedLanguage.German:
                    temp = $"{temp}&lang=de";
                    break;
                case SupportedLanguage.Portuguese:
                    temp = $"{temp}&lang=pt";
                    break;
                case SupportedLanguage.Romanian:
                    temp = $"{temp}&lang=ro";
                    break;
                case SupportedLanguage.Polish:
                    temp = $"{temp}&lang=pl";
                    break;
                case SupportedLanguage.Finnish:
                    temp = $"{temp}&lang=fi";
                    break;
                case SupportedLanguage.Dutch:
                    temp = $"{temp}&lang=nl";
                    break;
                case SupportedLanguage.French:
                    temp = $"{temp}&lang=fr";
                    break;
                case SupportedLanguage.Bulgarian:
                    temp = $"{temp}&lang=bg";
                    break;
                case SupportedLanguage.Swedish:
                    temp = $"{temp}&lang=sv";
                    break;
                case SupportedLanguage.ChineseTraditional:
                    temp = $"{temp}&lang=zh_tw";
                    break;
                case SupportedLanguage.ChineseSimplified:
                    temp = $"{temp}&lang=zh_cn";
                    break;
                case SupportedLanguage.Turkish:
                    temp = $"{temp}&lang=tr";
                    break;
                case SupportedLanguage.Croatian:
                    temp = $"{temp}&lang=hr";
                    break;
                case SupportedLanguage.Catalan:
                    temp = $"{temp}&lang=ca";
                    break;
            }

            return temp;
        }
    }
}