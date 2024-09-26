using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkeletonAI : MonoBehaviour
{
    public float detectionRadius = 5f;  // Radius within which the skeleton can detect the player
    public float attackDistance = 1.5f; // Distance at which the skeleton will try to attack the player
    public float roamSpeed = 2f;        // Speed while roaming
    public float chaseSpeed = 4f;       // Speed while chasing the player
    public float roamDuration = 2f;     // How long the skeleton walks in one direction
    public LayerMask playerLayer;       // Layer for detecting the player
    public LayerMask obstacleLayer;     // Layer for obstacles (e.g., walls)
    public float attackCooldown = 1.5f; // Time between attacks
    public int damage = 10;             // Amount of damage the skeleton deals

    public float jumpForce = 5f;        // Force applied for jumping
    public float wallCheckDistance = 0.5f;  // Distance to check for a wall in front
    public Transform wallCheckPoint;    // Point from where we check for obstacles (in front of the skeleton)
    public LayerMask groundLayer;       // Layer for detecting ground (for jumping)

    public bool drawDetectionRadius;
    public bool drawAttackDistance;

    protected Transform player;
    protected bool isChasing = false;
    protected bool isRoaming = true;
    protected bool isFacingRight = true;  // Determines the direction the skeleton is facing
    protected float lastAttackTime = 0f;  // Time since the last attack
    protected float roamTimeCounter = 0f; // Counter for roaming duration
    protected int roamDirection = 1;      // 1 for right, -1 for left

    private bool isGrounded = true;   // Check if the skeleton is on the ground
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ChooseNewDirection();  // Set the initial roam direction
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
                // Player out of range, return to idle (roaming) state
                isChasing = false;
            }
            Roam();
        }

        // Check if there's a wall in front and jump if needed
        if (IsWallInFront() && isGrounded)
        {
            Jump();
        }
    }

    // Roaming function for idle state
    void Roam()
    {
        if (isRoaming)
        {
            roamTimeCounter -= Time.deltaTime;

            // Move the skeleton in the current roam direction (left or right)
            rb.velocity = new Vector2(roamDirection * roamSpeed, rb.velocity.y);

            // Flip skeleton model based on movement direction
            if ((roamDirection > 0 && !isFacingRight) || (roamDirection < 0 && isFacingRight))
            {
                Flip();
            }

            // Change direction when roam duration is up
            if (roamTimeCounter <= 0)
            {
                ChooseNewDirection();
            }
        }
    }

    // Choose a new direction for roaming (left or right)
    void ChooseNewDirection()
    {
        // Randomly choose a direction: -1 for left, 1 for right
        roamDirection = Random.Range(0, 2) == 0 ? -1 : 1;
        roamTimeCounter = roamDuration; // Reset roam timer
    }

    // Chase the player when detected
    void ChasePlayer()
    {
        isRoaming = false;
        isChasing = true;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > attackDistance)
        {
            // Move towards player
            MoveTowards(player.position, chaseSpeed);
        }
        else
        {
            // Skeleton has reached attack distance, stop moving and attack
            rb.velocity = Vector2.zero;
            AttackPlayer();
        }
    }

    // Move the skeleton towards a target position
    void MoveTowards(Vector3 target, float speed)
    {
        Vector3 direction = (target - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);

        // Flip skeleton model based on movement direction
        if ((direction.x > 0 && !isFacingRight) || (direction.x < 0 && isFacingRight))
        {
            Flip();
        }
    }

    // Flip the skeleton by rotating on the y-axis
    void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    // Detect the player within the specified radius
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

    // Check if the skeleton can see the player (no obstacles in between)
    bool CanSeePlayer()
    {
        Vector2 directionToPlayer = (player.position - transform.position).normalized;
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, distanceToPlayer, obstacleLayer);

        return hit.collider == null;  // True if there are no obstacles (e.g., walls)
    }

    // Attack the player if within attack range
    void AttackPlayer()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            gameObject.GetComponent<Animator>().SetTrigger("Attack");
            // Damage the player if within attack distance
            if (Vector2.Distance(transform.position, player.position) <= attackDistance)
            {
                // Assuming the player has a script with a TakeDamage method
                player.GetComponent<Health>().takeDamage(damage);
                lastAttackTime = Time.time;
            }
        }
    }

    // Check if there's a wall in front of the skeleton
    bool IsWallInFront()
    {
        Vector2 direction = isFacingRight ? Vector2.right : Vector2.left;
        RaycastHit2D hit = Physics2D.Raycast(wallCheckPoint.position, direction, wallCheckDistance, obstacleLayer);

        return hit.collider != null; // True if a wall or obstacle is detected
    }

    // Make the skeleton jump
    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    // Draw gizmos for detection radius in the editor
    void OnDrawGizmosSelected()
    {
        // Draw the detection radius in the editor
        if (drawDetectionRadius)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, detectionRadius);
        }
        if (drawAttackDistance)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, attackDistance);
        }
    }
}