using CommunityToolkit.Mvvm.ComponentModel;

namespace Sample3
{
    public class MainWindowStore : ObservableObject
    {
        public string Title { get; } = "Hello";
    }
}
