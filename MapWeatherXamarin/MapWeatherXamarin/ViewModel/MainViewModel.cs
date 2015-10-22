using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using MapWeatherXamarin.Annotations;
using MapWeatherXamarin.Service.Weather;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using XLabs.Platform.Services.Geolocation;

namespace MapWeatherXamarin.ViewModel
{
    public class MainViewModel:INotifyPropertyChanged
    {
        private IGeolocator _geolocator;
        private double _latitude;
        private double _longitude;
        private string _temperature;
        private WeatherService _ws;

        public MainViewModel()
        {
            GetCoordCommand = new Command(GetGeolocation);
            _geolocator = DependencyService.Get<IGeolocator>();
            _ws = new WeatherService();
        }

        public ICommand GetCoordCommand { get; private set; }

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

        async void GetGeolocation()
        {
            var position = await _geolocator.GetPositionAsync(1000);
            Longitude = position.Longitude;
            Latitude = position.Latitude;
            var temperature = await GetWeather();
            AddPin(temperature);
        }

        async Task<string> GetWeather()
        {
            var weather = await _ws.GetForecastForTodayByGeo(Latitude, Longitude);
            return weather.Temperature.ToString();
        }

        private void AddPin(string temperature)
        {
            var position = new Xamarin.Forms.Maps.Position(Latitude, Longitude);
            CoordinatesAndTemperatureAreAvaliable?.Invoke(this, new PinEventArgs
            {
                Type = PinType.Place,
                Position = position,
                Label = temperature,
                Address = string.Empty
            });
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
