using System;
using System.Threading.Tasks;

namespace MeteoClients.ForecastIo
{
    public interface IForecastIoApiClient
    {
        Task<ForecastIoResponse> GetByCoordinatesAsync(float latitude, float longitude, DateTime? time = null);
    }
}