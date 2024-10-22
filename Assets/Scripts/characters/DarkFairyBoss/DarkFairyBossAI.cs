using UnityEngine;

public class DarkFairyBossAI : MonoBehaviour
{
    public float currentHealth;
    public float tiredDuration = 5f;
    public float attackCooldown = 2f;
    public float phase2SpeedMultiplier = 1.5f;

    public GameObject lightningCloudPrefab; // Assign cloud prefab for lightning attack
    public GameObject spiritCloudPrefab;
    public GameObject spiritPrefab; // Assign spirit prefab for Phase 2 attacks
    public Transform arenaBounds; // Reference for arena bounds to control flying area

    private bool isTired = false;
    private bool isPhase2 = false;
    private bool isDead = false;
    private float attackTimer = 0f;
    private float tiredTimer = 0f;
    private Animator animator;
    private Transform player;
    private Health health;

    private int phase1AttackCount = 0;
    private int phase2AttackCount = 0;
    private int maxAttacksBeforeTired = 3;

    private Vector3 flyTarget;

    private enum BossState { Idle, Attacking, Tired, Dead }
    private BossState currentState = BossState.Idle;

    private void Start()
    {
        health = GetComponent<Health>();
        currentHealth = health.currentHealth;
        player = GameObject.FindWithTag("Player").transform;
        animator = GetComponent<Animator>();
        SetNewFlyTarget(); // Set initial flying target
    }

    private void Update()
    {
        if (isDead) return;

        if (!isPhase2 && currentHealth <= health.maxHealth * 0.5f)
        {
            StartPhase2();
        }

        switch (currentState)
        {
            case BossState.Idle:
                HandleIdle();
                break;
            case BossState.Attacking:
                HandleAttack();
                break;
            case BossState.Tired:
                HandleTired();
                break;
        }
    }

    private void HandleIdle()
    {
        FlyAround();

        attackTimer += Time.deltaTime;
        if (attackTimer >= attackCooldown)
        {
            attackTimer = 0f;
            ChooseAttack();
        }
    }

    private void HandleAttack()
    {
        if (isPhase2)
        {
            PerformPhase2Attack();
        }
        else
        {
            PerformPhase1Attack();
        }

        if ((!isPhase2 && phase1AttackCount >= maxAttacksBeforeTired) ||
            (isPhase2 && phase2AttackCount >= maxAttacksBeforeTired))
        {
            EnterTiredState();
        }
    }

    private void HandleTired()
    {
        tiredTimer += Time.deltaTime;
        if (tiredTimer >= tiredDuration)
        {
            tiredTimer = 0f;
            isTired = false;
            currentState = BossState.Idle;
        }
    }

    private void ChooseAttack()
    {
        currentState = BossState.Attacking;
    }

    private void PerformPhase1Attack()
    {
        int attackType = Random.Range(1, 4);
        switch (attackType)
        {
            case 1:
                Attack1_Phase1();
                break;
            case 2:
                Attack2_Phase1();
                break;
            case 3:
                Attack3_Phase1();
                break;
        }

        phase1AttackCount++;
        currentState = BossState.Idle;
    }

    private void PerformPhase2Attack()
    {
        int attackType = Random.Range(1, 3);
        switch (attackType)
        {
            case 1:
                Attack1_Phase2();
                break;
            case 2:
                Attack2_Phase2();
                break;
        }

        phase2AttackCount++;
        currentState = BossState.Idle;
    }

    private void Attack1_Phase1()
    {
        // Summon clouds for lightning
        //animator.SetTrigger("Attack1");

        Vector3 playerPosition = player.position;

        for (int i = 0; i < Random.Range(3, 5); i++)
        {
            Vector3 cloudPosition = new Vector3(
                Random.Range(arenaBounds.position.x - 5f, -12), // Random position within the arena
                Random.Range(arenaBounds.position.y - 1f, -12), // Closer to the ground
                0);
            
            // Ensure at least one cloud is directly above the player
            if (i == 0)
            {
                cloudPosition = new Vector3(playerPosition.x, Random.Range(arenaBounds.position.y - 1f, arenaBounds.position.y + 1f), 0);
            }

            Instantiate(lightningCloudPrefab, cloudPosition, Quaternion.identity);

            // Here you can code the cloud to strike after 3-4 seconds and damage the player if hit
        }
    }

