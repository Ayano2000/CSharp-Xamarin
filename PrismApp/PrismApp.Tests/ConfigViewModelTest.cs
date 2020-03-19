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
                const string CAPE_TOWN = "Cape Town";
                const string STELLENBOSCH = "Stellenbosch";
                const string JOHANNESBURG = "Johannesburg";
                
                Configuration.CityNames = new List<string>();
                var navigationService = Substitute.For<INavigationService>();
            
                var viewModel = new ConfigViewModel(navigationService);
                
                viewModel.City = CAPE_TOWN;
                viewModel.AddCityButtonClicked.Execute(CAPE_TOWN);
                viewModel.City = STELLENBOSCH;
                viewModel.AddCityButtonClicked.Execute(STELLENBOSCH);
                viewModel.City = JOHANNESBURG;
                viewModel.AddCityButtonClicked.Execute(JOHANNESBURG);

                Assert.That(Configuration.CityNames.Count() == 3);
                Assert.That(Configuration.CityNames.Contains("Cape Town"));
                Assert.That(Configuration.CityNames.Contains("Stellenbosch"));
                Assert.That(Configuration.CityNames.Contains("Johannesburg"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}