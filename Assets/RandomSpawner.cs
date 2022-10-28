using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] enemyPrefabs;
    public int maxEnemies = 500;
    private float elapsed = 0;
    private int enemies = 0;
    public stageManager stageManagement;
    public int nextStage;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        elapsed += Time.deltaTime;
        if (elapsed >= .2)
        {
            SpawnEnemy();
            elapsed = 0.0f;
        }
    }
    public bool IsSpawnPointVisibleOnScreen(Transform spawnPoint)
    {
        Camera mainCamera = FindObjectOfType<Camera>();
        Vector3 screenPoint = mainCamera.WorldToViewportPoint(spawnPoint.position);
        bool onScreen = screenPoint.x > 0 && screenPoint.y > 0 && screenPoint.x < 1 && screenPoint.y < 1;
        return onScreen;
    }
    public void SpawnEnemy()
    {
        int randEnemy = Random.Range(0, 100);
        int randSpawnPoint = Random.Range(0, spawnPoints.Length);
        int currentTime = (int)Time.timeSinceLevelLoad;


        if (currentTime > 5 && enemies < maxEnemies)
        {
            int enemy;
            if (randEnemy <= 70)
            {
                enemy = 0;
            } else if (randEnemy <= 95)
            {
                enemy = 1;
            } else
            {
                enemy = 2;
            }
            if (!IsSpawnPointVisibleOnScreen(spawnPoints[randSpawnPoint])){
                Instantiate(enemyPrefabs[enemy], spawnPoints[randSpawnPoint].position, transform.rotation);
                enemies++;
            }

        }
        if (maxEnemies == enemies)
        {
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                stageManagement = FindObjectOfType<stageManager>();
                stageManagement.changeStage(nextStage);
                stageManagement.updateStage(nextStage);
            }

        }
    }
}
