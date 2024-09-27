using UnityEngine;
using System.Collections;

public class EntitySummoner : MonoBehaviour
{
    public int maxCalls;
    public float callInterval = 10f;
    public GameObject entityPrefab;
    public Transform spawnPoint;
    public float minDistance = 1f;
    public float maxDistance = 10f;
    public Vector3 spawnDirection;

    private int currentCalls = 0;
    private bool canCall = true;
    private float nextCallTime;

    private void Update()
    {
        CheckCallAvailability();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            SummonEntity();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            SummonEntity();
        }
    }

    private void CheckCallAvailability()
    {
        if (!canCall && Time.time >= nextCallTime)
        {
            canCall = true;
            //currentCalls = 0;
            nextCallTime = Time.time + callInterval;
        }
    }

    private void SummonEntity()
    {
        if (entityPrefab != null && canCall && currentCalls < maxCalls)
        {
            Vector3 spawnPosition = CalculateSpawnPosition();
            Instantiate(entityPrefab, spawnPosition, Quaternion.identity);
            IncrementCalls();
            ResetCallTimer();
        }
        else
        {
            Debug.LogError("Cannot summon entity. Entity prefab not assigned or call limit reached.");
        }
    }

    private void IncrementCalls()
    {
        currentCalls++;
        if (currentCalls >= maxCalls)
        {
            canCall = false;
        }
    }

    private void ResetCallTimer()
    {
        nextCallTime = Time.time + callInterval;
    }

    private Vector3 CalculateSpawnPosition()
    {
        Vector3 randomOffset = new Vector3(
            Random.Range(-spawnDirection.x, spawnDirection.x),
            Random.Range(-spawnDirection.y, spawnDirection.y),
            Random.Range(-spawnDirection.z, spawnDirection.z)
        );

        Vector3 offset = transform.TransformDirection(randomOffset);

        float distance = Random.Range(minDistance, maxDistance);
        Vector3 direction = (spawnPoint.position - transform.position).normalized;

        return transform.position + (direction * distance) + offset;
    }
}
