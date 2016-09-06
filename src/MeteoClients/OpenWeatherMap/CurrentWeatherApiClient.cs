// The MIT License (MIT)
// 
// Copyright (c) 2016 Volodymyr Gotra
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
// 
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeteoClients.Extensions;
using MeteoClients.OpenWeatherMap.Contracts.Current;

namespace MeteoClients.OpenWeatherMap
{
    public class CurrentWeatherApiClient : OpenWeatherMapApiClientBase, ICurrentWeatherApiClient
    {
        public CurrentWeatherApiClient(string apiKey, string baseUrl = "http://api.openweathermap.org/data/2.5")
            :base(apiKey, baseUrl)
        {
        }

        public async Task<GenericResponse> GetByCityAsync(string city, string countryCode = null)
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                throw new ArgumentNullException(nameof(city));
            }

            var url = ApplyOptionsToQuery(GetWeatherByCityUrl(city, countryCode));
            return await GetWeatherAsync<GenericResponse>(url);
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

        public async Task<GenericResponse> GetByCityIdAsync(int cityId)
        {
            if (cityId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(cityId));
            }

            var url = ApplyOptionsToQuery(GetWeatherByCityIdUrl(cityId));
            return await GetWeatherAsync<GenericResponse>(url);
        }

        public async Task<MultipleCitiesResponse> GetByCitiesIdsAsync(params int[] cityIds)
        {
            if (cityIds == null || !cityIds.Any())
            {
                throw new ArgumentNullException(nameof(cityIds));
            }

            if (!cityIds.All(x => x > 0))
            {
                throw new ArgumentOutOfRangeException(nameof(cityIds));
            }

            var url = ApplyOptionsToQuery(GetWeatherByCitiesIdsUrl(cityIds));
            return await GetWeatherAsync<MultipleCitiesResponse>(url, Encoding.UTF8);
        }

        public async Task<string> GetByCityIdAsHtmlAsync(int cityId)
        {
            var url = ApplyOptionsToQuery(GetWeatherByCityIdUrl(cityId));
            return await GetWeatherAsHtmlAsync(url);
        }

        public async Task<GenericResponse> GetByCityCoordinatesAsync(float latitude, float longitude)
        {
            if (90 < latitude || latitude < -90)
            {
                throw new ArgumentOutOfRangeException(nameof(latitude));
            }

            if (180 < longitude || longitude < -180)
            {
                throw new ArgumentOutOfRangeException(nameof(longitude));
            }

            var url = ApplyOptionsToQuery(GetWeatherByCityCoordinatesUrl(latitude, longitude));
            return await GetWeatherAsync<GenericResponse>(url);
        }

        public async Task<string> GetByCityCoordinatesAsHtmlAsync(float latitude, float longitude)
        {
            if (90 < latitude || latitude < -90)
            {
                throw new ArgumentOutOfRangeException(nameof(latitude));
            }

            if (180 < longitude || longitude < -180)
            {
                throw new ArgumentOutOfRangeException(nameof(longitude));
            }

            var url = ApplyOptionsToQuery(GetWeatherByCityCoordinatesUrl(latitude, longitude));
            return await GetWeatherAsHtmlAsync(url);
        }

        public async Task<GenericResponse> GetByZipCodeAsync(int zipCode, string countryCode)
        {
            if (string.IsNullOrWhiteSpace(countryCode))
            {
                throw new ArgumentNullException(nameof(countryCode));
            }

            if (zipCode <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(zipCode));
            }

            var url = ApplyOptionsToQuery(GetWeatherByZipCodeUrl(zipCode, countryCode));
            return await GetWeatherAsync<GenericResponse>(url);
        }

        public async Task<string> GetByZipCodeAsHtmlAsync(int zipCode, string countryCode)
        {
            if (string.IsNullOrWhiteSpace(countryCode))
            {
                throw new ArgumentNullException(nameof(countryCode));
            }

            if (zipCode <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(zipCode));
            }

            var url = ApplyOptionsToQuery(GetWeatherByZipCodeUrl(zipCode, countryCode));
            return await GetWeatherAsHtmlAsync(url);
        }

        public async Task<CitiesInRectangleResponse> GetByRectangleAsync(float longitudeTopLeft, float latitudeTopLeft, float longitudeBottomRight, float latitudeBottomRight, float zoom, 
            bool useServerClustering)
        {
            if (90 < latitudeBottomRight || latitudeBottomRight < -90)
            {
                throw new ArgumentOutOfRangeException(nameof(latitudeBottomRight));
            }

            if (180 < longitudeTopLeft || longitudeTopLeft < -180)
            {
                throw new ArgumentOutOfRangeException(nameof(longitudeTopLeft));
            }

            if (90 < latitudeBottomRight || latitudeBottomRight < -90)
            {
                throw new ArgumentOutOfRangeException(nameof(latitudeBottomRight));
            }

            if (180 < longitudeBottomRight || longitudeBottomRight < -180)
            {
                throw new ArgumentOutOfRangeException(nameof(longitudeBottomRight));
            }

            var url = ApplyOptionsToQuery(GetWeatherGetByRectangleZoneUrl(longitudeTopLeft, latitudeBottomRight, longitudeBottomRight, latitudeTopLeft, zoom, useServerClustering));
            return await GetWeatherAsync<CitiesInRectangleResponse>(url, Encoding.UTF8);
        }

        public async Task<CitiesInCircleResponse> GetByCircleAsync(float centerLongitude, float centerLatitude, bool useServerClustering)
        {
            if (90 < centerLatitude || centerLatitude < -90)
            {
                throw new ArgumentOutOfRangeException(nameof(centerLatitude));
            }

            if (180 < centerLongitude || centerLongitude < -180)
            {
                throw new ArgumentOutOfRangeException(nameof(centerLongitude));
            }

            var url = ApplyOptionsToQuery(GetWeatherGetByCircleUrl(centerLongitude, centerLatitude, useServerClustering));
            return await GetWeatherAsync<CitiesInCircleResponse>(url, Encoding.UTF8);
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
            if (cityIds.Any())
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

        private string GetWeatherGetByRectangleZoneUrl(float lonTopLeft, float latTopLeft, float lonBottomRight, float latBottomRight, float zoom,
            bool useServerClustering)
        {
            var cluster = useServerClustering ? "yes" : "no";
            var url = $"{BaseUrl}/box/city?bbox={lonTopLeft.Invariant()},{latTopLeft.Invariant()},{lonBottomRight.Invariant()},{latBottomRight.Invariant()},{zoom.Invariant()}&cluster={cluster}";
            return url;
        }

        private string GetWeatherGetByCircleUrl(float centerLongitude, float centerLatitude, bool useServerClustering)
        {
            var cluster = useServerClustering ? "yes" : "no";
            var url = $"{BaseUrl}/find?lat={centerLongitude.Invariant()}&lon={centerLatitude.Invariant()}&cluster={cluster}";
            return url;
        }
    }
}