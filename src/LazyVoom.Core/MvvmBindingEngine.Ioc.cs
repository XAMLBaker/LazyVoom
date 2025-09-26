namespace LazyVoom.Core
{
    public sealed partial class MvvmBindingEngine
    {
        Func<Type, object?>? _containerResolver;
        /// <summary>
        /// 컨테이너 리졸버를 설정합니다 (선택사항)
        /// </summary>
        /// 
        public void SetContainerResolver(Func<Type, object?> resolver)
        {
            _containerResolver = resolver;
        }
    }
}
