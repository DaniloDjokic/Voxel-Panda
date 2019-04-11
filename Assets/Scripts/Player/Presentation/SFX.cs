﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelPanda.Player.Events;

namespace VoxelPanda.Player.Presentation
{
	public class SFX : MonoBehaviour, IFlingListener, ICurveListener, IMoveListener
	{
        public string swipePullEvent = "Play_Swipe_Pull";
        public string swipeReleaseEvent = "Play_Swipe_Release";
        public string movingEvent = "Play_Skate";
        public string moveEventVelocityValue = "Skate_Speed";
        public string wallKickEvent = "Play_WallKick";

		public void OnCurveChanged(CurveData curveData)
		{
			//throw new System.NotImplementedException();
		}

		public void OnFlingEnded(FlingData flingData)
		{
            AkSoundEngine.PostEvent(swipeReleaseEvent, gameObject);
            //throw new System.NotImplementedException();
        }

		public void OnFlingRunning(FlingData flingData)
		{
			//throw new System.NotImplementedException();
		}

		public void OnFlingStarted(FlingData flingData)
		{
            AkSoundEngine.PostEvent(swipePullEvent, gameObject);
			//throw new System.NotImplementedException();
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
            float velocitySum = velocity.magnitude;
            AkSoundEngine.SetRTPCValue(moveEventVelocityValue, velocitySum);
		}

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Obstacle") || collision.collider.CompareTag("Wall"))
            {
                AkSoundEngine.PostEvent(wallKickEvent, gameObject);
            }
        }
    }
}

