using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
    public float damage;
    public float startDamageTime;
    private float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (time >= startDamageTime)
        {
            Health objectHealth = other.GetComponent<Health>();

            if (objectHealth != null && other.CompareTag("Player"))
            {
                objectHealth.takeDamage(damage);
                time = 0;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (time >= startDamageTime)
        {
            Health objectHealth = other.GetComponent<Health>();

            if (objectHealth != null && other.CompareTag("Player"))
            {
                objectHealth.takeDamage(damage);
                time = 0;
            }
        }
    }
}
