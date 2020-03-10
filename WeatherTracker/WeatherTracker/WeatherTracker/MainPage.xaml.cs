using System;
using Xamarin.Forms;

namespace WeatherTracker
{
    public partial class MainPage : ContentPage
    {
        WeatherViewModel _WeatherData;
        RestService _restService;

        public MainPage()
        {
            InitializeComponent();
            //this.InitializeComponent();
            //this.BindingContext = this;
            //this.IsBusy = false;
            //this.LoadButton.Clicked += LoadButton_clicked;
            _restService = new RestService();
        }

        //private void LoadButton_clicked(object sender, EventArgs e)
        //{
        //    Console.WriteLine("EHRE");
        //    this.IsBusy = true;
        //}

        async void OnGetWeatherButtonClicked(object sender, EventArgs e)
        {
            // this.IsBusy = true;
            // System.Threading.Thread.Sleep(5000);
            if (!string.IsNullOrWhiteSpace(_cityEntry.Text))
            {
                this._WeatherData = await _restService.GetWeatherData(GenerateRequestUri(Constants.Endpoint));
                // WeatherViewModel weatherData = await _restService.GetWeatherData(GenerateRequestUri(Constants.Endpoint));
                // this.IsBusy = false;
                BindingContext = _WeatherData;
            }
        }

        string GenerateRequestUri(string endpoint)
        {
            string requestUri = endpoint;
            requestUri += $"?q={_cityEntry.Text}";
            requestUri += "&units=metric"; // or units=imperial
            requestUri += $"&APPID={Constants.APIKey}";
            return requestUri;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

        }
    }
}