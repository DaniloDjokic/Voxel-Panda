using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelPanda.ProcGen.Elements;

public class CannonBehaviour : ObsBehaviour
{
    public BulletBehaviour bulletModel;
	public int poolSize = 4;
    public float secondsBetweenShots;
    public float phaseStart = 0f;
    private float timer = 0f;
    private const string launchBulletSFX = "Play_CanonLaunch";

	BulletPooler bulletPooler;

	private void Awake()
	{
		bulletPooler = new BulletPooler(bulletModel, poolSize);
	}

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

            var activeBullet = bulletPooler.GetBullet();

			if (activeBullet == null)
            {
                Debug.LogError("No bullet behaviour script on prefab");
            }
			activeBullet.Spawn(this.transform.position, rot);
			timer = 0f;
        }
        else
        {
            timer += Time.deltaTime;
        }
    }
}
