using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using NSubstitute;
using NSubstitute.Core;
using NUnit.Framework;
using Prism.Navigation;
using PrismApp.DTO;
using PrismApp.Services;
using PrismApp.ViewModels;
using PrismApp.Views;
using Xamarin.Essentials;

namespace PrismApp.Tests
{
    [TestFixture]
    class WeatherInfoViewModelTest
    {
        [Test]
        public void CityWeatherViewModelAdditionTest()
        {
            try
            {
                const string CAPE_TOWN = "Cape Town";
                const string STELLENBOSCH = "Stellenbosch";
                const string JOHANNESBURG = "Johannesburg";

                //Arrange
                //Set up substitutes, etc.
                Configuration.CityNames = new List<string>();
                
                var restService = Substitute.For<IRestService>();
                var queryService = Substitute.For<IQueryService>();
                var navigationService = Substitute.For<INavigationService>();

                queryService.GenerateQuery(CAPE_TOWN).Returns(CAPE_TOWN);
                queryService.GenerateQuery(STELLENBOSCH).Returns(STELLENBOSCH);
                queryService.GenerateQuery(JOHANNESBURG).Returns(JOHANNESBURG);
                restService.GetWeatherData(CAPE_TOWN).Returns(result => Task.FromResult(
                    new WeatherModel
                    {
                        Title = CAPE_TOWN,
                        Main = new MainModel{Temperature = 20}
                        //todo - populate the rest of the model
                    }));
                // Location = weather.Title;
                // Temperature = weather.Main.Temperature;
                // WindSpeed = weather.Wind.Speed;
                // Humidity = weather.Main.Humidity;
                // Visibility = weather.Visibility;
                // Sunrise = weather.Sys.Sunrise;
                // Sunset = weather.Sys.Sunset;
                
                restService.GetWeatherData(STELLENBOSCH).Returns(result => Task.FromResult(
                    new WeatherModel
                    {
                        Title = STELLENBOSCH
                        //todo - same here
                    }));
                restService.GetWeatherData(JOHANNESBURG).Returns(result => Task.FromResult(
                    new WeatherModel
                    {
                        Title = JOHANNESBURG
                        //todo - same here
                    }));

                Configuration.CityNames.Add(CAPE_TOWN);
                Configuration.CityNames.Add(STELLENBOSCH);
                Configuration.CityNames.Add(JOHANNESBURG);

                var viewModel = new WeatherInfoViewModel(navigationService, restService, queryService);

                //Act
                //Call functions
                viewModel.GetWeatherCommand.Execute(null);
                
                //Assert
                Assert.That(viewModel.CityWeatherViewModels.Count() == 3);
                
                //CAPE TOWN
                var capeTownModel = viewModel.CityWeatherViewModels.FirstOrDefault(vm => vm.Location == CAPE_TOWN);
                var stellenboschModel = viewModel.CityWeatherViewModels.FirstOrDefault(vm => vm.Location == STELLENBOSCH);
                var johannesburgModel = viewModel.CityWeatherViewModels.FirstOrDefault(vm => vm.Location == JOHANNESBURG);
                //now you've got the viewmodel
                //assert that the other properties have been set correctly
                Assert.That(capeTownModel != null); //this model was not generated correctly, or does not exist
                Assert.That(capeTownModel.Temperature == 20); //this might be null thus breaking the test

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}