using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] enemyPrefabs;
    public int maxEnemies = 25;
    private float elapsed = 0;
    private int enemies = 0;

    //private int currentTime;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        elapsed += Time.deltaTime;
        if (elapsed >= .5)
        {
            SpawnEnemy();
            elapsed = 0.0f;
        }
    }

    public void SpawnEnemy()
    {
        int randEnemy = Random.Range(0, enemyPrefabs.Length);
        int randSpawnPoint = Random.Range(0, spawnPoints.Length);
        int currentTime = (int)Time.timeSinceLevelLoad;

        if (currentTime > 5 && enemies < maxEnemies)
        {
            Instantiate(enemyPrefabs[randEnemy], spawnPoints[randSpawnPoint].position, transform.rotation);
            enemies++;
        }
    }
}
