using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using MapWeatherXamarin.Annotations;
using MapWeatherXamarin.Models;
using MapWeatherXamarin.Service.Weather;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using XLabs.Platform.Services.Geolocation;

namespace MapWeatherXamarin.ViewModel
{
    public class MainViewModel:INotifyPropertyChanged
    {
        private readonly IGeolocator _geolocator;
        private double _latitude;
        private double _longitude;
        private string _temperature;
        private Forecast _weather;
        private WeatherService _ws;

        public MainViewModel()
        {
            _geolocator = DependencyService.Get<IGeolocator>();
            _ws = new WeatherService();
            GetGeolocationAndWeather();
        }

        public double Longitude
        {
            get { return _longitude; }
            set { _longitude = value; OnPropertyChanged(); }
        }

        public double Latitude
        {
            get { return _latitude; }
            set
            {
                _latitude = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<PinEventArgs> CoordinatesAndTemperatureAreAvaliable;

        async void GetGeolocationAndWeather()
        {
            var position = await _geolocator.GetPositionAsync(5000);
            Longitude = position.Longitude;
            Latitude = position.Latitude;
            AddPin(await GetWeather());
        }

        private void AddPin(Forecast weather)
        {
            var position = new Xamarin.Forms.Maps.Position(Latitude, Longitude);
            var label = BuildLabel(weather.Temperature, weather.Humidity);
            CoordinatesAndTemperatureAreAvaliable?.Invoke(this, new PinEventArgs
            {
                Type = PinType.Place,
                Position = position,
                Label = label,
                Address = string.Empty
            });
        }

        async Task<Forecast> GetWeather()
        {
            return await _ws.GetForecastForTodayByGeo(Latitude, Longitude);
        }


        private string BuildLabel(double temperature, double humidity)
        {
            return $"Temperature: {temperature}°С, Humidity: {humidity}%";
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class PinEventArgs
    {
        public Xamarin.Forms.Maps.Position Position { get; set; }
        public PinType Type{ get; set; }
        public string Label { get; set; }
        public string  Address { get; set; }
    }
}
