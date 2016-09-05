using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MeteoClients.ForecastIo.Contracts;
using MeteoClients.ForecastIo.Settings;

namespace MeteoClients.ForecastIo
{
    public interface IForecastIoApiClient
    {
        string BaseUrl { get; }
        string ApiKey { get; }
        Unit Unit { get; set; }
        List<Block> ExcludeBlocks { get; set; }
        bool ExtendHourlyData { get; set; }
        SupportedLanguage Language { get; set; }
        Task<ForecastIoResponse> GetByCoordinatesAsync(float latitude, float longitude, DateTime? time = null);
    }
}