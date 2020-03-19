using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using NUnit.Framework;
using Prism.Navigation;
using PrismApp.DTO;
using PrismApp.Services;
using PrismApp.ViewModels;
using Xamarin.Essentials;

namespace PrismApp.Tests
{
    [TestFixture]
    public class MapViewModelTest
    {           
        [Test]
        public void MapLocationSetterTest()
        {
            try
            {
                Configuration.CityNames = new List<string> {"Stellenbosch"};

                var restService = Substitute.For<IRestService>();
                var queryService = Substitute.For<IQueryService>();
                var navigationService = Substitute.For<INavigationService>();

                queryService.GenerateQuery("Stellenbosch").Returns("SomeData");
                restService.GetWeatherData("SomeData").Returns(result => Task.FromResult(
                    new WeatherModel
                    {
                        Title = "Stellenbosch",
                        Coord = new CoordModel()
                        {
                            Lat = 111,
                            Lon = 222
                        }
                    }));
                
                var viewModel = new MapViewModel(navigationService, queryService, restService);
                
                Assert.That(viewModel.Location.Latitude == 111);
                Assert.That(viewModel.Location.Longitude == 222);
                
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }
    }
}