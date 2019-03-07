using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicMoveData : MonoBehaviour
{
    public PhysicsController physicsController;

    public float currentStamina;
    public Vector3 currentVelocity;
    public Vector3 currentPosition;

    private void Update()
    {
        RefreshValues();
    }

    void RefreshValues()
    {
        if (currentVelocity != physicsController.playerRigidBody.velocity)
            currentVelocity = physicsController.playerRigidBody.velocity;

        if (currentPosition != physicsController.playerTransform.position)
            currentPosition = physicsController.playerTransform.position;
    }
}
