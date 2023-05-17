using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeTransparent : MonoBehaviour
{

    Renderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        renderer.material.color = new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, 0.2f);
    }
}
