using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BulletSpawn : MonoBehaviour
{
    public Transform SpawnPoint;
    public GameObject[] Bullets;
    public int WhichBullet;

    public int maxAmmo = 5;
    private int currentAmmo;
    public float reloadTime = 2f;
    private float reloadTimer;

    private bool canShoot = true;

    void Start()
    {
        currentAmmo = maxAmmo;
        reloadTimer = 0f;
    }

    void Update()
    {
        if (currentAmmo < maxAmmo)
        {
            reloadTimer += Time.deltaTime;
            if (reloadTimer >= reloadTime)
            {
                currentAmmo++;
                reloadTimer = 0f;
            }
        }
    }

    void OnJump()
    {
        if (canShoot && currentAmmo > 0)
        {
            Instantiate(Bullets[WhichBullet], SpawnPoint.position, SpawnPoint.rotation);
            currentAmmo--;

            if (currentAmmo <= 0)
            {
                canShoot = false;
            }
        }
    }
}