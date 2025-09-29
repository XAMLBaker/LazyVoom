namespace LazyVoom.Core
{
    public sealed partial class LazyBoom
    {
        Func<Type, object?>? _containerResolver;
        /// <summary>
        /// �����̳� �������� �����մϴ� (���û���)
        /// </summary>
        /// 
        public void SetContainerResolver(Func<Type, object?> resolver)
        {
            _containerResolver = resolver;
        }
    }
}
