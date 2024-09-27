using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform shootingPoint;
    public GameObject bulletPrefab;
    public int level; 

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
            switch (level)
            {
                case 1:
                    StartCoroutine(ShootAfterDelay(0.5f));
                    break;
                case 2:
                    StartCoroutine(ShootSpread(0.7f));
                    break;
                case 3:
                    StartCoroutine(ShootLeftAndRight(0.5f));
                    break;
            }
        }
    }

    IEnumerator ShootAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);  // Wait for the specified delay (0.5s in this case)
        Instantiate(bulletPrefab, shootingPoint.position, transform.rotation);  // Instantiate the bullet
    }
    IEnumerator ShootSpread(float delay)
    {
        yield return new WaitForSeconds(delay); // Ожидаем указанное время
        Instantiate(bulletPrefab, shootingPoint.position, transform.rotation);
        yield return new WaitForSeconds(0.07f);
        Instantiate(bulletPrefab, shootingPoint.position, transform.rotation);
        yield return new WaitForSeconds(0.07f);
        Instantiate(bulletPrefab, shootingPoint.position, transform.rotation);
    }

    IEnumerator ShootLeftAndRight(float delay)
    {
        yield return new WaitForSeconds(delay);
        Instantiate(bulletPrefab, shootingPoint.position, transform.rotation);
        yield return new WaitForSeconds(0.06f);
        Instantiate(bulletPrefab, shootingPoint.position, transform.rotation);
        yield return new WaitForSeconds(0.06f);
        Instantiate(bulletPrefab, shootingPoint.position, transform.rotation);
        yield return new WaitForSeconds(0.06f);
        Instantiate(bulletPrefab, shootingPoint.position, transform.rotation);
        yield return new WaitForSeconds(0.06f);
        Instantiate(bulletPrefab, shootingPoint.position, transform.rotation);
    }



}
