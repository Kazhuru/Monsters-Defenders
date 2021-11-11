using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCanvasLoader : MonoBehaviour
{
    public void ResetGameStatus()
    {
        FindObjectOfType<GameStatus>().ResetGameStatus();
    }

    public void ResetGameStatusFromPause()
    {
        FindObjectOfType<GameMenu>().ResumeGame();
        FindObjectOfType<GameStatus>().ResetGameStatus();
    }

    public void GoToMenuFromPause()
    {
        FindObjectOfType<GameMenu>().ActiveGameTimeScale(true);
        FindObjectOfType<SceneLoader>().LoadMenuScene();
    }
}
