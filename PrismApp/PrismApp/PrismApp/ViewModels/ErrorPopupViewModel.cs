using System;
using Prism.Mvvm;
using Xamarin.Forms;

namespace PrismApp.ViewModels
{
    public class ErrorPopupViewModel : BindableBase
    {
        private string _message;

        public ErrorPopupViewModel()
        {
            Message = "Something Went wrong, Please try again later";
            
            MessagingCenter.Unsubscribe<string>(this, "ErrorMessage");
            MessagingCenter.Subscribe<string>(this, "ErrorMessage", 
                (message) => UpdateErrorMessage(message));
        }

        private void UpdateErrorMessage(string message)
        {
            Message = message;
        }
        public string Message
        {
            get
            {
                Console.WriteLine("HAHAHAHA");
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