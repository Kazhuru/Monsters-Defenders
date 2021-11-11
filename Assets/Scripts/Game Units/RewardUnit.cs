using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RewardUnit : MonoBehaviour
{

    [SerializeField] int goldReward = 50;
    [SerializeField] GameObject rewardAnimationPrefab;

    private GameStatus gameStatus;
    GameObject animationGameObj;

    void Start()
    {
        gameStatus = FindObjectOfType<GameStatus>();
        animationGameObj = null;
    }

    public void claimRewardForUnit()
    {
        gameStatus.IncreaseCurrentGold(goldReward);
        GameObject animationGameObj = Instantiate(rewardAnimationPrefab, gameObject.transform.position, Quaternion.identity);
        animationGameObj.GetComponentInChildren<TextMeshPro>().text = "+" + goldReward.ToString();
    }
}
