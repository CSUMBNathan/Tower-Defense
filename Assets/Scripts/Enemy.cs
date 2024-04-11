
using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float startSpeed = 10f;
    public float startHealth = 100f;
    private float health;
    
    [HideInInspector]
    public float speed;
    
    public int enemyValue = 50;

    public GameObject deathEffect;

    [Header("Unity Stuff")] public Image healthBar;

    private bool isDead = false;
    
    
    private void Start()
    {
        speed = startSpeed;
        health = startHealth;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        healthBar.fillAmount = health/startHealth;
        
        if (health <= 0 && ! isDead)
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
        isDead = true;
        GameObject effect = (GameObject) Instantiate(deathEffect, transform.position, quaternion.identity);
        Destroy(effect, 3f);
        
        PlayerStats.Money += enemyValue;

        WaveSpawner.EnemiesAlive--;
        
        Destroy(gameObject);
    }

   

    
}
