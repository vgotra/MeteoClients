using System.Collections.Generic;

namespace MeteoClients.OpenWeatherMap.Contracts
{
    public class CitiesInCircleResponse
    {
        public int Cod { get; set; }
        public float? CalcTime { get; set; }
        public string Message { get; set; }
        public int Count { get; set; }
        public List<OpenWeatherMapResponse> @List { get; set; }
    }
}