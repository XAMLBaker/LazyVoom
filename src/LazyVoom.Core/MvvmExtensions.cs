namespace LazyVoom.Core
{
    /// <summary>
    /// MVVM 바인딩을 위한 확장 메서드들
    /// </summary>
    public static class MvvmExtensions
    {
        /// <summary>
        /// View에 ViewModel을 자동으로 바인딩합니다
        /// </summary>
        public static void AutoBindViewModel(this object view, Action<object, object> setContext)
        {
            MvvmBindingEngine.Instance.EstablishBinding (view, setContext);
        }
        /// <summary>
        /// 컨벤션을 간편하게 설정합니다
        /// </summary>
        public static MvvmBindingEngine WithConvention(this MvvmBindingEngine engine, Func<Type, Type?> typeResolver)
        {
            engine.SetViewTypeToViewModelTypeResolver (typeResolver);
            return engine;
        }
        /// <summary>
        /// ViewModel 팩토리를 간편하게 등록합니다
        /// </summary>
        public static MvvmBindingEngine WithFactory<TView>(this MvvmBindingEngine engine, Func<object> factory)
        {
            engine.RegisterFactory<TView> (factory);
            return engine;
        }

        /// <summary>
        /// View-ViewModel 매핑을 간편하게 등록합니다
        /// </summary>
        public static MvvmBindingEngine WithMapping<TView, TViewModel>(this MvvmBindingEngine engine)
            where TView : class
            where TViewModel : class
        {
            engine.RegisterMapping<TView, TViewModel> ();
            return engine;
        }

        /// <summary>
        /// 컨테이너 리졸버를 간편하게 설정합니다
        /// </summary>
        public static MvvmBindingEngine WithContainerResolver(this MvvmBindingEngine engine, Func<Type, object?> containerResolver)
        {
            engine.SetContainerResolver (containerResolver);
            return engine;
        }
    }
}
