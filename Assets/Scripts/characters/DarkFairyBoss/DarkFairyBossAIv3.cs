using UnityEngine;
using UnityEngine.UIElements;

public class DarkFairyBossAIv3 : MonoBehaviour
{
    public float tiredDuration = 5f;
    public float attackCooldown = 2f;
    public float phase2SpeedMultiplier = 1.5f;
    public int maxAttacksBeforeTired = 5;
    public float damageCooldown;
    public float damage;

    public GameObject lightningCloudPrefab; // Assign cloud prefab for lightning attack
    public GameObject spiritCloudPrefab;
    public GameObject Thunder;
    public GameObject spiritPrefab; // Assign spirit prefab for Phase 2 attacks
    public Transform arenaCenter; // Reference for arena bounds to control flying area
    public Transform arenaLeftCorner;

    private float time = 0;
    private bool isTired = false;
    private bool isPhase2 = false;
    private bool isDead = false;
    private float attackTimer = 0f;
    private float tiredTimer = 0f;
    private Animator animator;
    private Transform player;
    private Health health;
    private Rigidbody2D _rb;

    private int phase1AttackCount = 0;
    private int phase2AttackCount = 0;
    private float halfWidth;
    private float halfHeight;

    private Vector3 flyTarget;

    private enum BossState { Idle, Attacking, Tired, Dead }
    private BossState currentState = BossState.Idle;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.isKinematic = true;
        halfWidth = Mathf.Abs(arenaCenter.position.x - arenaLeftCorner.position.x);
        halfHeight = Mathf.Abs(arenaCenter.position.y - arenaLeftCorner.position.y);

        health = GetComponent<Health>();
        player = GameObject.FindWithTag("Player").transform;
        animator = GetComponent<Animator>();
        SetNewFlyTarget(); // Set initial flying target
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Health objectHealth = other.GetComponent<Health>();

            if (objectHealth != null && other.CompareTag("Player") && time > damageCooldown)
            {
                objectHealth.takeDamage(damage);
                time = 0;
            }
        }
    }

    private void Update()
    {
        time += Time.deltaTime;

        if (isDead) return;

        if (!isPhase2 && health.currentHealth <= health.maxHealth * 0.5f)
        {
            StartPhase2();
            animator.SetFloat("Phase2", 1f);
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
            _rb.isKinematic = true;
            //TeleportAboveArena();
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
                Random.Range(arenaCenter.position.x - halfWidth, arenaCenter.position.x + halfWidth), // Random position within the arena
                arenaCenter.position.y - 5 * halfHeight/6,
                0);

            // Ensure at least one cloud is directly above the player
            if (i == 0)
            {
                cloudPosition = new Vector3(playerPosition.x, arenaCenter.position.y - 5 * halfHeight / 6, 0);
            }

            Instantiate(lightningCloudPrefab, cloudPosition, Quaternion.identity);

            // Here you can code the cloud to strike after 3-4 seconds and damage the player if hit
        }
    }

    private void Attack2_Phase1()
    {

        // Move boss to the left and slightly above the player
        Vector3 strikePosition = new Vector3(player.position.x + 2f, player.position.y + 2f, 0);
        //transform.position = Vector3.MoveTowards(transform.position, strikePosition, Time.deltaTime * 5f);
        transform.position = strikePosition;

        animator.SetTrigger("phase1_Attack2");


        // After a moment, strike with lightning and teleport away
        //Invoke("attack2Phase1", 1f);
        // You can add damage to the player here
        //TeleportAboveArena();
        //Invoke("TeleportAboveArena", 3f);
    }

    private void Attack3_Phase1()
    {
        animator.SetTrigger("phase1_Attack3");

        Vector3 cloudPosition = new Vector3(player.position.x, arenaCenter.position.y - 3 * halfHeight / 4, 0);
        Instantiate(Thunder, cloudPosition, Quaternion.identity);

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
        for (int i = 0; i < 4; i++)
        {
            Vector3 cloudPosition = new Vector3(
                Random.Range(arenaCenter.position.x - halfWidth, arenaCenter.position.x + halfWidth), // Random position within the arena
                arenaCenter.position.y - 5 * halfHeight / 6,
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
        _rb.isKinematic = false;
        //gameObject.transform.position = new Vector3(gameObject.transform.position.x, -12);
        //animator.SetTrigger("Tired");
        currentState = BossState.Tired;
        phase1AttackCount = 0;
        phase2AttackCount = 0;
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
            Random.Range(arenaCenter.position.x - halfWidth, arenaCenter.position.x + halfWidth),
            Random.Range(arenaCenter.position.y, arenaCenter.position.y + halfHeight),
            0);
    }

    private void TeleportAboveArena()
    {
        // Teleport the boss to a random position above the arena
        animator.SetTrigger("Teleportation");
        Vector3 teleportPosition = new Vector3(
            Random.Range(arenaCenter.position.x - halfWidth, arenaCenter.position.x + halfWidth),
            Random.Range(arenaCenter.position.y, arenaCenter.position.y + halfHeight),
            0);
        transform.position = teleportPosition;
    }
}
