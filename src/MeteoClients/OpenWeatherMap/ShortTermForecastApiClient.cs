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
using System.Threading.Tasks;
using MeteoClients.Extensions;
using MeteoClients.OpenWeatherMap.Contracts.ShortTermForecast;

namespace MeteoClients.OpenWeatherMap
{
    public class ShortTermForecastApiClient : OpenWeatherMapApiClientBase, IShortTermForecastApiClient
    {
        public ShortTermForecastApiClient(string apiKey, string baseUrl = "http://api.openweathermap.org/data/2.5") : base(apiKey, baseUrl)
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

        public async Task<GenericResponse> GetByCityIdAsync(int cityId)
        {
            if (cityId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(cityId));
            }

            var url = ApplyOptionsToQuery(GetWeatherByCityIdUrl(cityId));
            return await GetWeatherAsync<GenericResponse>(url);
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

        private string GetWeatherByCityUrl(string city, string countryCode)
        {
            var url = $"{BaseUrl}/forecast?q={city}";

            if (!string.IsNullOrWhiteSpace(countryCode))
            {
                url = $"{url},{countryCode}";
            }

            return url;
        }

        private string GetWeatherByCityIdUrl(int cityId)
        {
            var url = $"{BaseUrl}/forecast?id={cityId}";
            return url;
        }

        private string GetWeatherByCityCoordinatesUrl(float latitude, float longitude)
        {
            var url = $"{BaseUrl}/forecast?lat={latitude.Invariant()}&lon={longitude.Invariant()}";
            return url;
        }
    }
}