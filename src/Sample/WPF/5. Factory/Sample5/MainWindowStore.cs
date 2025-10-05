using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Sample5
{
    public partial class MainWindowStore : ObservableObject
    {
        public string Title { get; }
        [ObservableProperty] int count;
        public MainWindowStore(string arg)
        {
            Title = arg;
        }

        public MainWindowStore()
        {
            
        }

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
