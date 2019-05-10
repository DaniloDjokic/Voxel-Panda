using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelPanda.Player.Events;

namespace VoxelPanda.Player.Presentation
{
	public class Particles : MonoBehaviour, IFlingListener, ICurveListener, IMoveListener
	{
		public ParticleSystem skateSparks;
		public AnimationCurve sparkCurve;
		public GameObject hitVFXRoot;
		public ParticleSystem shockwaveVFX;
		public ParticleSystem sparksVFX;


		public void OnCurveChanged(CurveData curveData)
		{
			//throw new System.NotImplementedException();
		}

		public void OnFlingEnded(FlingData flingData)
		{
			//throw new System.NotImplementedException();
		}

		public void OnFlingRunning(FlingData flingData)
		{
			//throw new System.NotImplementedException();
		}

		public void OnFlingStarted(FlingData flingData)
		{
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
			var emission = skateSparks.emission;
			emission.rateOverTimeMultiplier = sparkCurve.Evaluate(velocity.magnitude);			
		}

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.CompareTag("Obstacle") || collision.collider.CompareTag("Wall"))
            {
                var contact = collision.contacts[0];
                //hitVFXRoot.transform.SetParent(transform.root);
                hitVFXRoot.transform.position = contact.point;
                hitVFXRoot.transform.LookAt(this.transform);
                shockwaveVFX.Play();
                sparksVFX.Play();
            }
        }
	}
}

