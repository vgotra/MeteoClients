using Newtonsoft.Json;

namespace MeteoClients.OpenWeatherMap.Contracts
{
    public class Main
    {
        public float Temp { get; set; }
        public int Pressure { get; set; }
        public int Humidity { get; set; }
        [JsonProperty(PropertyName = "temp_min")]
        public float TempMin { get; set; }
        [JsonProperty(PropertyName = "temp_max")]
        public float TempMax { get; set; }
    }
}