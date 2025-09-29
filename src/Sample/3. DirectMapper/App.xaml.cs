using LazyVoom.Core;
using System.Windows;

namespace Sample3
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup (e);

            LazyBoom.Instance
                .WithMapping<MainWindow, MainWindowStore> ();
        }
    }

}
