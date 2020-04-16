using System;
using Prism.Mvvm;
using Xamarin.Forms;

namespace PrismApp.ViewModels
{
    public class ErrorPopupViewModel : BindableBase
    {
        private string _message;
        public Command ErrorButtonClickedCommand { get; }

        public ErrorPopupViewModel()
        {
            Message = "Something Went wrong, Please try again later";
            
            ErrorButtonClickedCommand = new Command(execute: ErrorButtonClicked);
            
            MessagingCenter.Unsubscribe<string>("Error", "ErrorMessage");
            MessagingCenter.Subscribe<string>("Error", "ErrorMessage", 
                (message) => UpdateErrorMessage(message));
        }

        private void ErrorButtonClicked()
        {
            Console.WriteLine("HERE");
            MessagingCenter.Send(this, "ErrorButtonClicked");
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