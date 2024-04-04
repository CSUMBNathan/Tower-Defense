
using System;
using Unity.Mathematics;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float startSpeed = 10f;
    public float health = 100f;
    [HideInInspector]
    public float speed;
    
    public int enemyValue = 50;

    public GameObject deathEffect;

    private void Start()
    {
        speed = startSpeed;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Slow(float percent)
    {
        speed = startSpeed * (1f - percent);
    }

    void Die()
    {
        GameObject effect = (GameObject) Instantiate(deathEffect, transform.position, quaternion.identity);
        Destroy(effect, 3f);
        
        PlayerStats.Money += enemyValue;
        
        Destroy(gameObject);
    }

   

    
}
