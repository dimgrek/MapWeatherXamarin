using System;
using System.Threading.Tasks;
using MapWeatherXamarin.Models;

namespace MapWeatherXamarin.Service.Weather
{
    public interface IWeatherService
    {
        
        Task<Forecast> GetForecastForToday(string city);
        Task<Forecast> GetForecastForTomorrow(string city);
        Task<Forecast> GetForecastForTheDayAfterTomorrow(string city);
        Task<Forecast> GetForecastForTheDayAfterAfterTomorrow(string city);
    }
}
