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
                //Arrange
                //Set up substitutes, etc.
                ObservableCollection<CityWeatherViewModel> _cityWeatherModels = new ObservableCollection<CityWeatherViewModel>();
                List<string> cities = new List<string>();
                cities.Add("Cape Town");
                cities.Add("Stellenbosch");
                cities.Add("Johannesburg");

                var restService = Substitute.For<IRestService>();
                var queryService = Substitute.For<IQueryService>();
                var navigationService = Substitute.For<INavigationService>();

                foreach(string city in cities)
                {
                    queryService.GenerateQuery(cities[0]).Returns("City Name");
                    restService.GetWeatherData("City Name").Returns(result => Task.FromResult(
                        new WeatherModel
                        {
                            Title = city
                        }));

                }

                var viewModel = new WeatherInfoViewModel(navigationService, restService, queryService);

                //Act
                //Call functions

                //Assert
                //Assert the results from functions are as expected
                //MyCollection.Any(p => p.name == "bob" && p.Checked
                Assert.That(_cityWeatherModels.Contains(CityWeatherViewModel.Location.Equals = )
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
