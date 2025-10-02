using System;

namespace LazyVoom.Core
{
    /// <summary>
    /// ViewModel 해결 로직을 담당하는 내부 클래스
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
                    "ViewModel 생성 중 오류가 발생했습니다. 생성자 매개변수를 확인해주세요.", ex);
            }
        }

        private Type? DetermineViewModelType(object view)
        {
            // 인스턴스 기반 해결 시도
            var viewModelType = _instanceResolver?.Invoke (view);
            if (viewModelType != null)
                return viewModelType;

            // 타입 기반 해결 시도
            viewModelType = _typeResolver?.Invoke (view.GetType ());
            if (viewModelType != null)
                return viewModelType;

            // 기본 컨벤션 적용
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

            // Views → ViewModels 네임스페이스 변환
            var viewModelNamespace = viewFullName.Replace (".Views.", ".ViewModels.");

            // 클래스명 변환 (View → ViewModel 또는 단순히 ViewModel 추가)
            var suffix = viewModelNamespace.EndsWith ("View") ? "Model" : "ViewModel";
            var viewModelTypeName = $"{viewModelNamespace}{suffix}, {viewType.Assembly.FullName}";

            return Type.GetType (viewModelTypeName);
        }
    }
}
