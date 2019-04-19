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
    public float angleWhenBrakingStarts = 60;
    public float angleWhenBrakingStops = 80;
    public ParticleSystem brakingVFX;

    private float pauseTimer = 0f;
    public float angleSinceLastCheckpoint;
	private Quaternion rotationBase;
	private const string roboticArmSFXEvent = "Play_Robot_Arm";
	private bool isGoingToStartMoving = false;

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
			if (isGoingToStartMoving)
			{
				isGoingToStartMoving = false;
				AkSoundEngine.PostEvent(roboticArmSFXEvent, this.gameObject);
			}

			float currentAngle = (counterClockwise ? -speed : speed) * Time.deltaTime;            
            angleSinceLastCheckpoint += currentAngle;

            if(CheckpointReached())
            {
                if (counterClockwise)
                    currentAngle = -rotationBase.eulerAngles.y % angleAmountCheckpoint;
                else
                    currentAngle = angleAmountCheckpoint - (rotationBase.eulerAngles.y % angleAmountCheckpoint);

                PauseRotation();
            } 
            UpdateBrakingVFX(rotationBase.eulerAngles.y);
			Vector3 currentEuler = new Vector3(0, currentAngle, 0);
			craneHook.transform.rotation = rotationBase = Quaternion.Euler(0, rotationBase.eulerAngles.y + currentAngle, 0);
			craneHook.transform.position = RotateAroundPivot(craneHook.transform.position, craneBase.transform.position, currentEuler);
        }
        else
        {
            pauseTimer += Time.deltaTime;
			if (pauseTimer >= pauseTime)
			{
				isGoingToStartMoving = true;
			}
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

    bool IsBraking(float currentAngle) {
        float currentAngleRelative = currentAngle % angleAmountCheckpoint;
        return currentAngleRelative > angleWhenBrakingStarts && currentAngleRelative < angleWhenBrakingStops;
    }

    void UpdateBrakingVFX(float currentAngle) {
        var emission = brakingVFX.emission;
        emission.enabled = IsBraking(currentAngle);
    }

    bool CheckpointReached()
    {
        return Mathf.Abs(angleSinceLastCheckpoint) > angleAmountCheckpoint;
    }
}
