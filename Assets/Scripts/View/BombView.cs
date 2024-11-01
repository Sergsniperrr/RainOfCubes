using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class BombView : MonoBehaviour
{
    private Renderer _renderer;
    private Color _color = new(0f, 0f, 0f, 1f);
    private Color _currentColor;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _currentColor = _color;
    }

    public void ChangeTransparency(float value)
    {
        _currentColor.a = value;
        _renderer.material.color = _currentColor;
    }

    public void ResetColor()
    {
        _renderer.material.color = _color;
    }
}
