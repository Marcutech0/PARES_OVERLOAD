using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; 
    public Transform spawnPoint; 
    private Transform player;   
    public int maxEnemies;   
    public float spawnDelay; 
    public int currentActiveEnemies = 0; 

    void Start()
    {

        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform; 
        }
        else
        {
            Debug.LogError("Player object not found! Ensure it is tagged as 'Player'.");
            return;
        }
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            if (currentActiveEnemies < maxEnemies)
            {
                SpawnEnemy();
            }

            yield return new WaitForSeconds(spawnDelay);
        }
    }

    void SpawnEnemy()
    {
        if (spawnPoint == null)
        {
            Debug.LogError("Spawn point not set!");
            return;
        }


        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

        EnemyController enemyController = enemy.GetComponent<EnemyController>();
        if (enemyController != null)
        {
            enemyController.player = player;
        }

        currentActiveEnemies++;

        if (enemyController != null)
        {
            enemyController.OnEnemyDestroyed += HandleEnemyDestroyed;
        }
    }

    void HandleEnemyDestroyed()
    {
        currentActiveEnemies--;
    }
}
