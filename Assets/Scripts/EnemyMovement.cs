using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    private Transform target;
    private int wavePointIndex = 0;
    private Enemy enemy;

    
    void Start()
    {
        enemy = GetComponent <Enemy>();
        target = Waypoints.waypoints[0];
    }
    
    private void Update()
    {
        Vector3 direction = target.position - transform.position;
        transform.Translate(direction.normalized * (enemy.speed * Time.deltaTime),Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.3f)
        {
            GetNextWaypoint();
        }

        enemy.speed = enemy.startSpeed;
    }
    
    void GetNextWaypoint()
    {
        if (wavePointIndex >= Waypoints.waypoints.Length - 1)
        {
            EndPath();
        }
        wavePointIndex++;
        target = Waypoints.waypoints[wavePointIndex];
    }
    
    void EndPath()
    {
        PlayerStats.Lives--;
        WaveSpawner.EnemiesAlive--;
        Destroy(gameObject);
    }
}
