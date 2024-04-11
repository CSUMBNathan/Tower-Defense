
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
   public static int EnemiesAlive = 0;
   public Wave[] waves;
   
   public Transform spawnPoint;

   public float timeBetweenWaves = 5f;
   public float enemySpawnDelay = 0.3f;
   
   public TextMeshProUGUI waveCountDownText;
   
   private float countdown = 2f;
   private int waveIndex = 0;

   public GameManager gameManager;

   private void Update()
   {
      if (EnemiesAlive > 0)
      {
         return;
      }
      
      if (waveIndex == waves.Length)
      {
         gameManager.WinLevel();
         this.enabled = false;
      }
      
      if (countdown <= 0f)
      {
         StartCoroutine(SpawnWave());
         countdown = timeBetweenWaves;
         return;
      }

      countdown -= Time.deltaTime;
      countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
      waveCountDownText.text = string.Format("{0:00.00}",countdown);
   }

   IEnumerator SpawnWave()
   {
      PlayerStats.rounds++;

      Wave wave = waves[waveIndex];

      EnemiesAlive = wave.count;
      
      for (int i = 0; i < wave.count; i++)
      {
          SpawnEnemy(wave.enemy);
          yield return new WaitForSeconds(1f / wave.rate);
      } 
      waveIndex++;
   }

   void SpawnEnemy(GameObject enemy)
   {
      Instantiate(enemy, spawnPoint.position,spawnPoint.rotation);
   }
}
