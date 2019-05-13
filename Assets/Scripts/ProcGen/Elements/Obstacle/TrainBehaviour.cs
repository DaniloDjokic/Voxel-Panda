using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;

namespace VoxelPanda.ProcGen.Elements.Obstacle
{
	public class TrainBehaviour : ObsBehaviour
	{
        public Transform train;
		public float trainSpeed;
		public float trainStoppageTime;

		public Vector3 direction;
		public Vector3 leftPoint, rightPoint;
        public Vector3 target;
        private float offset = 0.05f;
        private bool isStopped = false;
        private const string playTrainSFX = "Play_Train";
        private const string stopTrainSFX = "Stop_Train";
		public bool alreadySetBoundaries = false;
        public ParticleSystem trainBrakingVFX;
        public GameObject traingBrakingVFXPivot;
        public float brakingDistance = 0.3f;

        private void Start()
        {
            SetBoundries();
        }

		private void OnDisable()
		{
			if (alreadySetBoundaries)
			{
				train.localPosition = leftPoint;
				isStopped = false;
			}
		}

		private void Update()
		{ 
            if(target.x * direction.x > train.localPosition.x * direction.x && !isStopped)
            {
                train.Translate(direction * trainSpeed * Time.deltaTime);
                PlayBrakingVFX(Mathf.Abs(target.x - train.localPosition.x));
            }
            else
            {
                if(!isStopped)
               {
                    StopBrakingVFX();
                    AkSoundEngine.PostEvent(stopTrainSFX, train.gameObject);
                    StartCoroutine(SwitchDirection());
                }
                    
            }
		}

        private void SetBoundries()
        {
            leftPoint = train.localPosition;
            rightPoint = leftPoint;
            rightPoint.x = -leftPoint.x;

            direction = Vector3.right;
            target = rightPoint;

            isStopped = false;
			alreadySetBoundaries = true;
        }

        IEnumerator SwitchDirection()
        {
            isStopped = true;

            if (direction == Vector3.left)
            {
                direction = Vector3.right;
                target = rightPoint;
            }
                
            else
            {
                direction = Vector3.left;
                target = leftPoint;
            }

            yield return new WaitForSeconds(trainStoppageTime);
            AkSoundEngine.PostEvent(playTrainSFX, train.gameObject);
            isStopped = false;
        }

        bool IsBraking(float distanceToTarget) {
            return distanceToTarget < brakingDistance;
        }

        void PlayBrakingVFX(float distanceToTarget) {

            if (IsBraking(distanceToTarget)) {
                var emission = trainBrakingVFX.emission;
                emission.enabled = true;
                float rotation = direction.x >= 0 ? 0 : 180;
                traingBrakingVFXPivot.transform.localRotation = Quaternion.Euler(0, rotation, 0);
            }

        }

        void StopBrakingVFX() {
            var emission = trainBrakingVFX.emission;
            emission.enabled = false;
        }
	}
}
