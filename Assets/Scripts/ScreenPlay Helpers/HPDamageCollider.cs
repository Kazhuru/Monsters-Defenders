using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPDamageCollider : MonoBehaviour
{

    private GameStatus gameStatus;

    private void Start()
    {
        gameStatus = FindObjectOfType<GameStatus>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameUnit unit = collision.gameObject.GetComponent<GameUnit>();
        if(!gameStatus.IsGameOver)
        {
            if (unit != null)
            {
                if (!unit.GetIsDefender())
                {
                    gameStatus.ReduceHealthPoints();
                }
            }
        }
    }
}
