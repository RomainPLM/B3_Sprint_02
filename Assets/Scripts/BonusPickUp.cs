using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class BonusPickUp : MonoBehaviour
{
    
    
    public void OnCollisionEnter(Collision other)
    {
        print("Collision detected");

        if (other.gameObject.layer == 9)
        {
            BulletSpawn bulletspawn = other.gameObject.GetComponent<BulletSpawn>();
            
            if (bulletspawn != null)
            {
                bulletspawn.ChangeBullet(Random.Range(1, 5));
                Destroy(gameObject);
            }
        }
    }
}