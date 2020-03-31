using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using Prism.Navigation;
using PrismApp.DTO;
using PrismApp.Services;
using PrismApp.ViewModels;

namespace PrismApp.Tests
{
    [TestFixture]
    class WeatherInfoViewModelTest
    {
        public WeatherModel Populate(string title, int value)
        {
            return new WeatherModel
            {
                Title = title,
                Main = new MainModel
                {
                    Temperature = value,
                    Humidity = value + 1
                },
                Sys = new SysModel
                {
                    Sunrise = value + 2,
                    Sunset = value + 3
                },
                Visibility = value + 4,
                Wind = new WindModel
                {
                    Speed = value + 5,
                }
            };
        }

        [Test]
        public void CityWeatherViewModelAdditionTest()
        {
            try
            {
                // Arrange
                const string CAPE_TOWN = "Cape Town";
                const string STELLENBOSCH = "Stellenbosch";
                
                var restService = Substitute.For<IRestService>();
                var queryService = Substitute.For<IQueryService>();
                var navigationService = Substitute.For<INavigationService>();
                var settingsService = Substitute.For<ISettingsService>();
                var locationService = Substitute.For<ILocationService>();


                settingsService.UserCities.Returns(new List<string>
                {
                    CAPE_TOWN, STELLENBOSCH
                });
                queryService.GenerateQuery(CAPE_TOWN).Returns(CAPE_TOWN);
                queryService.GenerateQuery(STELLENBOSCH).Returns(STELLENBOSCH);
                restService.GetWeatherData(CAPE_TOWN).Returns(result => Task.FromResult(
                    Populate(CAPE_TOWN, 20)));

                restService.GetWeatherData(STELLENBOSCH).Returns(result => Task.FromResult(
                    Populate(STELLENBOSCH, 30)));

                var viewModel = new WeatherInfoViewModel(navigationService, restService, queryService, settingsService, locationService);

                // Assert
                Assert.That(viewModel.CityWeatherViewModels.Count() == 2);
                
                var capeTownModel = viewModel.CityWeatherViewModels.FirstOrDefault(vm => vm.Location == CAPE_TOWN);
                var stellenboschModel =
                    viewModel.CityWeatherViewModels.FirstOrDefault(vm => vm.Location == STELLENBOSCH);

                if (capeTownModel != null)
                {
                    Assert.That(capeTownModel != null);
                    Assert.That(capeTownModel.Location == CAPE_TOWN);
                    Assert.That(capeTownModel.Temperature == 20);
                    Assert.That(capeTownModel.Humidity == 21);
                    Assert.That(capeTownModel.Sunrise == 22);
                    Assert.That(capeTownModel.Sunset == 23);
                    Assert.That(capeTownModel.Visibility == 24);
                    Assert.That(capeTownModel.WindSpeed == 25);
                }
                else Assert.Fail();

                if (stellenboschModel != null)
                {
                    Assert.That(stellenboschModel != null);
                    Assert.That(stellenboschModel.Location == STELLENBOSCH);
                    Assert.That(stellenboschModel.Temperature == 30);
                    Assert.That(stellenboschModel.Humidity == 31);
                    Assert.That(stellenboschModel.Sunrise == 32);
                    Assert.That(stellenboschModel.Sunset == 33);
                    Assert.That(stellenboschModel.Visibility == 34);
                    Assert.That(stellenboschModel.WindSpeed == 35);
                }
                else Assert.Fail();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}