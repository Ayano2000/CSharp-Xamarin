﻿using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WeatherTracker.ViewModels
{
    public class ViewModelBase: INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}