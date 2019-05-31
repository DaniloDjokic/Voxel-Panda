using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBehaviour : LocalUpdater<Rigidbody>
{
    public float speed;
    public float decorationSpeed = 0.000006f;
    private void FixedUpdate()
    {
        AddForceToRBs();
    }

    void AddForceToRBs()
    {
        foreach(Rigidbody rb in currentlyContained)
        {
            if (rb.CompareTag("Player"))
            {
                rb.AddForce(transform.right * speed * Time.deltaTime, ForceMode.Impulse);
            } else
            {
                rb.AddForce(transform.right * decorationSpeed * Time.deltaTime, ForceMode.Impulse);

            }
        }
    }

    protected override void PlayerEnters(GameObject player)
    {
        base.PlayerEnters(player);
    }

    protected override void PlayerExits(GameObject player)
    {
        base.PlayerExits(player);
    }
}
