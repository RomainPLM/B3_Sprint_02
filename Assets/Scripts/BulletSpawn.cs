using UnityEngine;
using UnityEngine.InputSystem;

public class BulletSpawn : MonoBehaviour
{
    public GameObject BouncyBullet;
    public Transform SpawnPoint;

    void Update()
    {
    }


    void OnJump()
    {
        Instantiate(BouncyBullet, SpawnPoint.position, SpawnPoint.rotation);
        print(" jump");
    }
}