using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelPanda.Player.Events;

namespace VoxelPanda.Score
{
	public class ScoreCalculator : IMoveListener
	{
        public float scorePerMeterMultiplier = 1;
        public float coinValue = 20f;

		private List<IScoreListener> listeners = new List<IScoreListener>();

        private float bestZ = 0f;
        private float currentScore = 0f;
        private float coinScore = 0f;

		public ScoreCalculator(MoveEvents moveEvents)
		{
			moveEvents.Subscribe(this);
		}

        public int GetScore()
        {
            return (int)Mathf.Round(currentScore);
        }

        public void Reset()
        {
            currentScore = bestZ = coinScore = 0f;
            NotifyScoreChanged(Mathf.Round(currentScore));
        }

        public void PickupCoin()
        {
            currentScore += coinValue;
            NotifyScoreChanged(Mathf.Round(currentScore));
        }

        public void OnPositionChanged(Vector3 position)
		{
            if (position.z > bestZ)
            {            
                UpdateScore(position);
                bestZ = position.z;
            }
        }

		public void OnVelocityChanged(Vector3 velocity)
		{
              
        }

        void UpdateScore(Vector3 position)
        {
            currentScore += (position.z - bestZ) * scorePerMeterMultiplier;
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