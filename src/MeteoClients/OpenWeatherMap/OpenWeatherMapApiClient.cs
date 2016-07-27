using System;
using System.Net.Http;
using System.Threading.Tasks;
using MeteoClients.Extensions;
using MeteoClients.OpenWeatherMap.Settings;
using Newtonsoft.Json;

namespace MeteoClients.OpenWeatherMap
{
    public class OpenWeatherMapApiClient : IOpenWeatherMapApiClient
    {
        private readonly OpenWeatherMapOptions _options;

        public OpenWeatherMapApiClient(OpenWeatherMapOptions options)
        {
            ValidateSettings(options);

            _options = options;
        }

        private void ValidateSettings(OpenWeatherMapOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (string.IsNullOrWhiteSpace(options.BaseUrl))
            {
                throw new ArgumentNullException(nameof(options.BaseUrl));
            }

            if (string.IsNullOrWhiteSpace(options.ApiKey))
            {
                throw new ArgumentNullException(nameof(options.ApiKey));
            }
        }

        public async Task<OpenWeatherMapResponse> GetByCityAsync(string city, string countryCode = null)
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                throw new ArgumentNullException(nameof(city));
            }

            var url = ApplyOptionsToQuery(GetWeatherByCityUrl(city, countryCode));
            return await GetWeatherAsync(url);
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
            return await GetWeatherAsync(url);
        }

        public async Task<string> GetByCityIdAsHtmlAsync(int cityId)
        {
            var url = ApplyOptionsToQuery(GetWeatherByCityIdUrl(cityId));
            return await GetWeatherAsHtmlAsync(url);
        }

        public async Task<OpenWeatherMapResponse> GetByCityCoordinatesAsync(float latitude, float longitude)
        {
            var url = ApplyOptionsToQuery(GetWeatherByCityCoordinatesUrl(latitude, longitude));
            return await GetWeatherAsync(url);
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
            return await GetWeatherAsync(url);
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

        private static async Task<OpenWeatherMapResponse> GetWeatherAsync(string url)
        {

            var message = await new HttpClient().GetAsync(url).ConfigureAwait(false);
            message.EnsureSuccessStatusCode();

            return JsonConvert.DeserializeObject<OpenWeatherMapResponse>(await message.Content.ReadAsStringAsync().ConfigureAwait(false));
        }

        private static async Task<string> GetWeatherAsHtmlAsync(string url)
        {
            var message = await new HttpClient().GetAsync(url).ConfigureAwait(false);
            message.EnsureSuccessStatusCode();

            return await message.Content.ReadAsStringAsync().ConfigureAwait(false);
        }

        private string GetWeatherByCityUrl(string city, string countryCode)
        {
            var url = $"{_options.BaseUrl}?q={city}";

            if (!string.IsNullOrWhiteSpace(countryCode))
            {
                url = $"{url},{countryCode}";
            }

            return url;
        }

        private string GetWeatherByCityIdUrl(int cityId)
        {
            var url = $"{_options.BaseUrl}?id={cityId}";
            return url;
        }

        private string GetWeatherByCityCoordinatesUrl(float latitude, float longitude)
        {
            var url = $"{_options.BaseUrl}?lat={latitude.Invariant()}&lon={longitude.Invariant()}";
            return url;
        }

        private string GetWeatherByZipCodeUrl(int zipCode, string countryCode)
        {
            var url = $"{_options.BaseUrl}?zip={zipCode},{countryCode}";
            return url;
        }

        private string ApplyOptionsToQuery(string url)
        {
            var temp = $"{url}&appid={_options.ApiKey}";

            temp = ApplySearchAccuracyOptions(temp);

            temp = ApplyUnitsOptions(temp);

            temp = ApplyLanguageOptions(temp);

            return temp;
        }

        private string ApplySearchAccuracyOptions(string url)
        {
            var temp = url;

            if (_options.SearchAccuracy == SearchAccuracy.Accurate)
            {
                temp = $"{temp}&type=accurate";
            }

            return temp;
        }

        private string ApplyUnitsOptions(string url)
        {
            var temp = url;

            switch (_options.UnitsFormat)
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

            switch (_options.Language)
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