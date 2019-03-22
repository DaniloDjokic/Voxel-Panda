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
            Quaternion rot = Quaternion.LookRotation(transform.right, Vector3.up);

            activeBullet = Instantiate(bullet, transform.position, rot).GetComponent<BulletBehaviour>();
            if (activeBullet == null)
            {
                Debug.LogError("No bullet behaviour script on prefab");
            }
            timer = 0f;
        }
        else
        {
            timer += Time.deltaTime;
        }
    }
}
