using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    [SerializeField] float firstAudioCrossFade = 0.05f;
    [SerializeField] float audioCrossFade = 0.4f;

    private AudioSource audioSourcePlayerOne;
    private AudioSource audioSourcePlayerTwo;

    public AudioMixer AudMix;
    public AudioMixerGroup[] AudioMixerGroups;

    const string EXPOSED_PARAM_PLAYER_ONE = "playerOneVol";
    const string EXPOSED_PARAM_PLAYER_TWO = "playerTwoVol";

    private void Awake()
    {
        int musicManagerCounter = FindObjectsOfType<MusicManager>().Length;
        if (musicManagerCounter > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        audioSourcePlayerOne = gameObject.GetComponents<AudioSource>()[0];
        audioSourcePlayerTwo = gameObject.GetComponents<AudioSource>()[1];
    }

    public void Play(AudioClip sceneAudioClip, float audioVolume)
    {
        if (audioSourcePlayerOne.clip == null && audioSourcePlayerOne.clip == null)
        {
            audioSourcePlayerOne.clip = sceneAudioClip;
            StartCoroutine(StartAudioCoroutine(
                audioSourcePlayerOne,
                EXPOSED_PARAM_PLAYER_ONE,
                firstAudioCrossFade,
                audioVolume));
        }
        else
        {
            if (audioSourcePlayerOne.isPlaying)
            {   //change to TwoAudioPlayer input audioClip
                if (audioSourcePlayerOne.clip.name != sceneAudioClip.name)
                {
                    StartCoroutine(StopAudioCoroutine(
                        audioSourcePlayerOne,
                        EXPOSED_PARAM_PLAYER_ONE,
                        audioCrossFade));

                    audioSourcePlayerTwo.clip = sceneAudioClip;
                    StartCoroutine(StartAudioCoroutine(
                        audioSourcePlayerTwo,
                        EXPOSED_PARAM_PLAYER_TWO,
                        audioCrossFade,
                        audioVolume));
                }
            }
            else if (audioSourcePlayerTwo.isPlaying)
            {   //change to OneAudioPlayer input audioClip
                if (audioSourcePlayerTwo.clip.name != sceneAudioClip.name)
                {
                    StartCoroutine(StopAudioCoroutine(
                        audioSourcePlayerTwo,
                        EXPOSED_PARAM_PLAYER_TWO,
                        audioCrossFade));

                    audioSourcePlayerOne.clip = sceneAudioClip;
                    StartCoroutine(StartAudioCoroutine(
                        audioSourcePlayerOne,
                        EXPOSED_PARAM_PLAYER_ONE,
                        audioCrossFade, 
                        audioVolume));
                }
            }
        }
    }

    IEnumerator StopAudioCoroutine(AudioSource audioSource, string param, float fadeTime)
    {
        yield return StartCoroutine(FadeMixerGroup.StartFade(
                audioSource.outputAudioMixerGroup.audioMixer,
                param, fadeTime, 0));
        audioSource.Stop();
    }

    IEnumerator StartAudioCoroutine(AudioSource audioSource, string param, float fadeTime, float audioVolume)
    {
        audioSource.Play();
        yield return StartCoroutine(FadeMixerGroup.StartFade(
                audioSource.outputAudioMixerGroup.audioMixer,
                param, fadeTime, audioVolume));
    }
}
