﻿using Prism;
using Prism.Ioc;
using PrismApp.Services;
using PrismApp.ViewModels;
using PrismApp.Views;
using Rg.Plugins.Popup.Contracts;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

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
            
            await NavigationService.NavigateAsync("WeatherInfoView");
        }

        private void RegisterServices(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IRestService, RestService>();
            containerRegistry.Register<ILocationService, LocationService>();
            containerRegistry.Register<IQueryService, QueryService>();
            containerRegistry.Register<ISettingsService, SettingsService>();
            containerRegistry.RegisterInstance(typeof(IPopupNavigation), PopupNavigation.Instance);
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            RegisterServices(containerRegistry);

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<WeatherInfoView, WeatherInfoViewModel>();
        }
    }
}
