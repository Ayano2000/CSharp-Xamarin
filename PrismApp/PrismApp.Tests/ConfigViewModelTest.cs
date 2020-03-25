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
    public class ConfigViewModelTest
    {
        [Test]
        public void CityAdditionToListTest()
        {
            try
            {
                // Arrange
                const string CAPE_TOWN = "Cape Town";
                const string STELLENBOSCH = "Stellenbosch";
                const string JOHANNESBURG = "Johannesburg";
                
                var navigationService = Substitute.For<INavigationService>();
                var settingsService = Substitute.For<ISettingsService>();
                
                settingsService.UserCities = new List<string>
                {
                    CAPE_TOWN, STELLENBOSCH, JOHANNESBURG 
                };
                
            
                var viewModel = new ConfigViewModel(navigationService, settingsService);

                // Act
                viewModel.City = CAPE_TOWN;
                viewModel.AddCityButtonClicked.Execute(CAPE_TOWN);
                viewModel.City = STELLENBOSCH;
                viewModel.AddCityButtonClicked.Execute(STELLENBOSCH);
                viewModel.City = JOHANNESBURG;
                viewModel.AddCityButtonClicked.Execute(JOHANNESBURG);

                // Assert
                Assert.That(settingsService.UserCities.Count() == 3);
                Assert.That(settingsService.UserCities.Contains("Cape Town"));
                Assert.That(settingsService.UserCities.Contains("Stellenbosch"));
                Assert.That(settingsService.UserCities.Contains("Johannesburg"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}