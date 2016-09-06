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
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using MeteoClients.Extensions;
using MeteoClients.ForecastIo.Contracts;
using MeteoClients.ForecastIo.Settings;
using Newtonsoft.Json;

namespace MeteoClients.ForecastIo
{
    public class ForecastIoApiClient : IForecastIoApiClient, IDisposable
    {
        private const string NumberOfApiCallsHeaderKey = "X-Forecast-API-Calls";
        private readonly HttpClient _httpClient = new HttpClient();

        public ForecastIoApiClient(string apiKey, string baseUrl = "https://api.forecast.io/forecast")
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
        public Unit Unit { get; set; } = Unit.Auto;
        public List<Block> ExcludeBlocks { get; set; }
        public bool ExtendHourlyData { get; set; } = false;
        public SupportedLanguage Language { get; set; } = SupportedLanguage.English;

        public async Task<GenericResponse> GetByCoordinatesAsync(float latitude, float longitude, DateTime? time = null)
        {
            if (90 < latitude || latitude < -90)
            {
                throw new ArgumentOutOfRangeException(nameof(latitude));
            }

            if (180 < longitude || longitude < -180)
            {
                throw new ArgumentOutOfRangeException(nameof(longitude));
            }

            var url = PrepareUrl(latitude, longitude, time);

            var message = await _httpClient.GetAsync(url).ConfigureAwait(false);
            message.EnsureSuccessStatusCode();

            var response = JsonConvert.DeserializeObject<GenericResponse>(await message.Content.ReadAsStringAsync().ConfigureAwait(false));
            response.NumberOfCalls = GetNumberOfCalls(message);

            return response;
        }

        private string PrepareUrl(float latitude, float longitude, DateTime? time = null)
        {
            //Dirty hack with ? and & query parameters separators -> it's possible to use such way for forming query: http://url?& 
            var baseUrl = PrepareBaseUrl();

            var temp = $"{baseUrl}/{latitude.Invariant()},{longitude.Invariant()}?";

            if (time.HasValue)
            {
                temp = $"{temp.TrimEnd('?')},{time:o}?";
            }

            temp = ApplyOptionsToQuery(temp);

            return temp;
        }

        private string PrepareBaseUrl()
        {
            var temp = $"{BaseUrl}/{ApiKey}";
            return temp;
        }

        private string ApplyOptionsToQuery(string url)
        {
            var temp = url;

            temp = ApplyUnitsOptions(temp);

            temp = ApplyLanguageOptions(temp);

            temp = ApplyExcludeOptions(temp);

            temp = ApplyExtendOptions(temp);

            return temp;
        }

        private string ApplyUnitsOptions(string url)
        {
            var temp = url;

            switch (Unit)
            {
                case Unit.Auto:
                    break;
                case Unit.Us:
                    temp = $"{temp}&units=us";
                    break;
                case Unit.Si:
                    temp = $"{temp}&units=si";
                    break;
                case Unit.Ca:
                    temp = $"{temp}&units=ca";
                    break;
                case Unit.Uk2:
                    temp = $"{temp}&units=uk2";
                    break;
            }

            return temp;
        }

        private string ApplyExcludeOptions(string url)
        {
            var temp = url;

            if (ExcludeBlocks != null && ExcludeBlocks.Any())
            {
                var partTemp = "&exclude=";

                foreach (var excludeBlock in ExcludeBlocks)
                {
                    switch (excludeBlock)
                    {
                        case Block.Currently:
                            partTemp = $"{partTemp}currently,";
                            break;
                        case Block.Minutely:
                            partTemp = $"{partTemp}minutely,";
                            break;
                        case Block.Hourly:
                            partTemp = $"{partTemp}hourly,";
                            break;
                        case Block.Daily:
                            partTemp = $"{partTemp}daily,";
                            break;
                        case Block.Alerts:
                            partTemp = $"{partTemp}alerts,";
                            break;
                        case Block.Flags:
                            partTemp = $"{partTemp}flags,";
                            break;
                    }
                }

                temp = $"{temp}{partTemp.TrimEnd(',')}";
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
                case SupportedLanguage.Arabic:
                    temp = $"{temp}&lang=ar";
                    break;
                case SupportedLanguage.Belarusian:
                    temp = $"{temp}&lang=be";
                    break;
                case SupportedLanguage.Bosnian:
                    temp = $"{temp}&lang=bs";
                    break;
                case SupportedLanguage.Czech:
                    temp = $"{temp}&lang=cz";
                    break;
                case SupportedLanguage.German:
                    temp = $"{temp}&lang=de";
                    break;
                case SupportedLanguage.Greek:
                    temp = $"{temp}&lang=el";
                    break;
                case SupportedLanguage.Spanish:
                    temp = $"{temp}&lang=es";
                    break;
                case SupportedLanguage.French:
                    temp = $"{temp}&lang=fr";
                    break;
                case SupportedLanguage.Croatian:
                    temp = $"{temp}&lang=hr";
                    break;
                case SupportedLanguage.Hungarian:
                    temp = $"{temp}&lang=hu";
                    break;
                case SupportedLanguage.Indonesian:
                    temp = $"{temp}&lang=id";
                    break;
                case SupportedLanguage.Italian:
                    temp = $"{temp}&lang=it";
                    break;
                case SupportedLanguage.Icelandic:
                    temp = $"{temp}&lang=is";
                    break;
                case SupportedLanguage.Cornish:
                    temp = $"{temp}&lang=kw";
                    break;
                case SupportedLanguage.Norwegian:
                    temp = $"{temp}&lang=nb";
                    break;
                case SupportedLanguage.Dutch:
                    temp = $"{temp}&lang=nl";
                    break;
                case SupportedLanguage.Polish:
                    temp = $"{temp}&lang=pl";
                    break;
                case SupportedLanguage.Portuguese:
                    temp = $"{temp}&lang=pt";
                    break;
                case SupportedLanguage.Russian:
                    temp = $"{temp}&lang=ru";
                    break;
                case SupportedLanguage.Slovak:
                    temp = $"{temp}&lang=sk";
                    break;
                case SupportedLanguage.Serbian:
                    temp = $"{temp}&lang=sr";
                    break;
                case SupportedLanguage.Swedish:
                    temp = $"{temp}&lang=sw";
                    break;
                case SupportedLanguage.Tetum:
                    temp = $"{temp}&lang=tet";
                    break;
                case SupportedLanguage.Turkish:
                    temp = $"{temp}&lang=tr";
                    break;
                case SupportedLanguage.Ukrainian:
                    temp = $"{temp}&lang=uk";
                    break;
                case SupportedLanguage.IgpayAtinlay:
                    temp = $"{temp}&lang=x-pig-latin";
                    break;
                case SupportedLanguage.SimplifiedChinese:
                    temp = $"{temp}&lang=zh";
                    break;
                case SupportedLanguage.TraditionalChinese:
                    temp = $"{temp}&lang=zh-tw";
                    break;
                case SupportedLanguage.Azerbaijani:
                    temp = $"{temp}&lang=az";
                    break;
            }

            return temp;
        }

        private string ApplyExtendOptions(string url)
        {
            var temp = url;

            if (ExtendHourlyData)
            {
                temp = $"{temp}&extend=hourly";
            }

            return temp;
        }

        private static int GetNumberOfCalls(HttpResponseMessage message)
        {
            var number = -1;
            IEnumerable<string> headerValues;

            if (message.Headers.TryGetValues(NumberOfApiCallsHeaderKey, out headerValues))
            {
                int.TryParse(headerValues.FirstOrDefault(), out number);
            }

            return number;
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}
