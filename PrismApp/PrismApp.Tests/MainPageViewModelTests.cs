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
using Xamarin.Essentials;

namespace PrismApp.Tests
{
    [TestFixture]
    public class MainPageViewModelTests
    {
        [Test]
        public void AddUserLocationTest()
        {
            try
            {
                //Arrange
                //Set up substitutes, etc.
                Configuration.CityNames = new List<string>();

                var restService = Substitute.For<IRestService>();
                var queryService = Substitute.For<IQueryService>();
                var navigationService = Substitute.For<INavigationService>();
                var locationService = Substitute.For<ILocationService>();

                //lat0, long0
                locationService.GetLocation().Returns(item => Task.FromResult(new Location(0, 0)));
                queryService.GenerateQuery(111, 222).Returns("ThisCanBeAnything");
                restService.GetWeatherData("ThisCanBeAnything").Returns(result => Task.FromResult(
                    new WeatherModel
                    {
                        Title = "Cape Town"
                    }));               
                

                var viewModel = new MainPageViewModel(navigationService, locationService, queryService, restService);

                //Act
                //Call functions
                //we are not acting here, a command is executed in the constructor

                //Assert
                //Assert the results from functions are as expected
                Assert.That(Configuration.CityNames.Contains("Cape Town"));
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
