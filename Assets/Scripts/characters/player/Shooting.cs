using System.Collections;
using System.Collections.Generic;
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
            shot = true;
            Instantiate(bulletPrefab, shootingPoint.position, transform.rotation);
        }
    }
}
