using UnityEngine;
using System.Collections;

public class EntitySummoner : MonoBehaviour
{
    public int maxCalls = 5;
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
        Debug.Log($"Current Calls: {currentCalls}, Can Call: {canCall}");
        CheckCallAvailability();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"OnTriggerEnter called. Other tag: {other.gameObject.tag}");
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Player entered trigger zone");
            SummonEntity();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Player pressed E inside trigger zone");
            SummonEntity();
        }
    }

    private void CheckCallAvailability()
    {
        if (!canCall && Time.time >= nextCallTime)
        {
            Debug.Log("Call availability reset");
            canCall = true;
            currentCalls = 0;
            nextCallTime = Time.time + callInterval;
            Debug.Log($"Next call time set to: {nextCallTime}");
        }
    }

    private void SummonEntity()
    {
        if (entityPrefab != null && canCall)
        {
            Vector3 spawnPosition = CalculateSpawnPosition();
            Instantiate(entityPrefab, spawnPosition, Quaternion.identity);
            IncrementCalls();
            ResetCallTimer();
            Debug.Log("Entity summoned");
        }
        else
        {
            Debug.LogError("Cannot summon entity. Entity prefab not assigned or call limit reached.");
        }
    }

    private void IncrementCalls()
    {
        currentCalls++;
        Debug.Log($"Incremented calls. Current: {currentCalls}");
        if (currentCalls >= maxCalls)
        {
            canCall = false;
            Debug.Log($"Reached call limit ({maxCalls}). Stopping calls for {callInterval} seconds.");
        }
    }

    private void ResetCallTimer()
    {
        nextCallTime = Time.time + callInterval;
        Debug.Log($"Reset call timer to: {nextCallTime}");
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
