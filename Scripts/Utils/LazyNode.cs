using System;
using Godot;

namespace GameOff2023.Scripts.Utils;

public class LazyNode<T> where T : Node
{
    public T Node
    {
        get
        {
            if (_innerNode == null)
            {
                _innerNode = _nodeGetter();
            }
            return _innerNode;
        }
    }
    private T _innerNode;
    
    private readonly Func<T> _nodeGetter;

    public LazyNode(Func<T> nodeGetter)
    {
        _nodeGetter = nodeGetter;
    }
}
