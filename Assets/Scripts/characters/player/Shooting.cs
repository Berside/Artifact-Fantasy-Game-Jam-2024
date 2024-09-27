using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform shootingPoint;
    public GameObject bulletPrefab;

    public float ShootingCooldown;
    private float ShootingCooldownTimer = 0;
    private bool shot = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (shot)
            ShootingCooldownTimer += Time.deltaTime;

        if ( ShootingCooldownTimer >= ShootingCooldown)
        {
            ShootingCooldownTimer = 0;
            shot = false;
        }

        if (Input.GetKey(KeyCode.F) && !shot)
        {
            gameObject.GetComponentInParent<Animator>().SetTrigger("Attack");
            shot = true;
            StartCoroutine(ShootAfterDelay(0.5f));
        }
    }

    IEnumerator ShootAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);  // Wait for the specified delay (0.5s in this case)
        Instantiate(bulletPrefab, shootingPoint.position, transform.rotation);  // Instantiate the bullet
    }
}
