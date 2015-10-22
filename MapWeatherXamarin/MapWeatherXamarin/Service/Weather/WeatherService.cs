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


        public async Task<ForecastResponce> ReturnResponce(string city)
        {
            var uri = GetForecastUI + city;
            var responce = await _restClient.GetAsync(uri);
            return JsonConvert.DeserializeObject<ForecastResponce>(responce);
        }

        public static Forecast Get(ForecastResponce fr, Days day)
        {

            return new Forecast
            {
                Humidity = fr.list[(int)(day)].Main.humidity,
                Temperature = Math.Round(fr.list[(int)(day)].Main.temp - 273)
            };
        }
    }
}
