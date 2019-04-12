using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlingData
{
    public Vector3 unmodifiedTouchStartingPosition { get; set; }
    public Vector3 unmodifiedTouchEndPosition { get; set; }
    public Vector3 RawFlingVector { get; set; }
    public Vector3 ModifiedFlingVector { get; set; }
    public Vector3 MaxFlingVector { get; set; }
    public Vector3 MaxCurrentFlingVector { get; set; }
    public Vector3 TouchStartingPosition { get; set; }
    public Vector3 TouchEndPosition { get; set; }
    public Vector3 PlayerPosition { get; set; }
    public Vector3 TransposedVectorEndPosition { get; set; }
    public float CurrentStamina { get; set; }
}
