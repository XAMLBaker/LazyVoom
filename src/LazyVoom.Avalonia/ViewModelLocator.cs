using Avalonia;
using Avalonia.Controls;
using LazyVoom.Core;
using System.Diagnostics;

namespace LazyVoom.Avalonia;

public static class ViewModelLocator
{
    public static readonly AttachedProperty<bool> AutoWireViewModelProperty =
    AvaloniaProperty.RegisterAttached<Control, bool> (
        "AutoWireViewModel",
        typeof (ViewModelLocator),
        defaultValue: false);

    static ViewModelLocator()
    {
        AutoWireViewModelProperty.Changed.AddClassHandler<Control> (OnAutoWireViewModelChanged);
    }

    public static bool GetAutoWireViewModel(Control control)
    {
        return control.GetValue (AutoWireViewModelProperty);
    }

    public static void SetAutoWireViewModel(Control control, bool value)
    {
        control.SetValue (AutoWireViewModelProperty, value);
    }

    private static void OnAutoWireViewModelChanged(Control control, AvaloniaPropertyChangedEventArgs e)
    {
        var value = e.NewValue as bool?;
        if (value.HasValue && value.Value)
        {
            Debug.WriteLine ($"[{DateTime.Now}] [Locator] {control.GetType ().Name} ViewModelLocator에 의해 자동 연결처리 합니다.");
            control.AutoBindViewModel (Bind);
        }
    }
    static void Bind(object view, object viewModel)
    {
        if (view is Control control)
        {
            Debug.WriteLine ($"[{DateTime.Now}] [Locator] {view.GetType ().Name}과 {viewModel.GetType ().Name}이 연결되었습니다.");
            control.DataContext = viewModel;
        }
    }
}

