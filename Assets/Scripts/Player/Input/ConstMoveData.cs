using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstMoveData : MonoBehaviour
{
    //Fling
    public float flingForce;
    [HideInInspector]
    public float maxStamina = 100f; //this should not be changed
    public float staminaPerFling;
    public float staminaRegenDelay;
    public float staminaRegenPerSecond;
    public float vectorStaminaCost = 1;
    public float vectorComprassionFactor;

    //Curve
    public float curveForce;
    public float accelerationDeadzone;
    public float accelerationFunctionModifier;
    public float minVelocityForCurve;

    //Misc
    public static float universalY = 1f;
	public Vector3 playerStartPosition;
}
