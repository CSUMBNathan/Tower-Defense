using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform target;
    private string enemyTag = "Enemy";
    private Enemy targetEnemy;
    
    [Header("General")]
    public float range = 15f;
    public float turnSpeed = 10f;

    
    [Header("Use Bullets")]
    public float fireRate = 1.0f;
    private float fireCountdown = 0f;
    
    [Header("Optional")]
    public GameObject bulletPrefab;
    
    [Header("Use Lasers")] 
    public bool useLaser;
    public int damageOverTime = 30;
    public float slowPct = 0.5f;
    
    public LineRenderer lineRenderer;
    public ParticleSystem laserImpactEffect;
    public Light impactLight;

    
    [Header("Unity SetupFields")]
    public Transform partToRotate;
    public Transform firePoint;
    
    
    
    void Start()
    {
        InvokeRepeating("UpdateTarget",0f,0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            if (useLaser)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    laserImpactEffect.Stop();
                    impactLight.enabled = false;
                }            }
            return;
        }
            ;

        LockOnTarget();

        if (useLaser)
        {
            Laser();
        }

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
        
    }

    void LockOnTarget()
    {
        Vector3 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation,lookRotation,Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void Laser()
    {
        targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);
        targetEnemy.Slow(slowPct);
        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            laserImpactEffect.Play();
            impactLight.enabled = true;
        }        
        lineRenderer.SetPosition(0,firePoint.position);
        lineRenderer.SetPosition(1,target.position);

        Vector3 dir = firePoint.position - target.position;
        
        laserImpactEffect.transform.position = target.position + dir.normalized;

        
        laserImpactEffect.transform.rotation = Quaternion.LookRotation(dir);


    }

    // ReSharper disable Unity.PerformanceAnalysis
    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }

            if (nearestEnemy != null && shortestDistance <= range)
            {
                target = nearestEnemy.transform;
                targetEnemy = nearestEnemy.GetComponent<Enemy>();
            }
            else
            {
                target = null;
            }
        }
        
        
    }

    void Shoot()
    {
       GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
       Bullet bullet = bulletGO.GetComponent<Bullet>();
       if(bullet != null)
           bullet.Seek(target);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position,range);
    }
}
