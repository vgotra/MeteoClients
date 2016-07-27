using System.Collections.Generic;

namespace MeteoClients.ForecastIo.Contracts
{
    public class Daily
    {
        public string Summary { get; set; }
        public string Icon { get; set; }
        public List<DailyForecast> Data { get; set; }
    }
}