using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsController : MonoBehaviour
{
    public Rigidbody playerRigidBody;
    public Transform playerTransform;

    public void ApplyFlingForce(Vector3 flingForceVector)
    {
        playerRigidBody.AddForce(flingForceVector * Time.deltaTime, ForceMode.Impulse);
    }

    public void ApplyCurveForce(Vector3 curveForceVector)
    {
        if(playerRigidBody.velocity.z > 2f) // change to variable in dynamic move data
             playerRigidBody.AddForce(curveForceVector * Time.deltaTime, ForceMode.Impulse);
    }
}
