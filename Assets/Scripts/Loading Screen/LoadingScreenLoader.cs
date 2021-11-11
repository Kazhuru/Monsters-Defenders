using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreenLoader : MonoBehaviour
{
    [SerializeField] float loadingSceneDelay = 3f;

    private void Start()
    {
        SceneLoader sceneLoader = FindObjectOfType<SceneLoader>();
        if (sceneLoader)
        {
            sceneLoader.LoadMenuWithDelay(loadingSceneDelay);
        }
    }
}
