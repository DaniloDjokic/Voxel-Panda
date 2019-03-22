using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelPanda.Player.Input;

public class RawAccInput : MonoBehaviour
{
    public CurveCalculator curveCalculator;

	private Vector3 currentAccelerationVector;
    private bool detectingInput = true;

	void Update ()
    {
        if(detectingInput)
            UpdateAcceleration();
	}

    public void SetInputDetection(bool active)
    {
        detectingInput = active;
    }

    void UpdateAcceleration()
    {
        currentAccelerationVector = new Vector3(Input.acceleration.x, 0f, 0f);
        curveCalculator.UpdateRawAccelerationVector(currentAccelerationVector);
    }
}
