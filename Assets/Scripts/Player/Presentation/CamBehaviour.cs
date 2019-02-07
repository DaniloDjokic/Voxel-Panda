using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelPanda.Player.Presentation {
	/// <summary>
	/// Deals with Camera Behaviour, such as deciding which object to follow
	/// </summary>
public class CamBehaviour : MonoBehaviour
	{
		public Transform followTarget;
		public Transform stopTarget;
		public Vector3 followDirection = new Vector3(0,0,1);
		public float followChangeDistance;
		public float transitionTime = 0.5f;

		private Vector3 deltaPositionFollow;
		private Vector3 deltaPositionStop;

		private CamState camState = CamState.FollowingTarget;
		private float transitionTimer = 0f;

		private void Start()
		{
			CalculateDeltaPositions(followTarget.position);

		}

		private void Update()
		{
			ChangeState();
			UpdateTimer();
			FollowObject();
		}

		private void FollowObject()
		{
			this.transform.position = CalcCamPosition();
		}
		
		private Vector3 CalcCamPosition()
		{
			return Vector3.Lerp(CalcFollowingTargetPosition(), CalcFollowingStopperPosition(), transitionTimer / transitionTime);
		}

		private void ChangeState()
		{
			bool shouldFollowTarget = ShouldFollowTarget();
			if (!shouldFollowTarget && camState == CamState.FollowingTarget)
			{
				camState = CamState.TransitionToStopper;
			} else if (shouldFollowTarget && camState == CamState.FollowingStopper)
			{
				camState = CamState.TransitionToTarget;
			} else if (camState == CamState.TransitionToStopper && transitionTimer >= transitionTime)
			{
				transitionTimer = transitionTime;
				camState = CamState.FollowingStopper;
			} else if (camState == CamState.TransitionToTarget && transitionTimer <= 0)
			{
				transitionTimer = 0f;
				camState = CamState.FollowingTarget;
			}
		}

		private void CalculateDeltaPositions(Vector3 followTargetPosition)
		{
			deltaPositionFollow = this.transform.position - followTargetPosition;
			deltaPositionStop = deltaPositionFollow + (followChangeDistance * followDirection);			
		}

		private void UpdateTimer()
		{
			if (camState == CamState.TransitionToStopper)
			{
				transitionTimer = transitionTime;
			} else if (camState == CamState.TransitionToTarget)
			{
				transitionTimer -= Time.deltaTime;
			}
		}

		private Vector3 CalcFollowingTargetPosition()
		{
			return CalcFollowPosition(this.deltaPositionFollow, followTarget.position);
		}

		private Vector3 CalcFollowingStopperPosition()
		{
			return CalcFollowPosition(this.deltaPositionStop, stopTarget.position);
		}

		private Vector3 CalcFollowPosition(Vector3 offset, Vector3 followTargetPosition)
		{
			return offset + Vector3.Scale(followTargetPosition, followDirection);
		}

		private bool ShouldFollowTarget()
		{
			Vector3 scaledTarget = Vector3.Scale(followTarget.position, followDirection);
			Vector3 scaledStopper = Vector3.Scale(stopTarget.position, followDirection);
			return Vector3.Distance(scaledTarget, scaledStopper) > followChangeDistance && scaledTarget.z > scaledStopper.z;
		}
	}

	internal enum CamState { FollowingTarget, FollowingStopper, TransitionToTarget, TransitionToStopper }
}

