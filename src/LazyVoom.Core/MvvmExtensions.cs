using System;

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
            Voom.Instance.EstablishBinding (view, setContext);
        }
        /// <summary>
        /// �������� �����ϰ� �����մϴ�
        /// </summary>
        public static Voom WithConvention(this Voom engine, Func<Type, Type?> typeResolver)
        {
            engine.SetViewTypeToViewModelTypeResolver (typeResolver);
            return engine;
        }
        /// <summary>
        /// ViewModel ���丮�� �����ϰ� ����մϴ�
        /// </summary>
        public static Voom WithFactory<TView>(this Voom engine, Func<object> factory)
        {
            engine.RegisterFactory<TView> (factory);
            return engine;
        }

        /// <summary>
        /// View-ViewModel ������ �����ϰ� ����մϴ�
        /// </summary>
        public static Voom WithMapping<TView, TViewModel>(this Voom engine)
            where TView : class
            where TViewModel : class
        {
            engine.RegisterMapping<TView, TViewModel> ();
            return engine;
        }

        /// <summary>
        /// �����̳� �������� �����ϰ� �����մϴ�
        /// </summary>
        public static Voom WithContainerResolver(this Voom engine, Func<Type, object?> containerResolver)
        {
            engine.SetContainerResolver (containerResolver);
            return engine;
        }
    }
}
