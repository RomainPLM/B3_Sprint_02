using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BulletSpawn : MonoBehaviour
{
    public Transform SpawnPoint;
    public GameObject[] Bullets;
    public int WhichBullet;
    

    void Update()
    {
    }


    void OnJump()
    {
        Instantiate(Bullets[WhichBullet], SpawnPoint.position, SpawnPoint.rotation);
        print(" jump");
    }
}