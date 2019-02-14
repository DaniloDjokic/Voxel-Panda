using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelPanda.Player.Events;

namespace VoxelPanda.Score
{
	public class ScoreCalculator : MonoBehaviour, IMoveListener
	{
		private List<IScoreListener> listeners = new List<IScoreListener>();

		public void OnPositionChanged(Vector3 position)
		{
			throw new System.NotImplementedException();
		}

		public void OnVelocityChanged(Vector3 velocity)
		{
			throw new System.NotImplementedException();
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

