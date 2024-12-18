using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BulletSpawn : MonoBehaviour
{
    public Transform SpawnPoint;
    public GameObject[] Bullets;
    public int WhichBullet;
    public float FireRate = 0.5f;
    
    private bool canShoot = true;

    void Start()
    {
    }

    void Update()
    {
    }

    void OnJump()
    {
        if (canShoot)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        Instantiate(Bullets[WhichBullet], SpawnPoint.position, SpawnPoint.rotation);
        canShoot = false;
        Invoke(nameof(ResetShoot), FireRate);
    }

    private void ResetShoot()
    {
        canShoot = true;
    }
}