using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using MapWeatherXamarin.Annotations;
using Xamarin.Forms;
using XLabs.Platform.Services.Geolocation;

namespace MapWeatherXamarin.ViewModel
{
    public class MainViewModel:INotifyPropertyChanged
    {
        private IGeolocator _geolocator;
        private string _latitude;
        private string _longitude;

        public MainViewModel()
        {
            GetCoordCommand = new Command(GetGeolocation);
            _geolocator = DependencyService.Get<IGeolocator>();
            Latitude = "lat";
            Longitude = "long";
        }

        public ICommand GetCoordCommand { get; private set; }

        public string Longitude
        {
            get { return _longitude; }
            set { _longitude = value; OnPropertyChanged(); }
        }


        public string Latitude
        {
            get { return _latitude; }
            set
            {
                _latitude = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        async void GetGeolocation()
        {
            var position = await _geolocator.GetPositionAsync(1000);
            Longitude = position.Longitude.ToString();
            Latitude = position.Latitude.ToString();
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
