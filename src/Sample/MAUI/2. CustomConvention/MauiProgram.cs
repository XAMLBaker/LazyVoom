using LazyVoom.Core;
using Microsoft.Extensions.Logging;

namespace Sample2
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder ();
            builder
                .UseMauiApp<App> ()
                .ConfigureFonts (fonts =>
                {
                    fonts.AddFont ("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont ("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            Voom.Instance
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
            return builder.Build ();
        }
    }
}
