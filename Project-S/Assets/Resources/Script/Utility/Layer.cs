using UnityEngine.InputSystem;

public struct Layer
{
    public static readonly Layer DEFAULT = new Layer("Default", LayerType.DEFAULT);

    public static readonly Layer UI = new Layer("UI", LayerType.UI); // UI

    private readonly int value;
    private readonly LayerType type;
    private Layer(string layerName, LayerType type)
    {
        this.value = UnityEngine.LayerMask.NameToLayer(layerName);
        this.type = type;
    }

    public int Value => value;

    public LayerType Type => type;

    public static implicit operator int(Layer layer)
    {
        return layer.value;
    }
}