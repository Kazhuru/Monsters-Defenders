using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Projectile Config")]
    [SerializeField] float damage = 10;
    [SerializeField] bool isDefenderProjectile = true;
    [SerializeField] bool hasCreationAnimation = false;
    [SerializeField] float projectileSpeed = 1f;

    [Header("Projectile SFX")]
    [SerializeField] AudioClip castSFX;
    [SerializeField] [Range(0f, 1f)] float castVolume = 1f;
    [SerializeField] AudioClip explotionSFX;
    [SerializeField] [Range(0f, 1f)] float explotionVolume = 1f;

    public void Start()
    {
        if (!isDefenderProjectile)
            projectileSpeed = -projectileSpeed;
    }

    public void Hit()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        AudioSource.PlayClipAtPoint(explotionSFX, Camera.main.transform.position, explotionVolume);
        GetComponent<Animator>().SetTrigger("collition");
    }

    public void DestroyProjectileEvent()
    {
        Destroy(gameObject);
    }

    public void Shoot()
    {
        AudioSource.PlayClipAtPoint(castSFX, Camera.main.transform.position, castVolume);
        if(!hasCreationAnimation)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileSpeed, 0);
        }
    }

    public void ShootAfterCreationEvent()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileSpeed, 0);
    }

    public float GetDamage() { return damage; }
    public bool GetIsDefenderProjectile() { return isDefenderProjectile; }
}
