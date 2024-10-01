using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class EntitySummoner : MonoBehaviour
{
    public int maxCalls;
    public float callInterval = 10f;
    public GameObject entityPrefab;
    public Transform spawnPoint;
    public float minDistance = 1f;
    public float maxDistance = 10f;
    public Vector3 spawnDirection;
    public Text summonText;
    private int currentCalls = 0;
    private bool canCall = true;
    private float time = 0;

    private void Update()
    {
        time += Time.deltaTime;

        if(!canCall && time >= callInterval)
        {
            canCall = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ShowSummonText();
        }
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            SummonEntity();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ShowSummonText();
        }
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            SummonEntity();
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HideSummonText();
        }
    }

    private void SummonEntity()
    {
        if (entityPrefab != null && canCall && currentCalls < maxCalls)
        {
            Vector3 spawnPosition = CalculateSpawnPosition();
            Instantiate(entityPrefab, spawnPosition, Quaternion.identity);

            currentCalls++;
            canCall = false;
            time = 0;
        }
        else
        {
            Debug.LogError("Cannot summon entity. Entity prefab not assigned or call limit reached.");
        }
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
    private void ShowSummonText()
    {
        if (summonText != null && !summonText.gameObject.activeSelf)
        {
            summonText.gameObject.SetActive(true);
        }
    }


    private void HideSummonText()
    {
        if (summonText != null && summonText.gameObject.activeSelf)
        {
            summonText.gameObject.SetActive(false);
        }
    }

}
