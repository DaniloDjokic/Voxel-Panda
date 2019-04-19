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

		private Vector3 direction;
		private Vector3 leftPoint, rightPoint;
        private Vector3 target;
        private float offset = 0.05f;
        private bool isStopped;
        private const string playTrainSFX = "Play_Train";
        private const string stopTrainSFX = "Stop_Train";

        private void Start()
        {
            SetBoundries();
        }

        private void Update()
		{
            if(Mathf.Abs((target.x - train.localPosition.x)) >= offset && !isStopped)
            {
                train.Translate(direction * trainSpeed * Time.deltaTime);
            }
            else
            {
                if(!isStopped)
                {
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
	}
}
