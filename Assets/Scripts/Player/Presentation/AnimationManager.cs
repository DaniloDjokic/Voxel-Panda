using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelPanda.Player.Events;

namespace VoxelPanda.Player.Presentation
{
	public class AnimationManager : MonoBehaviour, IFlingListener, ICurveListener, IMoveListener
	{
		public Animator animator;
		public string flingStartedAnimation = "Anim_FlingStarted";
		public string flingReleaseAnimation = "Anim_FlingRelease";
		public string movingAnimation = "Anim_Moving";
		public string curveLeftAnimation = "Anim_SwerveLeft";
		public string curveRightAnimation = "Anim_SwerveRight";
		private float previousCurveForce = 0f;

		public void OnCurveChanged(CurveData curveData)
		{
			float newCurveForce = curveData.ModifiedAccelerationVector.x;
			if (Mathf.Abs(newCurveForce) < Mathf.Abs(previousCurveForce)) {
				if (newCurveForce > previousCurveForce) {
					animator.SetTrigger(curveRightAnimation);
				} else {
					animator.SetTrigger(curveLeftAnimation);
				}
			}
			previousCurveForce = newCurveForce;
		}

		public void OnFlingEnded(FlingData flingData)
		{
			animator.SetTrigger(flingReleaseAnimation);
		}

		public void OnFlingRunning(FlingData flingData)
		{
			//throw new System.NotImplementedException();
		}

		public void OnFlingStarted(FlingData flingData)
		{
			animator.SetTrigger(flingStartedAnimation);
		}

		public void OnPositionChanged(Vector3 position)
		{
			//throw new System.NotImplementedException();
		}

        public void OnStaminaChanged(FlingData flingData)
        {
            //throw new System.NotImplementedException();
        }

        public void OnVelocityChanged(Vector3 velocity)
		{
			animator.SetFloat(movingAnimation, velocity.magnitude);
		}
	}
}

