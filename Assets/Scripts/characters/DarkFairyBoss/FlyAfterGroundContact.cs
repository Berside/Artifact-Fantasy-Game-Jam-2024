using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyAfterGroundContact : MonoBehaviour
{
    public float flySpeed = 2f;    // Speed at which the object flies to the target
    public float damage;
    public float damageCooldown;
    public float attackDistance;

    private float time = 0;
    private Transform target;       // The target to fly towards
    private Rigidbody2D _rb;       // The target to fly towards
    private bool hasContactedGround = false;  // Whether the object has contacted the ground

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the object collided with the ground layer
        if (collision.gameObject.layer == 6)
        {
            hasContactedGround = true;
            _rb.isKinematic = true;
        }
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
        // If the object has contacted the ground, start moving it towards the target
        if (hasContactedGround)
        {
            FlyTowardsTarget();
        }
        AttackPlayer();

        time += Time.deltaTime;
    }

    private void FlyTowardsTarget()
    {
        // Move towards the target at a constant speed
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * flySpeed * Time.deltaTime;
    }
    public void AttackPlayer()
    {
            // Damage the player if within attack distance
            if (Vector2.Distance(transform.position, target.position) <= attackDistance && time >= damageCooldown)
            {
                target.GetComponent<Health>().takeDamage(damage);
                time = 0;
            
            }
    }
}
