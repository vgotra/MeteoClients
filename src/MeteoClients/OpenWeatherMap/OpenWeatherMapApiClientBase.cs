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
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MeteoClients.OpenWeatherMap.Settings;
using Newtonsoft.Json;

namespace MeteoClients.OpenWeatherMap
{
    public class OpenWeatherMapApiClientBase : IOpenWeatherMapApiClientBase
    {
        public OpenWeatherMapApiClientBase(string apiKey, string baseUrl = "http://api.openweathermap.org/data/2.5")
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
        public int CountInResponse { get; set; } = 10;

        protected static async Task<T> GetWeatherAsync<T>(string url, Encoding encoding = null) where T: class
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

        protected static async Task<string> GetWeatherAsHtmlAsync(string url, Encoding encoding = null)
        {
            var message = await new HttpClient().GetAsync($"{url}&mode=html").ConfigureAwait(false);
            message.EnsureSuccessStatusCode();

            if (encoding == null)
            {
                return await message.Content.ReadAsStringAsync().ConfigureAwait(false);
            }

            return encoding.GetString(await message.Content.ReadAsByteArrayAsync().ConfigureAwait(false));
        }

        protected string ApplySearchAccuracyOptions(string url)
        {
            var temp = url;

            if (SearchAccuracy == SearchAccuracy.Accurate)
            {
                temp = $"{temp}&type=accurate";
            }

            return temp;
        }

        protected string ApplyCountOfResultsOptions(string url)
        {
            return $"{url}&cnt={CountInResponse}";
        }

        protected string ApplyUnitsOptions(string url)
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

        protected string ApplyLanguageOptions(string url)
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

        protected string ApplyOptionsToQuery(string url)
        {
            var temp = $"{url}&appid={ApiKey}";

            temp = ApplySearchAccuracyOptions(temp);

            temp = ApplyUnitsOptions(temp);

            temp = ApplyLanguageOptions(temp);

            temp = ApplyCountOfResultsOptions(temp);

            return temp;
        }
    }
}