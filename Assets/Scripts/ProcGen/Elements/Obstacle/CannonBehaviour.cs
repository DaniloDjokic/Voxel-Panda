using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelPanda.ProcGen.Elements;

public class CannonBehaviour : ObsBehaviour
{
    public GameObject bullet;
    public float secondsBetweenShots;
    public float phaseStart = 0f;
    private float timer = 0f;
    private const string launchBulletSFX = "Play_CanonLaunch";

    BulletBehaviour activeBullet;

    private void Start() {
    	timer = phaseStart;
    }

    private void Update()
    {
        Fire();
    }

    void Fire()
    {
        if(timer > secondsBetweenShots)
        {
            AkSoundEngine.PostEvent(launchBulletSFX, gameObject);
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
