using MapWeatherXamarin.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace MapWeatherXamarin.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            var vm = new MainViewModel();
            BindingContext = vm;
            vm.AddPinToMap += AddPinToMap;
        }

        private void AddPinToMap(object sender, PinEventArgs e)
        {
            var pin = new Pin
            {
                Type = PinType.Place,
                Position = e.Position,
                Label = e.Label,
                Address = e.Address
            };
            MyMap.Pins.Add(pin);
        }
    }
}
