using LazyVoom.Core;
using System.Windows;

namespace Sample2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup (e);

            MvvmBindingEngine.Instance
               .WithConvention ((viewType) =>
               {
                   var viewFullName = viewType.FullName;
                   if (string.IsNullOrEmpty (viewFullName))
                       return null;

                   // Views → ViewModels 네임스페이스 변환
                   var viewModelNamespace = viewFullName.Replace (".Views.", ".Stores.");
                   var viewModelTypeName = $"{viewModelNamespace}{"Store"}, {viewType.Assembly.FullName}";
                   return Type.GetType (viewModelTypeName);
               });
        }
    }
}
