using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelPanda.ProcGen.Elements;

public class BulletBehaviour : MonoBehaviour
{
    public float speed;
    public float secondsActive;
    public float explosionRadius;
    public float explosionKnockback;
    private const string explosionSFX = "Play_CanonExplosion";

    private float timer;

    private void Awake()
    {
        timer = secondsActive;    
    }

	private void Update()
    {
        if(timer >= 0f)
        {
            var x = speed * Time.deltaTime;
            transform.position += transform.forward * x;
            timer -= Time.deltaTime;
        }
        else
        {
            Despawn();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
		if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Obstacle")) 
			Collide();
    }

    void Collide()
    {
        Collider[] explosion = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach(Collider col in explosion)
        {
            if(col.gameObject.CompareTag("Player"))
            {
                Rigidbody player = col.gameObject.GetComponent<Rigidbody>();

                if(player != null)
                {
                    Vector3 forceVector = player.gameObject.transform.position - transform.position;
                    player.AddForce(forceVector.normalized * explosionKnockback * Time.deltaTime, ForceMode.VelocityChange);
                }
                else
                {
                    Debug.LogError("Player caught in explosion does not have a rigidbody");
                }
            }
        }
		Explode();

	}
    void Explode()
    {
        AkSoundEngine.PostEvent(explosionSFX, gameObject);
        Despawn();
    }

	public void Spawn(Vector3 position, Quaternion rotation)
	{
		this.transform.position = position;
		this.transform.rotation = rotation;
		this.gameObject.SetActive(true);
		timer = secondsActive;
	}

	public void Despawn()
	{
		this.gameObject.SetActive(false);
	}

	public bool AvailableToSpawn()
	{
		return !gameObject.activeInHierarchy;
	}
}
