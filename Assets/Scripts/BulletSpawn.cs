using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BulletSpawn : MonoBehaviour
{
    public Transform SpawnPoint;
    public GameObject[] Bullets;
    public int WhichBullet;
    public float FireRate = 0.5f;
    public Collider CanonCollider;

    public bool canShoot = true;
    public bool differentBullet;
    public bool inWall = false;

    public bool _haveBonus = false;
    public int _bulletBonus;

    public GameObject _muzzleFlash;

    public AudioClip[] audios;


    void Start()
    {
    }

    void Update()
    {
        if (differentBullet == false)
        {
            ChangeBack();
        }
    }

    void OnJump()
    {
        if (canShoot && !inWall)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        SfxManager._instance.PlayAudioClip(audios, SpawnPoint.transform, false, 1f);
        Instantiate(_muzzleFlash, SpawnPoint.position, Quaternion.identity);
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

            //WhichBullet = whichBullet;
            _bulletBonus = whichBullet;
        }
    }

    public void ChangeBack()
    {
        WhichBullet = 0;
    }

    private void OnTriggerStay(Collider other)
    {
        if (CanonCollider != null && CanonCollider.bounds.Intersects(other.bounds))
        {
            if (other.gameObject.layer == 12)
            {
                inWall = true;
            }
            else
            {
                inWall = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (CanonCollider != null && other.gameObject.layer == 12)
        {
            inWall = false;
        }
    }
    private void OnUseBonus()
    {
        WhichBullet = _bulletBonus;
        differentBullet = true;
    }
}