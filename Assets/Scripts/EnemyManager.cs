using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemy;
    public Transform[] spawnPoints;
    public float minSpawnTime, maxSpawnTime;

    void Start()
    {
        float spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }


    void Spawn()
    {

        int spawnPointIndex = Random.Range(0, spawnPoints.Length);

        Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }
}