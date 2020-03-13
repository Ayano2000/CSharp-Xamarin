using WeatherTracker.ViewModels;
using WeatherTracker.Views;
using Xamarin.Forms;

namespace WeatherTracker
{
    public partial class MainPage : ContentPage
    {
        public MainPageViewModel WeatherViewModel;
        public MainPage()
        {
            InitializeComponent();
            WeatherViewModel = new MainPageViewModel();
        }

        private void ConfigButton_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new ConfigView());
        }
    }
}