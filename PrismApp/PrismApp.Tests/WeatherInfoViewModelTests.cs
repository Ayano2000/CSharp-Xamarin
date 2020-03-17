using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NSubstitute;
using NSubstitute.Core;
using NUnit.Framework;
using Prism.Navigation;
using PrismApp.DTO;
using PrismApp.Services;
using PrismApp.ViewModels;

namespace PrismApp.Tests
{
    [TestFixture]
    public class WeatherInfoViewModelTests
    {
        [Test]
        public void Test1()
        {
            try
            {
                //Arrange
                //Set up substitutes, etc.
                
                Configuration.CityNames = new List<string>()
                {
                    "Johannesburg",
                    "Pretoria",
                    "Cape Town"
                }; 
            
                var restService = Substitute.For<IRestService>();
                var locationService = Substitute.For<ILocationService>();
                var navigationService = Substitute.For<INavigationService>();

                locationService.GetCityQuery("Johannesburg").Returns(string.Empty);
                locationService.GetCityQuery("Pretoria").Returns(string.Empty);
                locationService.GetCityQuery("Cape Town").Returns(string.Empty);
            
                locationService.GenerateQuery().Returns(query => Task.FromResult(string.Empty));
                restService.GetWeatherData(string.Empty).Returns(info => Task.FromResult(new WeatherModel()));

                var viewModel = new WeatherInfoViewModel(navigationService, restService, locationService);
            
                //Act
                //Call functions
                viewModel.WeatherButtonClickedCommand.Execute(null);
            
                //Assert
                //Assert the results from functions are as expected
                Assert.That(viewModel.CityWeatherViewModels.Any());
                // Assert.That(true, Is.False); //fails, expects false
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
