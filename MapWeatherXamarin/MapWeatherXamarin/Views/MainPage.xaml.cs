using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MapWeatherXamarin.ViewModel;
using Xamarin.Forms;

namespace MapWeatherXamarin.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            var vm = new MainViewModel();
            BindingContext = vm;
        }
    }
}
