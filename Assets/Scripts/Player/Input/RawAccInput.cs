using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelPanda.Player.Input;

public class RawAccInput : MonoBehaviour
{
    public CurveCalculator curveCalculator;

	private Vector3 currentAccelerationVector;
    private bool detectingInput = true;

    private void Start()
    {
        currentAccelerationVector = Vector3.zero;
    }

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
        Vector3 inputVector = new Vector3(Mathf.Round(Input.acceleration.x * 10.0f) / 10.0f, 0f, 0f);
        if(currentAccelerationVector.x != inputVector.x)
        {
            currentAccelerationVector = inputVector;
            curveCalculator.UpdateRawAccelerationVector(currentAccelerationVector);
        }           
    }
}
