using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSettings : MonoBehaviour
{
    [SerializeField] AudioClip sceneAudioClip;
    [SerializeField] [Range(0f, 1f)] float musicVolume = 1f;

    private MusicManager musicManager;

    public float MusicVolume { get => musicVolume; set => musicVolume = value; }

    public AudioClip GetSceneAudioClip() { return sceneAudioClip; }

    private void Start()
    {
        musicManager = FindObjectOfType<MusicManager>();

        if (sceneAudioClip != null && musicManager != null)
        {
            musicManager.Play(sceneAudioClip, musicVolume);
        }
    }
}
