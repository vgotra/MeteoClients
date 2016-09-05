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
            var client = new OpenWeatherMapApiClient(apiKey);

            var data = client.GetByCityAsync("London").ConfigureAwait(false).GetAwaiter().GetResult();
            Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));

            var cityAsHtml = client.GetByCityAsHtmlAsync("London").ConfigureAwait(false).GetAwaiter().GetResult();
            Console.WriteLine(cityAsHtml);

            var cityCoordinates = client.GetByCityCoordinatesAsync(51.507351f, -0.127758f).ConfigureAwait(false).GetAwaiter().GetResult();
            Console.WriteLine(JsonConvert.SerializeObject(cityCoordinates, Formatting.Indented));

            var cityCoordinatesAsHtml = client.GetByCityCoordinatesAsHtmlAsync(51.507351f, -0.127758f).ConfigureAwait(false).GetAwaiter().GetResult();
            Console.WriteLine(cityCoordinatesAsHtml);

            var cityId = client.GetByCityIdAsync(2643743).ConfigureAwait(false).GetAwaiter().GetResult();
            Console.WriteLine(JsonConvert.SerializeObject(cityId, Formatting.Indented));

            var cityIdAsHtml = client.GetByCityIdAsHtmlAsync(2643743).ConfigureAwait(false).GetAwaiter().GetResult();
            Console.WriteLine(cityIdAsHtml);

            var cityZip = client.GetByZipCodeAsync(79066, "US").ConfigureAwait(false).GetAwaiter().GetResult();
            Console.WriteLine(JsonConvert.SerializeObject(cityZip, Formatting.Indented));

            var cityZipAsHtml = client.GetByZipCodeAsHtmlAsync(79066, "US").ConfigureAwait(false).GetAwaiter().GetResult();
            Console.WriteLine(cityZipAsHtml);

            var cityByRectangle = client.GetByRectangleAsync(-179, -89, 179, 89, true, 5).ConfigureAwait(false).GetAwaiter().GetResult();
            Console.WriteLine(JsonConvert.SerializeObject(cityByRectangle, Formatting.Indented));

            var cityByCircle = client.GetByCircleAsync(55.5f, 37.5f, true, 5).ConfigureAwait(false).GetAwaiter().GetResult();
            Console.WriteLine(JsonConvert.SerializeObject(cityByCircle, Formatting.Indented));

            var citiesByIds = client.GetByCitiesIdsAsync(524901, 703448, 2643743).ConfigureAwait(false).GetAwaiter().GetResult();
            Console.WriteLine(JsonConvert.SerializeObject(citiesByIds, Formatting.Indented));
        }
    }
}
