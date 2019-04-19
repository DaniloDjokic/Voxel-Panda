using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelPanda.ProcGen.Elements;

public class RoboticArmBehaviour : ObsBehaviour
{
    public GameObject craneBase;
    public GameObject craneHook;
    public float speed;
    public float pauseTime;
    public float angleAmountCheckpoint;
    public bool counterClockwise;

    private float pauseTimer = 0f;
    public float angleSinceLastCheckpoint;
	private Quaternion rotationBase;

	private void Awake()
	{
		rotationBase = craneBase.transform.rotation;
	}

	private void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        if(pauseTimer >= pauseTime)
        {
            float currentAngle = (counterClockwise ? -speed : speed) * Time.deltaTime;            
            angleSinceLastCheckpoint += currentAngle;

            if(CheckpointReached())
            {
                if (counterClockwise)
                    currentAngle = -rotationBase.eulerAngles.y % angleAmountCheckpoint;
                else
                    currentAngle = angleAmountCheckpoint - (rotationBase.eulerAngles.y % angleAmountCheckpoint);

                PauseRotation();
            } else
			{
				Vector3 currentEuler = new Vector3(0, currentAngle, 0);
				craneHook.transform.rotation = rotationBase = Quaternion.Euler(0, rotationBase.eulerAngles.y + currentAngle, 0);
				craneHook.transform.position = RotateAroundPivot(craneHook.transform.position, craneBase.transform.position, currentEuler);
			}

        }
        else
        {
            pauseTimer += Time.deltaTime;
        }
    }

    Vector3 RotateAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles)
    {
        Vector3 dir = point - pivot;
        dir = Quaternion.Euler(angles) * dir;
        point = dir + pivot;
        return point;
    }

    void PauseRotation()
    {
        pauseTimer = 0;
        angleSinceLastCheckpoint = 0;
    }

    bool CheckpointReached()
    {
        return Mathf.Abs(angleSinceLastCheckpoint) > angleAmountCheckpoint;
    }
}
