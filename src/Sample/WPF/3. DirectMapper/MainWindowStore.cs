﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Sample3
{
    public partial class MainWindowStore : ObservableObject
    {
        [ObservableProperty] int count;

        [RelayCommand]
        private void Increment()
        {
            Count++;
        }

        [RelayCommand]
        private void Decrement()
        {
            Count--;
        }
    }
}