    private void Attack2_Phase1()
    {
        animator.SetTrigger("phase1_Attack2");

        // Move boss to the left and slightly above the player
        Vector3 strikePosition = new Vector3(player.position.x - 2f, player.position.y + 2f, 0);
        transform.position = Vector3.Lerp(transform.position, strikePosition, Time.deltaTime * 10f);

        // After a moment, strike with lightning and teleport away
        // You can add damage to the player here
        TeleportAboveArena();
    }

    private void Attack3_Phase1()
    {
        animator.SetTrigger("phase1_Attack3");

        // Boss casts a heavy thunderbolt on the player
        // You can instantiate a thunderbolt prefab here and deal damage
    }

    private void Attack1_Phase2()
    {
        animator.SetTrigger("phase2_Attack1");

        // Summon two spirits that fly towards the player
        for (int i = 0; i < 2; i++)
        {
            Vector3 spiritPosition = new Vector3(transform.position.x, transform.position.y, 0);
            GameObject spirit = Instantiate(spiritPrefab, spiritPosition, Quaternion.identity);

            // You will need to write code in the spirit prefab to move toward the player's position
        }
    }

    private void Attack2_Phase2()
    {
        animator.SetTrigger("phase2_Attack2");

        // Summon two clouds that spawn spirits like rain
        for (int i = 0; i < 2; i++)
        {
            Vector3 cloudPosition = new Vector3(
                Random.Range(arenaBounds.position.x - 5f, arenaBounds.position.x + 5f),
                Random.Range(arenaBounds.position.y + 3f, arenaBounds.position.y + 5f),
                0);

            Instantiate(spiritCloudPrefab, cloudPosition, Quaternion.identity);

            // The cloud prefab should be able to spawn spirits over time
        }
    }

    private void StartPhase2()
    {
        isPhase2 = true;
        animator.SetTrigger("Phase2Transform");
        // Increase movement speed in Phase 2
    }

    private void EnterTiredState()
    {
        isTired = true;
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, -12);
        //animator.SetTrigger("Tired");
        currentState = BossState.Tired;
        phase1AttackCount = 0;
        phase2AttackCount = 0;
    }

    private void Die()
    {
        isDead = true;
        animator.SetTrigger("Death");

        // After the death animation, trigger explosion
        //Invoke("FinalExplosion", 3f); // 3 seconds delay before explosion
    }

    private void FinalExplosion()
    {
        // Explosion logic here
        Destroy(gameObject); // Remove the boss after explosion
    }

    private void FlyAround()
    {
        // Move towards the flying target
        transform.position = Vector3.MoveTowards(transform.position, flyTarget, Time.deltaTime * 5f * (isPhase2 ? phase2SpeedMultiplier : 1f));

        // Set new target if the boss has reached the current one
        if (Vector3.Distance(transform.position, flyTarget) < 0.1f)
        {
            SetNewFlyTarget();
        }
    }

    private void SetNewFlyTarget()
    {
        // Randomly set a new flying target within the arena
        flyTarget = new Vector3(
            Random.Range(arenaBounds.position.x - 5f, arenaBounds.position.x + 5f),
            Random.Range(arenaBounds.position.y + 3f, arenaBounds.position.y + 5f),
            0);
    }

    private void TeleportAboveArena()
    {
        // Teleport the boss to a random position above the arena
        animator.SetTrigger("Teleportation");
        Vector3 teleportPosition = new Vector3(
            Random.Range(arenaBounds.position.x - 5f, arenaBounds.position.x + 5f),
            arenaBounds.position.y + 5f,
            0);
        transform.position = teleportPosition;
    }
}
