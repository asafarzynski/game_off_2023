using Godot;

namespace GameOff2023.Scripts.Utils;

public partial class NodeSingleton<T> : Node where T : NodeSingleton<T>
{
    public static T Instance { get; private set; }

    public NodeSingleton()
    {
        if (Instance == null)
        {
            Instance = (T)this;
        }
        else
        {
            Free();
        }
    }
}