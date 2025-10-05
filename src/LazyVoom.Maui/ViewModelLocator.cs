using LazyVoom.Core;
using System.Diagnostics;

namespace LazyVoom.Maui;
public static class ViewModelLocator
{
    public static readonly BindableProperty AutoWireViewModelProperty =
        BindableProperty.CreateAttached (
            "AutoWireViewModel",
            typeof (bool),
            typeof (ViewModelLocator),
            false,
            propertyChanged: OnAutoWireViewModelChanged
        );

    public static bool GetAutoWireViewModel(BindableObject obj)
    {
        return (bool)obj.GetValue (AutoWireViewModelProperty);
    }

    public static void SetAutoWireViewModel(BindableObject obj, bool value)
    {
        obj.SetValue (AutoWireViewModelProperty, value);
    }

    private static void OnAutoWireViewModelChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (newValue is bool value && value)
        {
            Debug.WriteLine ($"[{DateTime.Now}] [Locator] {bindable.GetType ().Name} ViewModelLocator에 의해 자동 연결처리 합니다.");

            bindable.AutoBindViewModel (Bind);
        }
    }

    static void Bind(object view, object viewModel)
    {
        if (view is BindableObject obj)
        {
            Debug.WriteLine ($"[{0}] [Locator] {view.GetType ().Name}과 {viewModel.GetType ().Name}과 연결되었습니다.", DateTime.Now.ToString ());
            obj.BindingContext = viewModel;
        }
    }
}