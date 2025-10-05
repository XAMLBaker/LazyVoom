﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Sample2
{
    public partial class MainPageStore : ObservableObject
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
