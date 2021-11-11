using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUnit : MonoBehaviour
{
    [Header("Game Unit Config")]
    [SerializeField] float currentSpeed = 1f;
    [SerializeField] float health = 100f;
    [SerializeField] bool isDefender = true;
    [SerializeField] float xSpawnOffset = 0f;
    [SerializeField] float ySpawnOffset = 0f;
    [SerializeField] bool isInvisible = false;

    [Header("Unit Animation Config")]
    [SerializeField] float xDeadOffset = 0f;
    [SerializeField] float yDeadOffset = 0f;

    private bool isMoving;
    private bool isAlive;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        isMoving = true;
        isAlive = true;

        if (isInvisible)
            ChangeSpriteRendererAlpha(0.6f);
    }

    void Update()
    {
        if (isMoving)
        {
            transform.Translate(Vector2.right * Time.deltaTime * currentSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Projectile projectile = collision.gameObject.GetComponent<Projectile>();
        if (projectile != null)
        {
            if (isDefender == true && projectile.GetIsDefenderProjectile() == false)
            {
                ProcessDamage(projectile.GetDamage());
                projectile.Hit();
            }
            if (isDefender == false && projectile.GetIsDefenderProjectile() == true)
            {
                ProcessDamage(projectile.GetDamage());
                projectile.Hit();
            }
        }
    }

    public bool ProcessDamage(float damage)
    {
        health -= damage;
        if (health <= 0 && isAlive)
        {
            isAlive = false;
            StartCoroutine(UnitDead());
        }
        return isAlive;
    }

    private IEnumerator UnitDead()
    {
        MoveOffScreenAfterDies();
        TriggerDeadAnimation();
        yield return new WaitForSeconds(1f);
    }

    private void MoveOffScreenAfterDies()
    {
        gameObject.transform.position = new Vector2(
            gameObject.transform.position.x + xDeadOffset,
            gameObject.transform.position.y + yDeadOffset);
        OffScreenHelper offScreenHelper = FindObjectOfType<OffScreenHelper>();

        offScreenHelper.MoveGameObjectOffScreen(gameObject
            .GetComponentInChildren<AttackRange>()
            .gameObject);
        offScreenHelper.MoveGameObjectOffScreen(gameObject
            .GetComponentInChildren<AttackRange>()
            .gameObject);
    }

    private void ClaimGoldIfIsRewardUnit()
    {
        RewardUnit unit = gameObject.GetComponent<RewardUnit>();
        if (unit != null)
            unit.claimRewardForUnit();
    }

    public void ChangeSpriteRendererAlpha(float alpha)
    {
        SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
        Color tmp = renderer.GetComponent<SpriteRenderer>().color;
        tmp.a = alpha;
        renderer.GetComponent<SpriteRenderer>().color = tmp;
    }

    //Animation Event Function
    public void DestroyUnitEvent()
    {
        Destroy(gameObject);
    }

    public void TriggerDeadAnimation()
    {
        animator.SetTrigger("dead");
        isMoving = false;
        ClaimGoldIfIsRewardUnit();
    }

    public void TriggerAttackAnimation()
    {
        animator.SetTrigger("attack");
        isMoving = false;
    }

    public void TriggerIdleAnimation()
    {
        animator.SetTrigger("idle");
        isMoving = false;
    }

    public void TriggerAttackProjectileAnimation()
    {
        animator.SetTrigger("attackProjectile");
        isMoving = false;
    }

    public void TriggerWalkAnimation()
    {
        if(isAlive == true)
        {
            animator.SetTrigger("walk");
            isMoving = true;
        }
    }

    public float GetCurrentSpeed() { return currentSpeed; }

    public float GetCurrentHealth() { return health; }

    public void SetCurrentHealth(float health) { this.health = health;  }

    public void SetIsMoving(bool moveChoise) { isMoving = moveChoise; }

    public bool GetIsAlive() { return isAlive; }

    public bool GetIsDefender() { return isDefender; }

    public float GetXSpawnOffset() { return xSpawnOffset; }
    
    public float GetYSpawnOffset() { return ySpawnOffset; }

    public bool IsInvisible { get => isInvisible; set => isInvisible = value; }
}
