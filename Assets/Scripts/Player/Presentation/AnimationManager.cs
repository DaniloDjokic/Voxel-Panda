using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelPanda.Player.Events;

namespace VoxelPanda.Player.Presentation
{
	public class AnimationManager : MonoBehaviour, IFlingListener, ICurveListener, IMoveListener
	{

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

		public void OnPositionChanged(Vector3 position)
		{
			throw new System.NotImplementedException();
		}

		public void OnVelocityChanged(Vector3 velocity)
		{
			throw new System.NotImplementedException();
		}

		// Use this for initialization
		void Start()
		{

		}

		// Update is called once per frame
		void Update()
		{

		}
	}
}

