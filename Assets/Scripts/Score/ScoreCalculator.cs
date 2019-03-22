using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelPanda.Player.Events;

namespace VoxelPanda.Score
{
	public class ScoreCalculator : IMoveListener
	{
		private List<IScoreListener> listeners = new List<IScoreListener>();

		public ScoreCalculator(MoveEvents moveEvents)
		{
			moveEvents.Subscribe(this);
		}

		public void OnPositionChanged(Vector3 position)
		{
		}

		public void OnVelocityChanged(Vector3 velocity)
		{
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

