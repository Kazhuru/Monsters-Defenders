using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffScreenHelper : MonoBehaviour
{
    public void MoveGameObjectOffScreen(GameObject gameObj)
    {
        gameObj.transform.position = new Vector2(transform.position.x, transform.position.y);
    }
}
