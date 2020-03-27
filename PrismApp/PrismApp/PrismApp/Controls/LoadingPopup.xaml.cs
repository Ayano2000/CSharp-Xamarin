using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PrismApp.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoadingPopup : PopupPage
    {
        private BindableProperty ProcessDescriptionProperty = BindableProperty.Create(
            propertyName: nameof(ProcessDescription),
            returnType: typeof(string),
            declaringType: typeof(LoadingPopup),
            defaultValue: string.Empty
        );

        public LoadingPopup()
        {
            InitializeComponent();
        }

        public string ProcessDescription
        {
            get => GetValue(ProcessDescriptionProperty) as string;
            set => SetValue(ProcessDescriptionProperty, value);
        }
    }
}