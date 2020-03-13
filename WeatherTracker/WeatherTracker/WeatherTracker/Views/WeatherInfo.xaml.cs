using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherTracker.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WeatherTracker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WeatherInfo : ContentView
    {
        public WeatherInfo()
        {
            InitializeComponent();;
        }
    }
}