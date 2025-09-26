using CommunityToolkit.Mvvm.ComponentModel;

namespace Sample2
{
    public class MainWindowStore : ObservableObject
    {
        public string Title { get; } = "Hello";
    }
}
