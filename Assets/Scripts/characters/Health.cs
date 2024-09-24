using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void takeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            if (gameObject.tag == "Enemy")
            {
                Destroy(gameObject);
            }
            else if (gameObject.tag == "Player")
            {
                // make something on player death
                Destroy(gameObject);
            }
        }
    }

    public void heal(float restore)
    {
        currentHealth += restore;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void setCurrentHealth(float health)
    {
        currentHealth = health;
    }

    public void setMaxHealth(float health)
    {
        maxHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
