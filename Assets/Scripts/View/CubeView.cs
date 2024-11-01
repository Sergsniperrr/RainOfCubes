using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class CubeView : MonoBehaviour
{
    private Renderer _renderer;
    private Color[] _colors = { Color.green, Color.yellow, Color.red, Color.white };
    private Queue<Color> _colorAssigner = new Queue<Color>();

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();

        UpdateColors();
    }

    public void ChangeColor()
    {
        _renderer.material.color = _colorAssigner.Dequeue();
    }

    public void UpdateColors()
    {
        _colorAssigner = new Queue<Color>(_colors);
    }

}
