using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasCameraHelper : MonoBehaviour
{
    public static void SetGameCameraToCanvas(ref GameObject canvasObject)
    {
        canvasObject.GetComponent<Canvas>().worldCamera =
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
}
