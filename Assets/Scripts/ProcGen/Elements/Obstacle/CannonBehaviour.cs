using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelPanda.ProcGen.Elements;

public class CannonBehaviour : ObsBehaviour
{
    public GameObject bullet;
    public float secondsBetweenShots;
    private float timer = 0f;

    BulletBehaviour activeBullet;
    private void Update()
    {
        Fire();
    }

    void Fire()
    {
        if(timer > secondsBetweenShots)
        {
            activeBullet = Instantiate(bullet, transform.position, transform.rotation).GetComponent<BulletBehaviour>();
            Physics.IgnoreCollision(GetComponent<Collider>(), activeBullet.gameObject.GetComponent<Collider>());
            timer = 0f;
        }
        else
        {
            timer += Time.deltaTime;
        }
    }
}
