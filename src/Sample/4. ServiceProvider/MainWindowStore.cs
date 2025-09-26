using CommunityToolkit.Mvvm.ComponentModel;

namespace Sample4
{
    public class MainWindowStore : ObservableObject
    {
        public string Title { get; } = "Hello";
    }
}
