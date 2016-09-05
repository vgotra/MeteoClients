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
