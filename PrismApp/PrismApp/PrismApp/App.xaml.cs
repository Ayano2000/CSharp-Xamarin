using Prism;
using Prism.Ioc;
using PrismApp.Services;
using PrismApp.ViewModels;
using PrismApp.Views;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using PrismApp.Constants;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

//[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace PrismApp
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            Configuration.CityNames = new List<string>();
            Configuration.CityNames = Settings.Settings.UserCities;
            await NavigationService.NavigateAsync("NavigationPage/MainPage");
            
        }

        private void RegisterServices(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IRestService, RestService>();
            containerRegistry.Register<ILocationService, LocationService>();
            containerRegistry.Register<IQueryService, QueryService>();
            containerRegistry.Register<ISettingsService, SettingsService>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            RegisterServices(containerRegistry);

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<Config, ConfigViewModel>();
            containerRegistry.RegisterForNavigation<WeatherInfo, WeatherInfoViewModel>();
            containerRegistry.RegisterForNavigation<Map, MapViewModel>();
        }
    }
}
