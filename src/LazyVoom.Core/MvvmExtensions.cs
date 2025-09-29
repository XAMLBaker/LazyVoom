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
            LazyBoom.Instance.EstablishBinding (view, setContext);
        }
        /// <summary>
        /// �������� �����ϰ� �����մϴ�
        /// </summary>
        public static LazyBoom WithConvention(this LazyBoom engine, Func<Type, Type?> typeResolver)
        {
            engine.SetViewTypeToViewModelTypeResolver (typeResolver);
            return engine;
        }
        /// <summary>
        /// ViewModel ���丮�� �����ϰ� ����մϴ�
        /// </summary>
        public static LazyBoom WithFactory<TView>(this LazyBoom engine, Func<object> factory)
        {
            engine.RegisterFactory<TView> (factory);
            return engine;
        }

        /// <summary>
        /// View-ViewModel ������ �����ϰ� ����մϴ�
        /// </summary>
        public static LazyBoom WithMapping<TView, TViewModel>(this LazyBoom engine)
            where TView : class
            where TViewModel : class
        {
            engine.RegisterMapping<TView, TViewModel> ();
            return engine;
        }

        /// <summary>
        /// �����̳� �������� �����ϰ� �����մϴ�
        /// </summary>
        public static LazyBoom WithContainerResolver(this LazyBoom engine, Func<Type, object?> containerResolver)
        {
            engine.SetContainerResolver (containerResolver);
            return engine;
        }
    }
}
