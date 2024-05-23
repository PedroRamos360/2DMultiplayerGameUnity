using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public GameObject enemyPrefab;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    private TimeoutManager timeoutManager;
    public float spawnEnemyInterval = 3.0f;
    private float timer = 0.0f;

    void Start()
    {
        timeoutManager = gameObject.AddComponent<TimeoutManager>();
    }


    void Update()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }
        timer += Time.deltaTime;
        if (timer >= spawnEnemyInterval)
        {
            SpawnEnemy();
            timer = 0.0f;
        }
    }

    private void SpawnEnemy()
    {
        Vector2 randomPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        PhotonNetwork.Instantiate(enemyPrefab.name, randomPosition, Quaternion.identity);
    }
}
