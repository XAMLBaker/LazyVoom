using LazyVoom.Core;
using System.Diagnostics;
using System.Windows;

namespace LazyVoom.WPF;

public static class ViewModelLocator
{
    public static readonly DependencyProperty AutoWireViewModelProperty =
        DependencyProperty.RegisterAttached ("AutoWireViewModel", typeof (bool), typeof (ViewModelLocator),
            new PropertyMetadata (false, OnAutoWireViewModelChanged));

    public static bool GetAutoWireViewModel(DependencyObject obj)
    {
        return (bool)obj.GetValue (AutoWireViewModelProperty);
    }

    public static void SetAutoWireViewModel(DependencyObject obj, bool value)
    {
        obj.SetValue (AutoWireViewModelProperty, value);
    }

    private static void OnAutoWireViewModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var value = (bool?)e.NewValue;
        if (value.HasValue && value.Value)
        {
            Debug.WriteLine ($"[{0}] [Locator] {d.GetType().Name} ViewModelLocator�� ���� �ڵ� ����ó�� �մϴ�.", DateTime.Now.ToString());
            d.AutoBindViewModel (Bind);
        }
    }

    static void Bind(object view, object viewModel)
    {
        if (view is FrameworkElement element)
        {
            Debug.WriteLine ($"[{0}] [Locator] {view.GetType ().Name}�� {viewModel.GetType ().Name}�� ����Ǿ����ϴ�.", DateTime.Now.ToString ());
            element.DataContext = viewModel;
        }
    }
}
