using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HtpTextManager : MonoBehaviour
{
    [SerializeField] Text[] howToPlaySlide;
    [SerializeField] Text displayText;

    private int textIndex = 0;

    private void Start()
    {
        displayText.text = howToPlaySlide[textIndex].text;
    }

    public void changeTextSlideToRight()
    {
        textIndex++;
        if (textIndex >= howToPlaySlide.Length)
            textIndex = 0;

        displayText.text = howToPlaySlide[textIndex].text;
    }

    public void changeTextSlideToLeft()
    {
        textIndex--;
        if (textIndex < 0)
            textIndex = howToPlaySlide.Length - 1;

        displayText.text = howToPlaySlide[textIndex].text;
    }
}
