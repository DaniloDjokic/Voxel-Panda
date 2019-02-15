using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelPanda.Player;
using VoxelPanda.Player.Events;

public class PhysicsApplier : MonoBehaviour, IFlingListener, ICurveListener
{
	private DynamicMoveData dynMoveData;

	public void Bind(DynamicMoveData dynMoveData)
	{
		this.dynMoveData = dynMoveData;
	}

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

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
