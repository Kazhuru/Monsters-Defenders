using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatMelee : MonoBehaviour
{
    [SerializeField] private AudioClip swingSFX;
    [SerializeField] [Range(0f, 1f)] float swingVolume = 0.1f;
    [SerializeField] float damageValue;
    [SerializeField] float damageInterval;

    private GameUnit gameUnit;
    private bool onCombat;
    private bool onIdle;
    private Coroutine attackIntervalCoroutine;

    private const float MELEE_DELAY = 0.6f;

    public bool OnCombat { get => onCombat; set => onCombat = value; }
    public bool OnIdle { get => onIdle; set => onIdle = value; }

    public void Start()
    {
        onCombat = false;
        gameUnit = gameObject.GetComponent<GameUnit>();
    }

    public void StartMeleeCombat(GameObject damageTarget)
    {
        onCombat = true;
        if (gameUnit.IsInvisible)
        {
            gameUnit.ChangeSpriteRendererAlpha(1f);
            gameUnit.IsInvisible = false;
        }
            
        gameUnit.TriggerAttackAnimation();
        StartCoroutine(StarMeleeCombatWithDelay(damageTarget));
    }

    public void EndMeleeCombat()
    {
        onCombat = false;
        if(attackIntervalCoroutine != null)
            StopCoroutine(attackIntervalCoroutine);
        gameUnit.TriggerWalkAnimation();
    }

    private IEnumerator CombatDamageInterval(GameObject damageTarget)
    {
        while(onCombat == true)
        {
            var unitTarget = damageTarget.GetComponent<GameUnit>(); 
            if(unitTarget.GetIsAlive())
            {
                unitTarget.ProcessDamage(damageValue);
            }
            yield return new WaitForSeconds(damageInterval);
        }
    }

    public void StartMeleeFriendlyQueue()
    {
        onIdle = true;
        gameUnit.TriggerIdleAnimation();
    }

    public void EndMeleeFriendlyQueue()
    {
        onIdle = false;
        gameUnit.TriggerWalkAnimation();
    }

    private IEnumerator StarMeleeCombatWithDelay(GameObject damageTarget)
    {
        yield return new WaitForSeconds(MELEE_DELAY);
        attackIntervalCoroutine = StartCoroutine(CombatDamageInterval(damageTarget));
    }

    //Animation Event Function
    public void MeleeSFXEvent()
    {
        AudioSource.PlayClipAtPoint(swingSFX, Camera.main.transform.position, swingVolume);
    }

    //Animation Event Function
    public void MeleeExtraAnimationEvent(GameObject inputGameObject)
    {
        AnimationsEvents animEvents = inputGameObject.GetComponent<AnimationsEvents>();
        if(animEvents != null)
        {
            Quaternion quaternion;
            if (!gameUnit.GetIsDefender())
                quaternion = Quaternion.Euler(0f, 180f, 0f);
            else
                quaternion = Quaternion.identity;

            Instantiate(inputGameObject, new Vector2(
                    gameObject.transform.position.x + animEvents.XInstanceOffSet,
                    gameObject.transform.position.y + animEvents.YInstanceOffSet),
                    quaternion);
        }
    }
}
