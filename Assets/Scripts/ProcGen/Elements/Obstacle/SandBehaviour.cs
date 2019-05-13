using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandBehaviour : LocalUpdater<Rigidbody>
{
    public float slowdownMultiplier;
    private const string sandRTPC = "Sound_Pitch";
    private int maxSandRTPCValue = 50, minSandRTPCValue = 0;
    private Transform player;
    public ParticleSystem sandVFX;

    private void Update()
    {
        var emission = sandVFX.emission;
        if (PlayerInSand())
        {
            AkSoundEngine.SetRTPCValue(sandRTPC, PlayerDistanceToRTPC());
            emission.enabled = true;
            sandVFX.transform.position = player.position;
        } else {
            emission.enabled = false;
        }
    }

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
        this.player = player.transform;
        base.PlayerEnters(player);
    }

    protected override void PlayerExits(GameObject player)
    {
        this.player = null;
        AkSoundEngine.SetRTPCValue(sandRTPC, minSandRTPCValue);
        base.PlayerExits(player);
    }
    private bool PlayerInSand()
    {
        return this.player != null && PlayerInDistance();
    }
    private int PlayerDistanceToRTPC()
    {
        Vector3 concreteDimensions = this.gridData.GetConcreteDimensions();
        Vector3 center = new Vector3(
            transform.position.x,
            this.player.position.y,
            transform.position.z);
        float maxDistance = Mathf.Sqrt(Mathf.Pow(concreteDimensions.x/2, 2) + Mathf.Pow(concreteDimensions.y / 2, 2));
        float currentDistance = Vector3.Distance(player.position, center);
        return (int)Mathf.Lerp(maxSandRTPCValue, minSandRTPCValue, currentDistance);
    }
    private bool PlayerInDistance() {
        Vector3 concreteDimensions = this.gridData.GetConcreteDimensions();
        Vector3 center = new Vector3(
            transform.position.x,
            this.player.position.y,
            transform.position.z);
        float maxDistance = Mathf.Sqrt(Mathf.Pow(concreteDimensions.x/2, 2) + Mathf.Pow(concreteDimensions.y / 2, 2));
        float currentDistance = Vector3.Distance(player.position, center);
        return currentDistance < maxDistance;
    }
}
