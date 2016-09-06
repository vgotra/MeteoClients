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
using MeteoClients.OpenWeatherMap;
using Newtonsoft.Json;

namespace OpenWeatherMap
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // get your api key at https://home.openweathermap.org/api_keys page
            var apiKey = "881c6ac8ad540793a69ff2eb9bbb3119";
            CheckCurrentWeather(apiKey);
            CheckShortTermWeather(apiKey);
        }

        private static void CheckShortTermWeather(string apiKey)
        {
            var shortTermForecastApiClient = new ShortTermForecastApiClient(apiKey);

            var data =
                shortTermForecastApiClient.GetByCityAsync("London").ConfigureAwait(false).GetAwaiter().GetResult();
            Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));

            var cityCoordinates =
                shortTermForecastApiClient.GetByCityCoordinatesAsync(51.507351f, -0.127758f)
                    .ConfigureAwait(false)
                    .GetAwaiter()
                    .GetResult();
            Console.WriteLine(JsonConvert.SerializeObject(cityCoordinates, Formatting.Indented));

            var cityId =
                shortTermForecastApiClient.GetByCityIdAsync(2643743).ConfigureAwait(false).GetAwaiter().GetResult();
            Console.WriteLine(JsonConvert.SerializeObject(cityId, Formatting.Indented));
        }

        private static void CheckCurrentWeather(string apiKey)
        {
            var currentWeatherApiClient = new CurrentWeatherApiClient(apiKey);

            var data =
                currentWeatherApiClient.GetByCityAsync("London").ConfigureAwait(false).GetAwaiter().GetResult();
            Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));

            var cityAsHtml =
                currentWeatherApiClient.GetByCityAsHtmlAsync("London")
                    .ConfigureAwait(false)
                    .GetAwaiter()
                    .GetResult();
            Console.WriteLine(cityAsHtml);

            var cityCoordinates =
                currentWeatherApiClient.GetByCityCoordinatesAsync(51.507351f, -0.127758f)
                    .ConfigureAwait(false)
                    .GetAwaiter()
                    .GetResult();
            Console.WriteLine(JsonConvert.SerializeObject(cityCoordinates, Formatting.Indented));

            var cityCoordinatesAsHtml =
                currentWeatherApiClient.GetByCityCoordinatesAsHtmlAsync(51.507351f, -0.127758f)
                    .ConfigureAwait(false)
                    .GetAwaiter()
                    .GetResult();
            Console.WriteLine(cityCoordinatesAsHtml);

            var cityId =
                currentWeatherApiClient.GetByCityIdAsync(2643743).ConfigureAwait(false).GetAwaiter().GetResult();
            Console.WriteLine(JsonConvert.SerializeObject(cityId, Formatting.Indented));

            var cityIdAsHtml =
                currentWeatherApiClient.GetByCityIdAsHtmlAsync(2643743)
                    .ConfigureAwait(false)
                    .GetAwaiter()
                    .GetResult();
            Console.WriteLine(cityIdAsHtml);

            var cityZip =
                currentWeatherApiClient.GetByZipCodeAsync(79066, "US")
                    .ConfigureAwait(false)
                    .GetAwaiter()
                    .GetResult();
            Console.WriteLine(JsonConvert.SerializeObject(cityZip, Formatting.Indented));

            var cityZipAsHtml =
                currentWeatherApiClient.GetByZipCodeAsHtmlAsync(79066, "US")
                    .ConfigureAwait(false)
                    .GetAwaiter()
                    .GetResult();
            Console.WriteLine(cityZipAsHtml);

            var cityByRectangle =
                currentWeatherApiClient.GetByRectangleAsync(-179, -89, 179, 89, 10, true)
                    .ConfigureAwait(false)
                    .GetAwaiter()
                    .GetResult();
            Console.WriteLine(JsonConvert.SerializeObject(cityByRectangle, Formatting.Indented));

            var cityByCircle =
                currentWeatherApiClient.GetByCircleAsync(55.5f, 37.5f, true)
                    .ConfigureAwait(false)
                    .GetAwaiter()
                    .GetResult();
            Console.WriteLine(JsonConvert.SerializeObject(cityByCircle, Formatting.Indented));

            var citiesByIds =
                currentWeatherApiClient.GetByCitiesIdsAsync(524901, 703448, 2643743)
                    .ConfigureAwait(false)
                    .GetAwaiter()
                    .GetResult();
            Console.WriteLine(JsonConvert.SerializeObject(citiesByIds, Formatting.Indented));
        }
    }
}
