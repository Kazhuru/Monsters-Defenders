using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttachDefenderInfo : MonoBehaviour
{
    [SerializeField] private string defenderName;
    [SerializeField] private Sprite defenderSprite;
    [SerializeField] private int defenderCost;
    [SerializeField] Text costText;

    private void Start()
    {
        costText.text = defenderCost.ToString();
    }

    public string DefenderName { get => defenderName; set => defenderName = value; }
    public Sprite DefenderSprite { get => defenderSprite; set => defenderSprite = value; }
    public int DefenderCost { get => defenderCost; set => defenderCost = value; }
}
