using LazyVoom.Core;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace Sample4
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup (e);
            var services = new ServiceCollection ();

            IServiceProvider provider = services.BuildServiceProvider ();

            MvvmBindingEngine.Instance
               .WithContainerResolver ((vmType) =>
               {
                   return provider.GetService (vmType) ?? Activator.CreateInstance (vmType);
               })
               .RegisterMapping<MainWindow, MainWindowStore> ();
        }
    }
}
