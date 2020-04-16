using System;
using Prism.Mvvm;
using Xamarin.Forms;

namespace PrismApp.ViewModels
{
    public class ErrorPopupViewModel : BindableBase
    {
        private string _message;
        public Command ErrorButtonClickedCommand { get; }
        
        public Command RetryCommand { get; set; }

        public ErrorPopupViewModel()
        {
            ErrorButtonClickedCommand = new Command(execute: ErrorButtonClicked);
        }

        private void ErrorButtonClicked()
        {
            RetryCommand?.Execute(null);
        }

        private void UpdateErrorMessage(string message)
        {
            Message = message;
        }
        public string Message
        {
            get
            {
                return _message;
            }
            set
            {
                _message = value;
                RaisePropertyChanged(nameof(Message));
            }
        }
    }
}