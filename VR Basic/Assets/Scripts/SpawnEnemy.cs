using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefabs;
    [SerializeField] private int numberOfEnemies;
    [SerializeField] private Vector2 spawnPosX;
    [SerializeField] private Vector2 spawnPosZ;


    [SerializeField] private float minSpawnInterval;
    [SerializeField] private float maxSpawnInterval;
    private void Start() {
        StartCoroutine(SpawnEnemyCharacterRoutine());
    }

    IEnumerator SpawnEnemyCharacterRoutine() {
        for (int i = 0; i<numberOfEnemies;i ++) {
            Vector3 randomPosition =  new Vector3(
                Random.Range(spawnPosX.x, spawnPosX.y),
                0,
                Random.Range(spawnPosZ.x,spawnPosZ.y));

            Instantiate(enemyPrefabs,randomPosition,Quaternion.identity);

            float randomIntervalTime = Random.Range(minSpawnInterval,maxSpawnInterval); 
            yield return new WaitForSeconds(randomIntervalTime);
        }
    }
}
