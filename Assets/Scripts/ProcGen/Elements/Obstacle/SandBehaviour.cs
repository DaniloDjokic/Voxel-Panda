using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandBehaviour : LocalUpdater<Rigidbody>
{
    public float slowdownMultiplier;

    private void FixedUpdate()
    {
        AddForceToRBs();
    }

    private void AddForceToRBs()
    {
        foreach (Rigidbody rb in currentlyContained)
        {
            rb.AddForce(rb.velocity * -1 * slowdownMultiplier);
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
