using System;
using System.Threading.Tasks;
using MapWeatherXamarin.Models;
using MapWeatherXamarin.Service.Networking;
using Newtonsoft.Json;

namespace MapWeatherXamarin.Service.Weather
{
    public class WeatherService:IWeatherService
    {
        public enum Days
        {
            Today = 0,
            Tomorrow = 7,
            DayAfterTomorrow = 14,
            DayAfterAfterTomorrow = 21
        }

        private const string GetForecastUI = "http://api.openweathermap.org/data/2.5/forecast?q=";
        private const string GetForecastByGeo = "http://api.openweathermap.org/data/2.5/forecast?";
        private const string AppID = "&APPID=796fc5d2ebcbb4206a4f0ab759a0f904";
        private const string Lat = "lat=";
        private const string Lon = "lon=";
        private readonly IRestClient _restClient = new RestClient();

        public async Task<Forecast> GetForecastForToday(string city)
        {
            var forecastResponce = await ReturnResponce(city);
            return Get(forecastResponce, Days.Today);
        }

        public async Task<Forecast> GetForecastForTomorrow(string city)
        {
            var forecastResponce = await ReturnResponce(city);
            return Get(forecastResponce, Days.Tomorrow);
        }

        public async Task<Forecast> GetForecastForTheDayAfterTomorrow(string city)
        {
            var forecastResponce = await ReturnResponce(city);
            return Get(forecastResponce, Days.DayAfterTomorrow);
        }

        public async Task<Forecast> GetForecastForTheDayAfterAfterTomorrow(string city)
        {
            var forecastResponce = await ReturnResponce(city);
            return Get(forecastResponce, Days.DayAfterAfterTomorrow);
        }

        public async Task<Forecast> GetForecastForTodayByGeo(double latitude, double longitude)
        {
            var forecastResponce = await ReturnResponceByGeo(latitude, longitude);
            return Get(forecastResponce, Days.Today);
        }


        private async Task<ForecastResponce> ReturnResponce(string city)
        {
            var uri = GetForecastUI + city;
            var responce = await _restClient.GetAsync(uri);
            return JsonConvert.DeserializeObject<ForecastResponce>(responce);
        }

        private async Task<ForecastResponce> ReturnResponceByGeo(double latitude, double longitude)
        {
            var latitudeRounded = Math.Round(latitude).ToString();
            var longitudeRounded = Math.Round(longitude).ToString();
            var uri = GetForecastByGeo + Lat + latitudeRounded + "&" + Lon + longitudeRounded + AppID;
            var responce = await _restClient.GetAsync(uri);
            return JsonConvert.DeserializeObject<ForecastResponce>(responce);
        }

        private static Forecast Get(ForecastResponce fr, Days day)
        {

            return new Forecast
            {
                Humidity = fr.list[(int)(day)].Main.humidity,
                Temperature = Math.Round(fr.list[(int)(day)].Main.temp - 273)
            };
        }
    }
}
