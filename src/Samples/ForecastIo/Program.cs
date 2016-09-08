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
using MeteoClients.ForecastIo;
using MeteoClients.ForecastIo.Settings;
using Newtonsoft.Json;

namespace ForecastIo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // get your api key at https://developer.forecast.io/ page
            var apiKey = "f08971947f0cc51a005bcc250fd47604";
            var client = new ForecastIoApiClient(apiKey);

            // default language
            var data = client.GetByCoordinatesAsync(51.507351f, -0.127758f).ConfigureAwait(false).GetAwaiter().GetResult();
            Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));

            // german language
            client.Language = SupportedLanguage.German;
            data = client.GetByCoordinatesAsync(51.507351f, -0.127758f).ConfigureAwait(false).GetAwaiter().GetResult();
            Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));

            // unit 
            client.Unit = Unit.Ca;
            data = client.GetByCoordinatesAsync(51.507351f, -0.127758f).ConfigureAwait(false).GetAwaiter().GetResult();
            Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));

            // exclude blocks
            client.ExcludeBlocks = new List<Block>() { Block.Currently, Block.Alerts };
            data = client.GetByCoordinatesAsync(51.507351f, -0.127758f).ConfigureAwait(false).GetAwaiter().GetResult();
            Console.WriteLine(JsonConvert.SerializeObject(data, Formatting.Indented));
        }
    }
}
