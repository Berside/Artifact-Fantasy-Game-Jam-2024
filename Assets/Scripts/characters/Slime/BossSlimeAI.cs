using System.Collections;
using UnityEngine;

public class BossSlimeAI : SlimeAI
{
    public GameObject slimePrefab; // Prefab of the smaller slime
    public float spawnInterval = 15f; // Time between slime spawns
    public int minSlimesToSpawn = 4; // Minimum number of slimes to spawn
    public int maxSlimesToSpawn = 8; // Maximum number of slimes to spawn
    public float spawnRadius = 2f; // Radius around the boss to spawn slimes

    private bool isSpawning = false; // Whether the boss is currently spawning
    private float nextSpawnTime = 0f; // Time to spawn the next wave of slimes

    new void Update()
    {
        base.Update();

        if (Time.time > nextSpawnTime && !isSpawning)
        {
            StartCoroutine(SpawnSlimes());
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    IEnumerator SpawnSlimes()
    {
        isSpawning = true;
        rb.velocity = Vector2.zero; // Stop the boss from moving
        yield return new WaitForSeconds(3f); // Pause for 3 seconds

        // Spawn slimes around the boss
        int numberOfSlimes = Random.Range(minSlimesToSpawn, maxSlimesToSpawn + 1);
        for (int i = 0; i < numberOfSlimes; i++)
        {
            Vector2 spawnPosition = new Vector2(Random.Range(transform.position.x - 5, transform.position.x + 5), transform.position.y);
            Instantiate(slimePrefab, spawnPosition, Quaternion.identity);
        }

        isSpawning = false;
    }
}
