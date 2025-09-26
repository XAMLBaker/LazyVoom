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
        /// View�� �����ϴ� ViewModel�� �ڵ����� �����մϴ�
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
            // 1. Ÿ�� ��� ��Ͽ��� ã��
            var registeredInstance = GetRegisteredViewModel (view.GetType ());
            if (registeredInstance != null)
                return registeredInstance;

            // 2. ���丮���� ã��
            var factoryInstance = CreateFromFactory (view);
            if (factoryInstance != null)
                return factoryInstance;

            // 3. ������ ��� �ذ�
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
        /// View Ÿ�Կ� ���� ViewModel ���丮�� ����մϴ�
        /// </summary>
        public void RegisterFactory<TView>(Func<object> viewModelFactory)
        {
            RegisterFactory (typeof (TView).FullName ?? typeof (TView).Name, viewModelFactory);
        }

        /// <summary>
        /// View Ÿ�Ը� ���� ViewModel ���丮�� ����մϴ�
        /// </summary>
        public void RegisterFactory(string viewTypeName, Func<object> viewModelFactory)
        {
            _instanceProviders[viewTypeName] = viewModelFactory;
        }

        /// <summary>
        /// View�� ViewModel Ÿ���� ���� �����մϴ�
        /// </summary>
        public void RegisterMapping<
            [DynamicallyAccessedMembers (DynamicallyAccessedMemberTypes.PublicConstructors)] TView,
            [DynamicallyAccessedMembers (DynamicallyAccessedMemberTypes.PublicConstructors)] TViewModel>()
        {
            RegisterMapping (typeof (TView).FullName ?? typeof (TView).Name, typeof (TViewModel));
        }

        /// <summary>
        /// View Ÿ�Ը�� ViewModel Ÿ���� �����մϴ�
        /// </summary>
        public void RegisterMapping(string viewTypeName, Type viewModelType)
        {
            _typeRegistry[viewTypeName] = viewModelType;
        }

        /// <summary>
        /// DI �����̳� �������� �����մϴ�
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
