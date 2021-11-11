using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    MusicManager musicManager;

    private void Start()
    {
        musicManager = FindObjectOfType<MusicManager>();
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Game Scene");
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadScene("Menu Scene");
    }

    public void LoadHowToPlayScene()
    {
        SceneManager.LoadScene("How To Play Scene");
    }

    public void LoadGameOverScene()
    {
        SceneManager.LoadScene("GameOver Scene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadMenuWithDelay(float delay)
    {
        StartCoroutine(WaitAndLoadMenuScene(delay));
    }

    private IEnumerator WaitAndLoadMenuScene(float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);
        LoadMenuScene();
    }

    public void LoadGameOverWithDelay(float delay)
    {
        StartCoroutine(WaitAndLoadGameOverScene(delay));
    }

    private IEnumerator WaitAndLoadGameOverScene(float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);
        LoadGameOverScene();
    }
}
