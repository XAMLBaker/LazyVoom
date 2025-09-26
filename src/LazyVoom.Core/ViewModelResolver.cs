namespace LazyVoom.Core
{
    /// <summary>
    /// ViewModel �ذ� ������ ����ϴ� ���� Ŭ����
    /// </summary>
    internal class ViewModelResolver
    {
        private Func<Type, Type?>? _typeResolver;
        private Func<object, Type?>? _instanceResolver;
        private Func<object, Type, object>? _customFactory;
        private readonly Func<Type, object> _defaultActivator;

        public ViewModelResolver()
        {
            _defaultActivator = type => Activator.CreateInstance (type)
                ?? throw new InvalidOperationException ($"Cannot create instance of {type.Name}");
        }

        public void Configure(
            Func<Type, Type?>? typeResolver,
            Func<object, Type?>? instanceResolver,
            Func<object, Type, object>? customFactory)
        {
            _typeResolver = typeResolver;
            _instanceResolver = instanceResolver;
            _customFactory = customFactory;
        }
        public void SetTypeBasedResolver(Func<Type, Type?>? resolver)
        {
            _typeResolver = resolver;
        }

        public void SetInstanceBasedResolver(Func<object, Type?>? resolver)
        {
            _instanceResolver = resolver;
        }

        public void SetCustomFactory(Func<object, Type, object>? factory)
        {
            _customFactory = factory;
        }

        public object? ResolveByConvention(object view)
        {
            try
            {
                var viewModelType = DetermineViewModelType (view);
                if (viewModelType == null)
                    return null;

                return CreateViewModelInstance (view, viewModelType);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException (
                    "ViewModel ���� �� ������ �߻��߽��ϴ�. ������ �Ű������� Ȯ�����ּ���.", ex);
            }
        }

        private Type? DetermineViewModelType(object view)
        {
            // �ν��Ͻ� ��� �ذ� �õ�
            var viewModelType = _instanceResolver?.Invoke (view);
            if (viewModelType != null)
                return viewModelType;

            // Ÿ�� ��� �ذ� �õ�
            viewModelType = _typeResolver?.Invoke (view.GetType ());
            if (viewModelType != null)
                return viewModelType;

            // �⺻ ������ ����
            return ApplyNamingConvention (view.GetType ());
        }

        private object CreateViewModelInstance(object view, Type viewModelType)
        {
            return _customFactory?.Invoke (view, viewModelType)
                ?? _defaultActivator (viewModelType);
        }

        private static Type? ApplyNamingConvention(Type viewType)
        {
            var viewFullName = viewType.FullName;
            if (string.IsNullOrEmpty (viewFullName))
                return null;

            // Views �� ViewModels ���ӽ����̽� ��ȯ
            var viewModelNamespace = viewFullName.Replace (".Views.", ".ViewModels.");

            // Ŭ������ ��ȯ (View �� ViewModel �Ǵ� �ܼ��� ViewModel �߰�)
            var suffix = viewModelNamespace.EndsWith ("View") ? "Model" : "ViewModel";
            var viewModelTypeName = $"{viewModelNamespace}{suffix}, {viewType.Assembly.FullName}";

            return Type.GetType (viewModelTypeName);
        }
    }
}
