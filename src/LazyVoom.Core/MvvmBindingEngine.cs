using System.Diagnostics.CodeAnalysis;

namespace LazyVoom.Core
{
    public sealed partial class MvvmBindingEngine
    {
        private static readonly Lazy<MvvmBindingEngine> _instance = new (() => new MvvmBindingEngine ());

        private readonly Dictionary<string, Func<object>> _instanceProviders;
        private readonly Dictionary<string, Type> _typeRegistry;
        private readonly ViewModelResolver _resolver;

        private MvvmBindingEngine()
        {
            _instanceProviders = new Dictionary<string, Func<object>> ();
            _typeRegistry = new Dictionary<string, Type> ();
            _resolver = new ViewModelResolver ();
        }

        public static MvvmBindingEngine Instance => _instance.Value;

        /// <summary>
        /// View에 대응하는 ViewModel을 자동으로 연결합니다
        /// </summary>
        public void EstablishBinding(object targetView, Action<object, object> contextSetter)
        {
            var viewModelInstance = ResolveViewModelInstance (targetView);

            if (viewModelInstance != null)
            {
                contextSetter (targetView, viewModelInstance);
            }
        }

        private object? ResolveViewModelInstance(object view)
        {
            // 1. 타입 기반 등록에서 찾기
            var registeredInstance = GetRegisteredViewModel (view.GetType ());
            if (registeredInstance != null)
                return registeredInstance;

            // 2. 팩토리에서 찾기
            var factoryInstance = CreateFromFactory (view);
            if (factoryInstance != null)
                return factoryInstance;

            // 3. 컨벤션 기반 해결
            return _resolver.ResolveByConvention (view);
        }

        private object? GetRegisteredViewModel(Type viewType)
        {
            var key = viewType.FullName ?? viewType.Name;
            if (!_typeRegistry.ContainsKey (key))
                return null;

            var viewModelType = _typeRegistry[key];

            return _containerResolver != null
             ? _containerResolver (viewModelType) : Activator.CreateInstance(viewModelType);
        }

        private object? CreateFromFactory(object view)
        {
            var key = view.GetType ().FullName ?? view.GetType ().Name;
            return _instanceProviders.TryGetValue (key, out var factory)
                ? factory ()
                : null;
        }

        /// <summary>
        /// View 타입에 대한 ViewModel 팩토리를 등록합니다
        /// </summary>
        public void RegisterFactory<TView>(Func<object> viewModelFactory)
        {
            RegisterFactory (typeof (TView).FullName ?? typeof (TView).Name, viewModelFactory);
        }

        /// <summary>
        /// View 타입명에 대한 ViewModel 팩토리를 등록합니다
        /// </summary>
        public void RegisterFactory(string viewTypeName, Func<object> viewModelFactory)
        {
            _instanceProviders[viewTypeName] = viewModelFactory;
        }

        /// <summary>
        /// View와 ViewModel 타입을 직접 매핑합니다
        /// </summary>
        public void RegisterMapping<
            [DynamicallyAccessedMembers (DynamicallyAccessedMemberTypes.PublicConstructors)] TView,
            [DynamicallyAccessedMembers (DynamicallyAccessedMemberTypes.PublicConstructors)] TViewModel>()
        {
            RegisterMapping (typeof (TView).FullName ?? typeof (TView).Name, typeof (TViewModel));
        }

        /// <summary>
        /// View 타입명과 ViewModel 타입을 매핑합니다
        /// </summary>
        public void RegisterMapping(string viewTypeName, Type viewModelType)
        {
            _typeRegistry[viewTypeName] = viewModelType;
        }

        /// <summary>
        /// DI 컨테이너 리졸버를 제거합니다
        /// </summary>
        public void ClearContainerResolver()
        {
            _containerResolver = null;
        }
        public void SetViewTypeToViewModelTypeResolver(Func<Type, Type?> typeResolver)
        {
            _resolver.SetTypeBasedResolver (typeResolver);
        }

        public void SetViewModelFactory(Func<object, Type?>? instanceResolver)
        {
            _resolver.SetInstanceBasedResolver (instanceResolver);
        }

        public void SetCustomFactory(Func<object, Type, object>? customFactory)
        {
            _resolver.SetCustomFactory (customFactory);
        }
    }
}
