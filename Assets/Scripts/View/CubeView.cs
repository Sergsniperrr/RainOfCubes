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

    public void SetRandomColor()
    {
        float minHue = 0;
        float maxHue = 1;
        float minSaturation = 1f;
        float maxSaturation = 1f;
        float minValue = 1f;
        float maxValue = 1;

        _renderer.material.color = Random.ColorHSV(minHue, maxHue, minSaturation, maxSaturation, minValue, maxValue);
    }

    public void ResetColor()
    {
        _renderer.material.color = Color.white;
    }

}
