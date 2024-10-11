using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawner : MonoBehaviour
{
    public float interval;
    public EnemyTemplate[] spawnEnemies;
    public Transform[] spawnPositions;

    private void Start()
    {
        StartCoroutine(waitSpawn());
    }

    private IEnumerator waitSpawn()
    {
        yield return new WaitForSeconds(interval);

        Transform newEnemy = MainManager.Pooling.TakeEnemy(spawnEnemies[Random.Range(0, spawnEnemies.Length)]);
        newEnemy?.GetComponent<AIThink_Base>().StartEnemy(spawnPositions[Random.Range(0, spawnPositions.Length)].position);

        StartCoroutine(waitSpawn());
    }
}
