using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public float speed;
    public float secondsActive;
    public float explosionRadius;
    public float explosionKnockback;

    private float timer;

    private void Awake()
    {
        timer = secondsActive;    
    }

    private void Update()
    {
        if(timer >= 0f)
        {
            transform.Translate(transform.forward * speed * Time.deltaTime);
            timer -= Time.deltaTime;
        }
        else
        {
            Explode();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Explode();
    }

    void Explode()
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
        Destroy(this.gameObject);
    }
}
