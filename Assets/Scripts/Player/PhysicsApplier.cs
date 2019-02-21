using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelPanda.Player;
using VoxelPanda.Player.Events;

public class PhysicsApplier : MonoBehaviour, IFlingListener, ICurveListener
{
	private DynamicMoveData dynMoveData;
	private Vector3 startingPosition;

	public void Bind(DynamicMoveData dynMoveData)
	{
		this.dynMoveData = dynMoveData;
	}
	

	// Use this for initialization
	void Awake () {
		startingPosition = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ResetPosition()
	{
		this.transform.position = startingPosition;
	}


	#region EventCallbacks
	public void OnCurveChanged(CurveData curveData)
	{
		throw new System.NotImplementedException();
	}

	public void OnFlingEnded(FlingData flingData)
	{
		throw new System.NotImplementedException();
	}

	public void OnFlingRunning(FlingData flingData)
	{
		throw new System.NotImplementedException();
	}

	public void OnFlingStarted(FlingData flingData)
	{
		throw new System.NotImplementedException();
	}
	#endregion
}
