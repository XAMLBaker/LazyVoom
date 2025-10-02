using LazyVoom.Core;
using System.Windows;

namespace Sample5
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup (e);

            LazyVoom.Core.LazyVoom.Instance
              .WithFactory<MainWindow> (() =>
              {
                  string arg = " Factory Make!";
                  return new MainWindowStore (arg);
              });
        }
    }

}
