
using LazyVoom.Core;
using System.Windows;

namespace LazyVoom.WPF
{
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
                d.AutoBindViewModel (Bind);
            }
        }

        static void Bind(object view, object viewModel)
        {
            if (view is FrameworkElement element)
                element.DataContext = viewModel;
        }
    }
}
