using System.Collections;
using UnityEngine;

public class SkeletonBossAI : MonoBehaviour
{
    public float detectionRadius = 5f;
    public float attackDistance = 1.5f;
    public float roamSpeed = 2f;
    public float chaseSpeed = 4f;
    public float roamDuration = 2f;
    public LayerMask playerLayer;
    public LayerMask obstacleLayer;
    public float attackCooldown = 1.5f;
    public int damage = 10;
    public float jumpForce = 5f;
    public float wallCheckDistance = 0.5f;
    public Transform wallCheckPoint;
    public LayerMask groundLayer;
    public GameObject skeletonMinionPrefab;  // Prefab for the skeleton minions
    public Transform[] spawnPoints;  // Possible points where minions can spawn
    public float spawnCooldown = 90f;  // 1.5 minutes cooldown for spawning
    public float spawnDelay = 3f;      // 3-5 seconds delay for spawning minions

    private Transform player;
    private bool isChasing = false;
    private bool isRoaming = true;
    private bool isFacingRight = true;
    private float lastAttackTime = 0f;
    private float roamTimeCounter = 0f;
    private int roamDirection = 1;
    private bool isGrounded = true;
    private Rigidbody2D rb;
    private float lastSpawnTime = 0f;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        ChooseNewDirection();
    }

    void Update()
    {
        // Check if skeleton is on the ground
        isGrounded = Physics2D.OverlapCircle(transform.position, 0.1f, groundLayer);

        player = DetectPlayer();

        if (player != null && CanSeePlayer())
        {
            ChasePlayer();
        }
        else
        {
            if (isChasing)
            {
                isChasing = false;
            }
            Roam();
        }

        if (IsWallInFront() && isGrounded)
        {
            Jump();
        }

        // Check if it's time to spawn skeleton minions
        if (Time.time >= lastSpawnTime + spawnCooldown)
        {
            StartCoroutine(SpawnSkeletons());
            lastSpawnTime = Time.time;
        }
    }

    void Roam()
    {
        if (isRoaming)
        {
            roamTimeCounter -= Time.deltaTime;

            rb.velocity = new Vector2(roamDirection * roamSpeed, rb.velocity.y);
            animator.SetBool("isWalking", true);

            if ((roamDirection > 0 && !isFacingRight) || (roamDirection < 0 && isFacingRight))
            {
                Flip();
            }

            if (roamTimeCounter <= 0)
            {
                animator.SetBool("isWalking", false);
                ChooseNewDirection();
            }
        }
    }

    void ChooseNewDirection()
    {
        roamDirection = Random.Range(0, 2) == 0 ? -1 : 1;
        roamTimeCounter = roamDuration;
    }

    void ChasePlayer()
    {
        isRoaming = false;
        isChasing = true;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > attackDistance)
        {
            MoveTowards(player.position, chaseSpeed);
        }
        else
        {
            rb.velocity = Vector2.zero;
            animator.SetTrigger("Attack");
            animator.SetBool("isWalking", false);
            StartCoroutine(AttackPlayer());
        }
    }

    void MoveTowards(Vector3 target, float speed)
    {
        Vector3 direction = (target - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);
        animator.SetBool("isWalking", true);

        if ((direction.x > 0 && !isFacingRight) || (direction.x < 0 && isFacingRight))
        {
            Flip();
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    Transform DetectPlayer()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectionRadius, playerLayer);

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                return hit.transform;
            }
        }
        return null;
    }

    bool CanSeePlayer()
    {
        Vector2 directionToPlayer = (player.position - transform.position).normalized;
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, distanceToPlayer, obstacleLayer);

        // return hit.collider == null;
        return true;
    }

    IEnumerator AttackPlayer()
    {
        yield return new WaitForSeconds(0.5f);
        if (Time.time >= lastAttackTime + attackCooldown)
        {

            if (Vector2.Distance(transform.position, player.position) <= attackDistance)
            {
                player.GetComponent<Health>().takeDamage(damage);
                lastAttackTime = Time.time;
            }
        }
    }

    bool IsWallInFront()
    {
        Vector2 direction = isFacingRight ? Vector2.right : Vector2.left;
        RaycastHit2D hit = Physics2D.Raycast(wallCheckPoint.position, direction, wallCheckDistance, obstacleLayer);

        return hit.collider != null;
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    IEnumerator SpawnSkeletons()
    {
        isRoaming = false;
        isChasing = false;

        animator.SetTrigger("Spawn");

        yield return new WaitForSeconds(Random.Range(3f, 5f));

        for (int i = 0; i < Random.Range(1, 3); i++)
        {
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);
            Instantiate(skeletonMinionPrefab, spawnPoints[spawnPointIndex].position, Quaternion.identity);
        }

        isRoaming = true;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }
}
