using UnityEngine;
using UnityEngine.InputSystem;

public class BulletSpawn : MonoBehaviour
{
    public GameObject BouncyBullet;
    public Transform SpawnPoint;
    public float BulletForce = 100f;

    void Update()
    {
    }


    void OnJump()
    {
        Instantiate(BouncyBullet, SpawnPoint.position, SpawnPoint.rotation);
        BouncyBullet.GetComponent<Rigidbody>().AddForce(SpawnPoint.forward * BulletForce);
        print(" jump");
    }
}