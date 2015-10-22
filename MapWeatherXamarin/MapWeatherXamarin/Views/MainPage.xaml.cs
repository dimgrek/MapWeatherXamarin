using MapWeatherXamarin.ViewModel;
using Xamarin.Forms.Maps;

namespace MapWeatherXamarin.Views
{
    public partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
            var vm = new MainViewModel();
            BindingContext = vm;
            vm.CoordinatesAndTemperatureAreAvaliable += CoordinatesAndTemperatureAreAvaliable;
        }

        private void CoordinatesAndTemperatureAreAvaliable(object sender, PinEventArgs e)
        {
            var pin = new Pin
            {
                Type = e.Type,
                Position = e.Position,
                Label = e.Label,
                Address = e.Address
            };
            MyMap.Pins.Add(pin);
            MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(
                new Position(e.Position.Latitude, e.Position.Longitude),
                Distance.FromKilometers(3)));
        }
    }
}