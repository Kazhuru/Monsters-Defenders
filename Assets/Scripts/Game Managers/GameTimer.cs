using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public static GameTimer instance;

    [SerializeField] Text timeCounter;

    private TimeSpan timePlaying;
    private bool timerGoing = false;
    private Coroutine coroutineTimer = null;

    private float elapsedTime;

    public float ElapsedTime { get => elapsedTime; set => elapsedTime = value; }

    private void Awake()
    {
        instance = this;
    }

    public void BeginTimer()
    {
        timerGoing = true;
        elapsedTime = 0f;

        if(coroutineTimer == null)
            coroutineTimer = StartCoroutine(UpdateTimer());
    }

    public void EndTimer()
    {
        timerGoing = false;
        coroutineTimer = null;
    }

    private IEnumerator UpdateTimer()
    {
        while(timerGoing)
        {
            elapsedTime += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);
            string timePlayingString = timePlaying.ToString("mm\\:ss");
            timeCounter.text = timePlayingString;

            yield return null;
        }
    }
}
