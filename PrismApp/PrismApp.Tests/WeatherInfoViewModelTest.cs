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
                    Humidity = value
                },
                Sys = new SysModel
                {
                    Sunrise = value,
                    Sunset = value
                },
                Visibility = value,
                Wind = new WindModel
                {
                    Speed = value,
                }
            };
        }

        [Test]
        public void CityWeatherViewModelAdditionTest()
        {
            try
            {
                const string CAPE_TOWN = "Cape Town";
                const string STELLENBOSCH = "Stellenbosch";

                Configuration.CityNames = new List<string>();

                var restService = Substitute.For<IRestService>();
                var queryService = Substitute.For<IQueryService>();
                var navigationService = Substitute.For<INavigationService>();

                queryService.GenerateQuery(CAPE_TOWN).Returns(CAPE_TOWN);
                queryService.GenerateQuery(STELLENBOSCH).Returns(STELLENBOSCH);
                restService.GetWeatherData(CAPE_TOWN).Returns(result => Task.FromResult(
                    Populate(CAPE_TOWN, 20)));

                restService.GetWeatherData(STELLENBOSCH).Returns(result => Task.FromResult(
                    Populate(STELLENBOSCH, 30)));

                Configuration.CityNames.Add(CAPE_TOWN);
                Configuration.CityNames.Add(STELLENBOSCH);

                var viewModel = new WeatherInfoViewModel(navigationService, restService, queryService);

                viewModel.GetWeatherCommand.Execute(null);

                Assert.That(viewModel.CityWeatherViewModels.Count() == 3);

                var capeTownModel = viewModel.CityWeatherViewModels.FirstOrDefault(vm => vm.Location == CAPE_TOWN);
                var stellenboschModel =
                    viewModel.CityWeatherViewModels.FirstOrDefault(vm => vm.Location == STELLENBOSCH);

                if (capeTownModel != null)
                {
                    Assert.That(capeTownModel != null);
                    Assert.That(capeTownModel.Location == CAPE_TOWN);
                    Assert.That(capeTownModel.Temperature == 20);
                    Assert.That(capeTownModel.Humidity == 20);
                    Assert.That(capeTownModel.Sunrise == 20);
                    Assert.That(capeTownModel.Sunset == 20);
                    Assert.That(capeTownModel.Visibility == 20);
                    Assert.That(capeTownModel.WindSpeed == 20);
                }
                else Assert.Fail();

                if (stellenboschModel != null)
                {
                    Assert.That(stellenboschModel != null);
                    Assert.That(stellenboschModel.Location == STELLENBOSCH);
                    Assert.That(stellenboschModel.Temperature == 30);
                    Assert.That(stellenboschModel.Humidity == 30);
                    Assert.That(stellenboschModel.Sunrise == 30);
                    Assert.That(stellenboschModel.Sunset == 30);
                    Assert.That(stellenboschModel.Visibility == 30);
                    Assert.That(stellenboschModel.WindSpeed == 30);
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