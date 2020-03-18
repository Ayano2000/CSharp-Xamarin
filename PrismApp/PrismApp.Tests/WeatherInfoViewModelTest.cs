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
                        Main = new MainModel
                        {
                            Temperature = 20,
                            Humidity = 20
                        },
                        Sys = new SysModel
                        {
                            Sunrise = 20,
                            Sunset = 20
                        },
                        Visibility = 20,
                        Wind = new WindModel
                        {
                            Speed = 20,
                        }
                    }));
                
                restService.GetWeatherData(STELLENBOSCH).Returns(result => Task.FromResult(
                    new WeatherModel
                    {
                        Title = STELLENBOSCH,
                        Main = new MainModel
                        {
                            Temperature = 30,
                            Humidity = 30
                        },
                        Sys = new SysModel
                        {
                            Sunrise = 30,
                            Sunset = 30
                        },
                        Visibility = 30,
                        Wind = new WindModel
                        {
                            Speed = 30,
                        }
                    }));
                restService.GetWeatherData(JOHANNESBURG).Returns(result => Task.FromResult(
                    new WeatherModel
                    {
                        Title = JOHANNESBURG,
                        Main = new MainModel
                        {
                            Temperature = 40,
                            Humidity = 40
                        },
                        Sys = new SysModel
                        {
                            Sunrise = 40,
                            Sunset = 40
                        },
                        Visibility = 40,
                        Wind = new WindModel
                        {
                            Speed = 40,
                        }
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

                // can use an if statement to check of a certain viewmodel is null but this throws an exception as is.
                Assert.That(capeTownModel != null);
                Assert.That(capeTownModel.Location == "Cape Town");
                Assert.That(capeTownModel.Temperature == 20);
                Assert.That(capeTownModel.Humidity == 20);
                Assert.That(capeTownModel.Sunrise == 20);
                Assert.That(capeTownModel.Sunset == 20);
                Assert.That(capeTownModel.Visibility == 20);
                Assert.That(capeTownModel.WindSpeed == 20);

                Assert.That(stellenboschModel != null);
                Assert.That(stellenboschModel.Location == "Stellenbosch");
                Assert.That(stellenboschModel.Temperature == 30);
                Assert.That(stellenboschModel.Humidity == 30);
                Assert.That(stellenboschModel.Sunrise == 30);
                Assert.That(stellenboschModel.Sunset == 30);
                Assert.That(stellenboschModel.Visibility == 30);
                Assert.That(stellenboschModel.WindSpeed == 30);

                Assert.That(johannesburgModel != null);
                Assert.That(johannesburgModel.Location == "Johannesburg");
                Assert.That(johannesburgModel.Temperature == 40);
                Assert.That(johannesburgModel.Humidity == 40);
                Assert.That(johannesburgModel.Sunrise == 40);
                Assert.That(johannesburgModel.Sunset == 40);
                Assert.That(johannesburgModel.Visibility == 40);
                Assert.That(johannesburgModel.WindSpeed == 40);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}