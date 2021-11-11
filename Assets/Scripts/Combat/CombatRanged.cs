using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatRanged : MonoBehaviour
{
    [SerializeField] Projectile projectilePrefab;
    [SerializeField] float xProjectileOffset = 0f;
    [SerializeField] float yProjectileOffset = 0f;

    [SerializeField] float projectileInterval = 1f;
    
    private GameUnit gameUnit;
    private int counterOfEnemiesOnRange;
    private bool combatStatus;
    private bool onMeleeCombat;
    private bool coroutineRunning;
    private Coroutine attackIntervalCoroutine;
    private CombatMelee combatMelee;

    public void Start()
    {
        gameUnit = gameObject.GetComponent<GameUnit>();
        combatMelee = gameUnit.gameObject.GetComponent<CombatMelee>();
        counterOfEnemiesOnRange = 0;
        combatStatus = false;
        onMeleeCombat = false;
        coroutineRunning = false;
    }

    private void Update()
    {
        bool lastCombatStatus = combatStatus;
        UpdateCombatStatus();
        onMeleeCombat = combatMelee.OnCombat;

        if (!onMeleeCombat && gameUnit.GetIsAlive())
        {
            if(combatStatus)
            {
                StartCombatCoroutine();
            }
            else
            {
                EndCombatCoroutine();
            }
        }
        else
        {
            EndCombatCoroutine();
        }
    }

    private void StartCombatCoroutine()
    {
        if (!coroutineRunning)
        {
            attackIntervalCoroutine = StartCoroutine(CombatProjectileFireInterval());
        }
    }

    private void EndCombatCoroutine()
    {
        if(attackIntervalCoroutine != null)
        {
            StopCoroutine(attackIntervalCoroutine);
            attackIntervalCoroutine = null;
        }
        coroutineRunning = false;
    }

    public void UnitOnRangeCombat()
    {
        counterOfEnemiesOnRange++;
    }

    public void UnitOffRangeCombat()
    {
        counterOfEnemiesOnRange--;
    }

    private void UpdateCombatStatus()
    {
        if (counterOfEnemiesOnRange < 0)
            counterOfEnemiesOnRange = 0;
        if (counterOfEnemiesOnRange == 0)
            combatStatus = false;
        else
            combatStatus = true;
    }

    private IEnumerator CombatProjectileFireInterval()
    {
        coroutineRunning = true;
        while (combatStatus == true)
        {
            gameUnit.TriggerAttackProjectileAnimation();
            Vector2 positionInstance = new Vector2(
                transform.position.x + xProjectileOffset,
                transform.position.y + yProjectileOffset);
            if (gameUnit.GetIsDefender())
            {
                Projectile projectile = Instantiate(projectilePrefab, positionInstance, Quaternion.identity);
                projectile.Shoot();
            }

            else
            {
                Projectile projectile = Instantiate(projectilePrefab, positionInstance, Quaternion.Euler(0f, 180f, 0f));
                projectile.Shoot();
            }
                
            yield return new WaitForSeconds(projectileInterval);
        }
        coroutineRunning = false;
    }

    //Animation Event Function
    public void EndAttackProjectileEvent()
    {
        if (gameUnit.GetIsAlive())
            gameUnit.TriggerWalkAnimation();
    }

    public bool OnMeleeCombat { get => onMeleeCombat; set => onMeleeCombat = value; }
}
