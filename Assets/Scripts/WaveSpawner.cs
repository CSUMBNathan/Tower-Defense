
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
   public Transform enemyPrefab;
   public Transform spawnPoint;

   public float timeBetweenWaves = 5f;
   public float enemySpawnDelay = 0.3f;
   public TextMeshProUGUI waveCountDownText;
   
   private float countdown = 2f;
   

   private int waveIndex = 0;

   private void Update()
   {
      if (countdown <= 0)
      {
         StartCoroutine(SpawnWave());
         countdown = timeBetweenWaves;
      }

      countdown -= Time.deltaTime;
      waveCountDownText.text = Mathf.Round(countdown).ToString();
   }

   IEnumerator SpawnWave()
   {
      waveIndex++;
      
      for (int i = 0; i < waveIndex; i++)
      {
          SpawnEnemy();
          yield return new WaitForSeconds(enemySpawnDelay);
      }
   }

   void SpawnEnemy()
   {
      Instantiate(enemyPrefab, spawnPoint.position,spawnPoint.rotation);
   }
}
