using System.Collections.Generic;

namespace MeteoClients.OpenWeatherMap.Contracts
{
    public class CitiesInRectangleResponse
    {
        public int Cod { get; set; }
        public float? CalcTime { get; set; }
        public string Message { get; set; }
        public int Cnt { get; set; }
        public List<OpenWeatherMapResponse> @List { get; set; }
    }
}