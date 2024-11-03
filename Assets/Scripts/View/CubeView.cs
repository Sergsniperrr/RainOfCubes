using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class CubeView : MonoBehaviour
{
    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();

        ResetColor();
    }

    public void ChangeColor(Color color)
    {
        _renderer.material.color = color;
    }

    public void ResetColor()
    {
        ChangeColor(Color.white);
    }

}
