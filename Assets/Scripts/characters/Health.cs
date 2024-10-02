using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth;
    public bool damageNotAllowed = false;

    private Rigidbody2D rb;
    private bool _isDead = false;

    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    public bool isDead()
    {
        return _isDead;
    }

    public void takeDamage(float damage)
    {
        if (_isDead || damageNotAllowed)
            return;

        currentHealth -= damage;

        if (currentHealth > 1)
            _animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            _isDead = true;
            _animator.SetTrigger("Death");
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
