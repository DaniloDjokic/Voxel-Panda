using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelPanda.Player.Events;

namespace VoxelPanda.Score
{
	public class ScoreCalculator : IMoveListener
	{
        public float scorePerMeterMultiplier = 1;

		private List<IScoreListener> listeners = new List<IScoreListener>();

        private Vector3 startingPos;
        private float bestZ = 0f;
        private float currentScore = 0f;

		public ScoreCalculator(MoveEvents moveEvents, Vector3 startingPos)
		{
			moveEvents.Subscribe(this);
            this.startingPos = startingPos;
		}

		public void OnPositionChanged(Vector3 position)
		{
            if (position.z > bestZ && position.z > startingPos.z)
            {
                bestZ = position.z;
                UpdateScore(position);
            }
        }

		public void OnVelocityChanged(Vector3 velocity)
		{
            
		}

        void UpdateScore(Vector3 position)
        {
            currentScore = (position.z - startingPos.z) * scorePerMeterMultiplier;
            NotifyScoreChanged(Mathf.Round(currentScore));
        }

		public void Subscribe(IScoreListener listener)
		{
			if(!listeners.Contains(listener))
			{
				listeners.Add(listener);
			}
		}

		private void NotifyScoreChanged(float newScore)
		{
			foreach (var listener in listeners)
			{
				listener.OnScoreChanged(newScore);
			}
		}
	}
}

