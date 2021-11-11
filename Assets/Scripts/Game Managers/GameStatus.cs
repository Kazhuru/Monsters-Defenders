using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GameStatus : MonoBehaviour
{
    [Header("Gold Status")]
    [SerializeField] int startingGold;
    [SerializeField] Text goldText;
    [Header("Health Status")]
    [SerializeField] GameObject hpText;
    [SerializeField] GameObject hpBar;
    [SerializeField] GameObject hpIcon;
    [SerializeField] float hpBarWidht;
    [SerializeField] float hpDamagePerUnit = 10;
    [SerializeField] float hpReductionInterval = 0.1f;
    [SerializeField] float startLowHpAnim = 35f;
    [Header("GameOver config")]
    [SerializeField] float delayAfterGameover = 1f;
    [SerializeField] GameObject gameOverBoard;
    [SerializeField] AudioClip gameOverAudioClip;
    [SerializeField] [Range(0f, 1f)] float gameOverVolume = 1f;

    private int currentGold;
    private float currentHpPercentage;

    private RectTransform hpBarRect;
    private TextMeshPro hpTextTMpro;
    private bool lowHpAnimSet;
    private bool isGameOver;


    public int CurrentGold { get => currentGold; set => currentGold = value; }
    public float CurrentHpPercentage { get => currentHpPercentage; set => currentHpPercentage = value; }
    public bool IsGameOver { get => isGameOver; set => isGameOver = value; }

    private void Start()
    {
        hpBarRect = hpBar.GetComponent<RectTransform>();
        hpTextTMpro = hpText.GetComponent<TextMeshPro>();

        RestartStatusVariables();
    }
    private void Update()
    {
        goldText.text = currentGold.ToString();
        hpTextTMpro.text = currentHpPercentage.ToString() + "%";
    }

    public void ReduceCurrentGold(int defenderCost)
    {
        currentGold -= defenderCost;
        if (currentGold < 0)
            currentGold = 0;
    }

    public void IncreaseCurrentGold(int defenderCost)
    {
        currentGold += defenderCost;
    }

    public void ReduceHealthPoints()
    {
        currentHpPercentage -= hpDamagePerUnit;
        if(currentHpPercentage < startLowHpAnim && !lowHpAnimSet)
        {
            StartLowHpAnimations();
            lowHpAnimSet = true;
        }
        StartCoroutine(SmoothBarHpChange());

        if (currentHpPercentage <= 0)
        {
            currentHpPercentage = 0;
            StartCoroutine(StartGameOverStatus());
        } 
    }

    private IEnumerator SmoothBarHpChange()
    {
        bool onChange = true;
        float previousPercentage = currentHpPercentage + hpDamagePerUnit;

        while (onChange)
        {
            float resultWidht = (previousPercentage / 100) * hpBarWidht;
            hpBarRect.sizeDelta = new Vector2(resultWidht, hpBarRect.sizeDelta.y);

            if (previousPercentage <= currentHpPercentage)
                onChange = false;
            else
                previousPercentage -= 0.25f;

            yield return new WaitForSeconds(hpReductionInterval);
        }
    }

    private IEnumerator StartGameOverStatus()
    {
        isGameOver = true;
        GameTimer.instance.EndTimer();
        SetGameButtonsInteractable(false);

        yield return new WaitForSeconds(delayAfterGameover);

        MusicManager musicManager = FindObjectOfType<MusicManager>();
        if (gameOverAudioClip != null && musicManager != null)
            musicManager.Play(gameOverAudioClip, gameOverVolume);

        GameObject gameOverGui = Instantiate(gameOverBoard);
        CanvasCameraHelper.SetGameCameraToCanvas(ref gameOverGui);
    }

    public void ResetGameStatus()
    {
        RestartStatusVariables();
        StartNormalHpAnimations();
        StartAgainGameMusic();
        ClearGameObjects();
        SetGameButtonsInteractable(true);
    }

    private void ClearGameObjects()
    {
        var units = FindObjectsOfType<GameUnit>();
        foreach (var unit in units)
            Destroy(unit.gameObject);

        var projectiles = FindObjectsOfType<Projectile>();
        foreach (var projectile in projectiles)
            Destroy(projectile.gameObject);

        var canvasArray = GameObject.FindGameObjectsWithTag("Popup Canvas");
        foreach (GameObject canvasObj in canvasArray)
            Destroy(canvasObj);
            
    }

    private static void StartAgainGameMusic()
    {
        MusicManager musicManager = FindObjectOfType<MusicManager>();
        SceneSettings sceneSettings = FindObjectOfType<SceneSettings>();
        if (sceneSettings != null && musicManager != null)
        {
            musicManager.Play(sceneSettings.GetSceneAudioClip(), sceneSettings.MusicVolume);
        }
    }

    private void RestartStatusVariables()
    {
        lowHpAnimSet = false;
        isGameOver = false;
        currentGold = startingGold;
        currentHpPercentage = 100;
        hpBarRect.sizeDelta = new Vector2(hpBarWidht, hpBarRect.sizeDelta.y);
        GameTimer.instance.BeginTimer();
    }

    private void StartLowHpAnimations()
    {
        if (hpIcon != null)
            hpIcon.GetComponent<Animator>().SetTrigger("lowHP");
        if (hpBar != null)
            hpBar.GetComponent<Animator>().SetTrigger("lowHP");
    }

    private void StartNormalHpAnimations()
    {
        if (hpIcon != null)
            hpIcon.GetComponent<Animator>().SetTrigger("normalHP");
        if (hpBar != null)
            hpBar.GetComponent<Animator>().SetTrigger("normalHP");
    }

    public void SetGameButtonsInteractable(bool setValue)
    {
        var buttonsObjs = GameObject.FindGameObjectsWithTag("Game Button");
        foreach (var button in buttonsObjs)
        {
            button.GetComponent<Button>().interactable = setValue;
        }
    }
}
