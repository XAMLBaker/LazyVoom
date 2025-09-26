namespace LazyVoom.Core
{
    /// <summary>
    /// MVVM ���ε��� ���� Ȯ�� �޼����
    /// </summary>
    public static class MvvmExtensions
    {
        /// <summary>
        /// View�� ViewModel�� �ڵ����� ���ε��մϴ�
        /// </summary>
        public static void AutoBindViewModel(this object view, Action<object, object> setContext)
        {
            MvvmBindingEngine.Instance.EstablishBinding (view, setContext);
        }
        /// <summary>
        /// �������� �����ϰ� �����մϴ�
        /// </summary>
        public static MvvmBindingEngine WithConvention(this MvvmBindingEngine engine, Func<Type, Type?> typeResolver)
        {
            engine.SetViewTypeToViewModelTypeResolver (typeResolver);
            return engine;
        }
        /// <summary>
        /// ViewModel ���丮�� �����ϰ� ����մϴ�
        /// </summary>
        public static MvvmBindingEngine WithFactory<TView>(this MvvmBindingEngine engine, Func<object> factory)
        {
            engine.RegisterFactory<TView> (factory);
            return engine;
        }

        /// <summary>
        /// View-ViewModel ������ �����ϰ� ����մϴ�
        /// </summary>
        public static MvvmBindingEngine WithMapping<TView, TViewModel>(this MvvmBindingEngine engine)
            where TView : class
            where TViewModel : class
        {
            engine.RegisterMapping<TView, TViewModel> ();
            return engine;
        }

        /// <summary>
        /// �����̳� �������� �����ϰ� �����մϴ�
        /// </summary>
        public static MvvmBindingEngine WithContainerResolver(this MvvmBindingEngine engine, Func<Type, object?> containerResolver)
        {
            engine.SetContainerResolver (containerResolver);
            return engine;
        }
    }
}
