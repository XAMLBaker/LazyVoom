using System;

namespace LazyVoom.Core;

public partial class Voom
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
