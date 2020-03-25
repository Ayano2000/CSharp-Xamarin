using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using PrismApp.Services;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace PrismApp.ViewModels
{
    public class WeatherInfoViewModel : BindableBase
    {
        private DelegateCommand _navigateCommand;
        private readonly IRestService _restService;
        private readonly INavigationService _navigationService;
        private readonly IQueryService _queryService;
        private ObservableCollection<CityWeatherViewModel> _cityWeatherModels;
        private bool _isBusy;

        public Command GetWeatherButtonClicked { get; }
        public Command GetWeatherCommand { get; }

        public DelegateCommand NavigateCommand =>
            _navigateCommand ?? (_navigateCommand = new DelegateCommand(ExecuteNavigationCommand));

        public WeatherInfoViewModel(INavigationService navigationService, IRestService restService, IQueryService queryService)
        {
            CityWeatherViewModels = new ObservableCollection<CityWeatherViewModel>();
            _restService = restService;
            _navigationService = navigationService;
            _queryService = queryService;
            GetWeatherCommand = new Command(async () => await GetWeatherInfo());
            GetWeatherCommand.Execute(null);
        }
        public ObservableCollection<CityWeatherViewModel> CityWeatherViewModels
        {
            get => this._cityWeatherModels;
            set
            {
                _cityWeatherModels = value;
                RaisePropertyChanged();
            }
        }

        public bool IsBusy
        {
            get => this._isBusy;
            set
            {
                _isBusy = value;
                RaisePropertyChanged();
            }
        }

        async void ExecuteNavigationCommand() //async if you're awaiting
        {
            await _navigationService.GoBackAsync();
        }

        private async Task GetWeatherInfo()
        {
            IsBusy = true;
            foreach (var city in Configuration.CityNames)
            {
                if (!string.IsNullOrWhiteSpace(city))
                {
                    string requestUri = _queryService.GenerateQuery(city);
                    // Await result from endpoint query
                    var weatherModel = await _restService.GetWeatherData(requestUri);
                    CityWeatherViewModels.Add(new CityWeatherViewModel(weatherModel));
                }
                IsBusy = false;
            }
        }
    }
}