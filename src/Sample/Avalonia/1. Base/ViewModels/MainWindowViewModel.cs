using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Sample1.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
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
