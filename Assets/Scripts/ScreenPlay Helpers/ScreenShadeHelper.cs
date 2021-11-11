using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShadeHelper : MonoBehaviour
{
    [SerializeField] float shadeAlpha = 0.2f;

    public void EnableScreenShade()
    {
        SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
        Color tmp = renderer.GetComponent<SpriteRenderer>().color;
        tmp.a = shadeAlpha;
        renderer.GetComponent<SpriteRenderer>().color = tmp;
    }

    public void DisableScreenShade()
    {
        SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
        Color tmp = renderer.GetComponent<SpriteRenderer>().color;
        tmp.a = 0;
        renderer.GetComponent<SpriteRenderer>().color = tmp;
    }
}
