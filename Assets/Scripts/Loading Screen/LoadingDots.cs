using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadingDots : MonoBehaviour
{
    [SerializeField] float loopSpeed = 1f;
    private TextMeshProUGUI textMesh;

    private void Start()
    {
        textMesh = gameObject.GetComponent<TextMeshProUGUI>();
        StartCoroutine(DotsLoop());
    }

    IEnumerator DotsLoop()
    {
        while (true)
        {
            if (textMesh.text.ToCharArray().Length >= 3)
            {
                textMesh.text = "";
            }
            else
            {
                textMesh.text = textMesh.text + ".";
            }
            yield return new WaitForSeconds(loopSpeed);
        }
    }
}
