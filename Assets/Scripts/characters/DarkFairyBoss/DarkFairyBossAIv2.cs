using System.Collections;
using UnityEngine;

public class DarkFairyBossAIv2 : MonoBehaviour
{
    public float health = 100f;
    public float currentHealth;
    public float tiredDuration = 5f; // Time the boss stays on the ground
    public float idleSpeedPhase1 = 2f;
    public float idleSpeedPhase2 = 4f;
    public float attackCooldownPhase1 = 5f;
    public float attackCooldownPhase2 = 3f;
    public GameObject lightningCloudPrefabPhase1;
    public GameObject lightningCloudPrefabPhase2;
    public GameObject spiritPrefab;
    public Transform player;
    public Animator animator; // Leave space for animation triggers

    private bool isPhase2 = false;
    private bool isTired = false;
    private bool isDead = false;
    private float attackCooldownTimer;
    private int attacksCount = 0;
    private int maxAttacksBeforeTired = 3;
    private bool canAttack = true;

    private void Start()
    {
        currentHealth = health;
        attackCooldownTimer = attackCooldownPhase1;
    }

    private void Update()
    {
        if (isDead) return;

        if (currentHealth <= 0)
        {
            StartCoroutine(DeathSequence());
            return;
        }

        if (!isPhase2 && currentHealth <= health / 2)
        {
            StartCoroutine(TransformToPhase2());
        }

        if (isTired)
        {
            // Boss is tired and on the ground
            return;
        }

        if (canAttack)
        {
            if (attackCooldownTimer <= 0)
            {
                PerformAttack();
                attackCooldownTimer = isPhase2 ? attackCooldownPhase2 : attackCooldownPhase1;
            }
            else
            {
                attackCooldownTimer -= Time.deltaTime;
            }
        }
        else
        {
            IdleMovement();
        }
    }

    private void IdleMovement()
    {
        // Movement logic to keep boss flying around the arena
        float speed = isPhase2 ? idleSpeedPhase2 : idleSpeedPhase1;
        // Add idle movement logic here, keeping distance from the player
    }

    private void PerformAttack()
    {
        int attackType = Random.Range(1, 4); // Random attack between 1 and 3

        switch (attackType)
        {
            case 1:
                if (!isPhase2)
                    StartCoroutine(Phase1_Attack1());
                else
                    StartCoroutine(Phase2_Attack1());
                break;
            case 2:
                if (!isPhase2)
                    StartCoroutine(Phase1_Attack2());
                else
                    StartCoroutine(Phase2_Attack2());
                break;
            case 3:
                if (!isPhase2)
                    StartCoroutine(Phase1_Attack3());
                else
                    StartCoroutine(Phase2_Attack3());
                break;
        }

        attacksCount++;
        if (attacksCount >= maxAttacksBeforeTired)
        {
            StartCoroutine(GetTired());
        }
    }

    private IEnumerator Phase1_Attack1()
    {
        animator.SetTrigger("SummonClouds"); // Placeholder for animation trigger
        yield return new WaitForSeconds(1f); // Placeholder for animation duration

        // Summon 3-4 clouds, one above the player
        int cloudCount = Random.Range(3, 5);
        for (int i = 0; i < cloudCount; i++)
        {
            Vector3 cloudPosition = i == 0 ? player.position : RandomPositionInArena();
            Instantiate(lightningCloudPrefabPhase1, cloudPosition, Quaternion.identity);
        }

        yield return new WaitForSeconds(3f); // Time before clouds strike
        // Add code to deal damage to player here
    }

    private IEnumerator Phase1_Attack2()
    {
        animator.SetTrigger("LightningStrike"); // Placeholder for animation trigger
        yield return new WaitForSeconds(0.5f);

        // Move close to the player and attack with lightning
        Vector3 attackPosition = player.position + new Vector3(-1f, 1f, 0);
        transform.position = attackPosition;
        // Add code to deal damage to player here

        yield return new WaitForSeconds(0.5f); // Time for attack
        animator.SetTrigger("Teleport");
        // Teleport back to an idle position
        transform.position = RandomPositionInArena();
    }

    private IEnumerator Phase1_Attack3()
    {
        animator.SetTrigger("HeavyThunderbolt");
        yield return new WaitForSeconds(1f); // Placeholder for casting time

        // Cast thunderbolt to strike the player
        Vector3 strikePosition = player.position;
        // Add thunderbolt and damage logic here
    }

    private IEnumerator Phase2_Attack1()
    {
        animator.SetTrigger("SummonSpirits");
        yield return new WaitForSeconds(1f); // Placeholder for animation duration

        // Summon 2 spirits at boss' hands
        Instantiate(spiritPrefab, transform.position + new Vector3(-1, 0, 0), Quaternion.identity);
        Instantiate(spiritPrefab, transform.position + new Vector3(1, 0, 0), Quaternion.identity);
    }

    private IEnumerator Phase2_Attack2()
    {
        animator.SetTrigger("SummonClouds"); // Placeholder for animation trigger
        yield return new WaitForSeconds(1f);

        // Summon 2 clouds that rain spirits
        Instantiate(lightningCloudPrefabPhase2, RandomPositionInArena(), Quaternion.identity);
        Instantiate(lightningCloudPrefabPhase2, RandomPositionInArena(), Quaternion.identity);
    }

    private IEnumerator Phase2_Attack3()
    {
        // This attack happens after the death animation
        yield return null;
    }

    private IEnumerator GetTired()
    {
        canAttack = false;
        isTired = true;
        animator.SetTrigger("Tired");
        yield return new WaitForSeconds(tiredDuration);
        isTired = false;
        canAttack = true;
        attacksCount = 0;
    }

    private IEnumerator TransformToPhase2()
    {
        canAttack = false;
        animator.SetTrigger("Transform");
        yield return new WaitForSeconds(3f);
        isPhase2 = true;
        canAttack = true;
    }

    private IEnumerator DeathSequence()
    {
        isDead = true;
        animator.SetTrigger("Death");
        yield return new WaitForSeconds(3f);
        // Explosion and vanishing after death animation
        // Add explosion logic here
        Destroy(gameObject);
    }

    private Vector3 RandomPositionInArena()
    {
        // Define the bounds of the arena and return a random position for the boss to fly
        return new Vector3(Random.Range(-8f, 8f), Random.Range(2f, 5f), 0);
    }
}
