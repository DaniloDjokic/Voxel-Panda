using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelPanda.Player.Events;

public class DynamicMoveData : MonoBehaviour
{
    public PhysicsController physicsController;
	public MoveEvents moveEvents;

    public float currentStamina;
    public Vector3 currentVelocity;
    public Vector3 currentPosition;
	private bool fireEvents = true;

    private void Update()
    {
        RefreshValues();
    }

    void RefreshValues()
    {
		if (fireEvents)
		{
			if(currentVelocity != physicsController.playerRigidBody.velocity)
			{
				currentVelocity = physicsController.playerRigidBody.velocity;
				moveEvents.NotifyVelocityChanged(currentVelocity);
			}


			if(currentPosition != physicsController.playerTransform.position)
			{
				currentPosition = physicsController.playerTransform.position;
				moveEvents.NotifyPositionChanged(currentPosition);
			}
		}

    }

	public void ResetValues()
	{
		fireEvents = false;
		currentVelocity = physicsController.playerRigidBody.velocity = Vector3.zero;
		physicsController.ResetPosition();
		currentPosition = physicsController.playerTransform.position;
		fireEvents = true;
	}
}
