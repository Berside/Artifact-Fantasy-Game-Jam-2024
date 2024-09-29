using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth;

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
        currentHealth -= damage;

        _animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            _isDead = true;
            float delay = GetAnimationClipLength("Death");
            print(delay);
            if (gameObject.tag == "Enemy")
            {
                if (gameObject.layer == 10)
                {
                    _animator.SetTrigger("Death");
                    StartCoroutine(SceneM(delay));

                }
                else
                {
                    _animator.SetTrigger("Death");
                    StartCoroutine(destroyGameObject(delay));
                }
            }
            else if (gameObject.tag == "Player")
            {
                _animator.SetTrigger("Death");
                StartCoroutine(playerDeath(delay));
                
            }
        }
    }
    IEnumerator playerDeath(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    IEnumerator SceneM(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("MainMenu");
    }
    IEnumerator destroyGameObject(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
    public float GetAnimationClipLength(string clipName)
    {
        // Loop through all animation clips in the Animator
        foreach (AnimationClip clip in _animator.runtimeAnimatorController.animationClips)
        {
            // Check if the clip name matches the provided name
            if (clip.name == clipName)
            {
                return clip.length;  // Return the length in seconds
            }
        }

        // Return 0 if the clip was not found
        Debug.LogWarning("Animation clip not found: " + clipName);
        return 0f;
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
