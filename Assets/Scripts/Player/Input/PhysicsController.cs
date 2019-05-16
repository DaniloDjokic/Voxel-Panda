using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using VoxelPanda.Player.Events;

public class PhysicsController : MonoBehaviour
{
    public ConstMoveData constMoveData;
    public DynamicMoveData dynamicMoveData;
    public Rigidbody playerRigidBody;
    public Transform playerTransform;
	public float curveToRadiansFactor = 0.05f;
    private Vector3 startingPosition;

	public void ResetPlayer()
	{
		dynamicMoveData.ResetValues();
	}
	public void RevivePlayer()
	{

	}

	public void ResetPosition()
    {
        this.transform.position = constMoveData.playerStartPosition;
    }

    public void ApplyFlingForce(Vector3 flingForceVector)
    {
        playerRigidBody.AddForce(flingForceVector, ForceMode.VelocityChange);
    }

    public void ApplyCurveForce(Vector3 curveForceVector)
    {
        if(playerRigidBody.velocity.z > constMoveData.minVelocityForCurve)
		{
			Vector3 rotation = (curveForceVector.x > 0) ? Vector3.right : Vector3.left;
			playerRigidBody.velocity = Vector3.RotateTowards(playerRigidBody.velocity, rotation, Mathf.Abs(curveForceVector.x) * curveToRadiansFactor * Time.deltaTime, 0f);
		}
	}

    public void Bind(DynamicMoveData dynamicMoveData)
    {
        this.dynamicMoveData = dynamicMoveData;
    }
}
