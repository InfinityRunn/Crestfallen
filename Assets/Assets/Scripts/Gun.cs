using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private float shootDelay;
    [SerializeField] private float repeatDelay;
    public GameObject bullet;
    [SerializeField] private float xBulletSpawnOffset;
    [SerializeField] private float yBulletSpawnOffset;
    private Vector3 bulletSpawn;

    void Start()
    {
        bulletSpawn = transform.position;
        bulletSpawn.x = bulletSpawn.x + xBulletSpawnOffset;
        bulletSpawn.y = bulletSpawn.y + yBulletSpawnOffset;
        InvokeRepeating("FireBullets", shootDelay, repeatDelay);
    }

    void Update()
    {
        
    }

    void FireBullets()
    {
        Instantiate(bullet, bulletSpawn, transform.rotation);
    }
}
