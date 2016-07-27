namespace MeteoClients.ForecastIo.Contracts
{
    public class Alert
    {
        public string Title { get; set; }
        public long Expires { get; set; }
        public string Uri { get; set; }
        public string Description { get; set; }
    }
}