using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bullet : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;

    public float damage;
    public float fbTime;
    private float bulletFlightTime;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }

    // Update is called once per frame
    void Update()
    {
        bulletFlightTime += Time.deltaTime;
        if (bulletFlightTime > fbTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Health objectHealth = other.GetComponent<Health>();

        if (objectHealth != null && other.CompareTag("Enemy") && !objectHealth.isDead())
        {
            objectHealth.takeDamage(damage);
        }

        if (!other.CompareTag("Player") && !other.CompareTag("Fireball"))
        {
            Destroy(gameObject);
        }
    }

}
