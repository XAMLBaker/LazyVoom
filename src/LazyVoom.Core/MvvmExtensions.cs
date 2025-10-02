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
            LazyVoom.Instance.EstablishBinding (view, setContext);
        }
        /// <summary>
        /// �������� �����ϰ� �����մϴ�
        /// </summary>
        public static LazyVoom WithConvention(this LazyVoom engine, Func<Type, Type?> typeResolver)
        {
            engine.SetViewTypeToViewModelTypeResolver (typeResolver);
            return engine;
        }
        /// <summary>
        /// ViewModel ���丮�� �����ϰ� ����մϴ�
        /// </summary>
        public static LazyVoom WithFactory<TView>(this LazyVoom engine, Func<object> factory)
        {
            engine.RegisterFactory<TView> (factory);
            return engine;
        }

        /// <summary>
        /// View-ViewModel ������ �����ϰ� ����մϴ�
        /// </summary>
        public static LazyVoom WithMapping<TView, TViewModel>(this LazyVoom engine)
            where TView : class
            where TViewModel : class
        {
            engine.RegisterMapping<TView, TViewModel> ();
            return engine;
        }

        /// <summary>
        /// �����̳� �������� �����ϰ� �����մϴ�
        /// </summary>
        public static LazyVoom WithContainerResolver(this LazyVoom engine, Func<Type, object?> containerResolver)
        {
            engine.SetContainerResolver (containerResolver);
            return engine;
        }
    }
}
