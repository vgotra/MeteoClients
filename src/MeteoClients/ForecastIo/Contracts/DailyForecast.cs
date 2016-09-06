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
namespace MeteoClients.ForecastIo.Contracts
{
    public class DailyForecast
    {
        public long Time { get; set; }
        public string Summary { get; set; }
        public string Icon { get; set; }
        public long SunriseTime { get; set; }
        public long SunsetTime { get; set; }
        public float MoonPhase { get; set; }
        public float PrecipAccumulation { get; set; }
        public float PrecipIntensity { get; set; }
        public float PrecipIntensityMax { get; set; }
        public long PrecipIntensityMaxTime { get; set; }
        public float PrecipProbability { get; set; }
        public string PrecipType { get; set; }
        public float TemperatureMin { get; set; }
        public long TemperatureMinTime { get; set; }
        public float TemperatureMax { get; set; }
        public long TemperatureMaxTime { get; set; }
        public float ApparentTemperatureMin { get; set; }
        public long ApparentTemperatureMinTime { get; set; }
        public float ApparentTemperatureMax { get; set; }
        public long ApparentTemperatureMaxTime { get; set; }
        public float DewPoint { get; set; }
        public float WindSpeed { get; set; }
        public float WindBearing { get; set; }
        public float CloudCover { get; set; }
        public float Humidity { get; set; }
        public float Pressure { get; set; }
        public float Visibility { get; set; }
        public float Ozone { get; set; }
    }
}