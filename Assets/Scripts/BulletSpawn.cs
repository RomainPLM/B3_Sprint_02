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
    public bool differentBullet;
    

    void Start()
    {
    }

    void Update()
    {
        if (differentBullet == false)
        {
            changeBack();
        }
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
        differentBullet = false;
        Invoke(nameof(ResetShoot), FireRate);
    }

    private void ResetShoot()
    {
        canShoot = true;
    }

    public void ChangeBullet(int whichBullet)
    {
        for (int i = 0; i < 1; i++)
        {
            differentBullet = true;
            WhichBullet = whichBullet;
        }
        
        
    }

    public void changeBack()
    {
        WhichBullet = 0;
    }
}