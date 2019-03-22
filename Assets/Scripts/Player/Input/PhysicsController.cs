using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelPanda.Player.Events;

public class PhysicsController : MonoBehaviour
{
    public ConstMoveData constMoveData;
    public DynamicMoveData dynamicMoveData;
    public Rigidbody playerRigidBody;
    public Transform playerTransform;
    private Vector3 startingPosition;

	public void ResetPlayer()
	{
		dynamicMoveData.ResetValues();
	}

	public void ResetPosition()
    {
        this.transform.position = constMoveData.playerStartPosition;
    }

    public void ApplyFlingForce(Vector3 flingForceVector)
    {
        playerRigidBody.AddForce(flingForceVector * Time.deltaTime, ForceMode.Impulse);
    }

    public void ApplyCurveForce(Vector3 curveForceVector)
    {
        if(playerRigidBody.velocity.z > constMoveData.minVelocityForCurve) 
             playerRigidBody.AddForce(curveForceVector * Time.deltaTime, ForceMode.Impulse);
    }

    public void Bind(DynamicMoveData dynamicMoveData)
    {
        this.dynamicMoveData = dynamicMoveData;
    }
}
