using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private Color _color;

    private void OnCollisionEnter(Collision collision)
    {
        ChangeColor(collision.gameObject);       
    }

    private void ChangeColor(GameObject cube)
    {
        cube.GetComponent<Renderer>().material.color = _color;
    }
}
