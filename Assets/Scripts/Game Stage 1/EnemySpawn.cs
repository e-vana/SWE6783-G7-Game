using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemy;
    public Transform spawnPoint;
    public int currentTime;
    public float elapsed = 0;
    private int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        elapsed += Time.deltaTime;
        if (elapsed >= 1)
        {
            SpawnEnemy();
            elapsed = elapsed % 1;
        }
    }

    public void SpawnEnemy()
    {
        currentTime = (int)Time.timeSinceLevelLoad;
        if (currentTime > 5)
        {
            if (currentTime % 2 == 0)
            {
                if (count < 25)
                {
                    Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
                    count += 5;
                }
            }
        }
    }
}
