using System.Collections;
using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEngine;

public class SlimeAI : MonoBehaviour
{
    public float idleJumpInterval = 2f; // Time between idle jumps
    public float chaseJumpInterval = 1.5f; // Time between chase jumps
    public float jumpForce = 5f; // Force applied when jumping
    public float detectionRadius = 5f; // Radius for detecting the player
    public float moveSpeed = 2f; // Movement speed when chasing
    public LayerMask playerLayer; // Layer of the player
    public Transform groundCheck; // Ground check position

    public float attackRange = 1f; // Range within which the slime can attack
    public int damage = 10; // Damage the slime inflicts on the player
    public float attackCooldown = 2f; // Time between attacks

    private bool isIdle = true;
    private Transform player;
    private Rigidbody2D rb;
    private float nextJumpTime = 0f;
    private bool isGrounded;
    private float groundCheckRadius = 0.6f;
    private float lastAttackTime = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, LayerMask.GetMask("Ground"));

        if (Time.time > nextJumpTime && isGrounded)
        {
            if (isIdle)
            {
                JumpIdle();
            }
            else
            {
                ChasePlayer();
            }

            nextJumpTime = Time.time + (isIdle ? idleJumpInterval : chaseJumpInterval);
        }

        DetectPlayer();

        if (!isIdle && player != null)
        {
            TryAttackPlayer();
        }
    }

    void JumpIdle()
    {
        // Jump in a random direction
        rb.velocity = new Vector2(Random.Range(-1f, 1f) * moveSpeed, jumpForce);
    }

    void ChasePlayer()
    {
        if (player != null)
        {
            // Calculate direction to the player
            Vector2 direction = (player.position - transform.position).normalized;
            rb.velocity = new Vector2(direction.x * moveSpeed, jumpForce);
        }
    }

    void DetectPlayer()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, detectionRadius, playerLayer);

        if (playerCollider != null)
        {
            // Player detected, chase
            player = playerCollider.transform;
            isIdle = false;
        }
        else
        {
            // No player detected, return to idle
            player = null;
            isIdle = true;
        }
    }

    void TryAttackPlayer()
    {
        // Check if the player is within attack range
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= attackRange && Time.time >= lastAttackTime + attackCooldown)
        {
            AttackPlayer();
            lastAttackTime = Time.time; // Update the attack time to implement cooldown
        }
    }

    void AttackPlayer()
    {
        // Placeholder for damaging the player

        

        // Optional: Apply knockback to the player
        Health playerHealth = player.GetComponent<Health>();
        if (playerHealth.tag == "Player")
        {
            playerHealth.takeDamage(damage);
            Debug.Log("Slime attacks the player!");
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Draw the detection radius in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
