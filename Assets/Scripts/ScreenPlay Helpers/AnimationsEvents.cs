using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsEvents : MonoBehaviour
{
    [SerializeField] float xInstanceOffSet = 0f;
    [SerializeField] float yInstanceOffSet = 0f;

    // For position adjustment when creating a animation in another animation event
    public float XInstanceOffSet { get => xInstanceOffSet; set => xInstanceOffSet = value; }
    public float YInstanceOffSet { get => yInstanceOffSet; set => yInstanceOffSet = value; }

    public void DestroyAllAnimationEvent()
    {
        Destroy(gameObject.transform.parent.gameObject);
    }

    public void DestroyAnimationEvent()
    {
        Destroy(gameObject);
    }
}
