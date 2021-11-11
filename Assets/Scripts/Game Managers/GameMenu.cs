using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    [SerializeField] Sprite pauseSprite;
    [SerializeField] Sprite playSprite;
    [SerializeField] GameObject prefabMenuCanvas;

    private Image image;
    private bool isActiveMenu;
    private GameObject menuGui;

    // Cached vars
    private GameStatus gameStatus;

    void Start()
    {
        image = gameObject.GetComponent<Image>();
        isActiveMenu = false;

        gameStatus = FindObjectOfType<GameStatus>();
    }

    public void MenuGameOnClick()
    {
        if (!isActiveMenu)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }  
    }

    public void PauseGame()
    {
        isActiveMenu = true;
        ChangeButtonToPlay();
        gameStatus.SetGameButtonsInteractable(false);

        FindObjectOfType<ScreenShadeHelper>().EnableScreenShade();

        GameObject menuGui = Instantiate(prefabMenuCanvas);
        CanvasCameraHelper.SetGameCameraToCanvas(ref menuGui);

        ActiveGameTimeScale(false);
    }

    public void ResumeGame()
    {
        isActiveMenu = false;
        ChangeButtonToPause();
        gameStatus.SetGameButtonsInteractable(true);

        FindObjectOfType<ScreenShadeHelper>().DisableScreenShade();

        var canvas = GameObject.FindGameObjectWithTag("Popup Canvas");
        Destroy(canvas);

        ActiveGameTimeScale(true);
    }

    public void ActiveGameTimeScale(bool active)
    {
        if(active)
            Time.timeScale = 1;
        else
            Time.timeScale = 0;
    }

    private void ChangeButtonToPause() { image.sprite = pauseSprite; }

    private void ChangeButtonToPlay() { image.sprite = playSprite; }
}
