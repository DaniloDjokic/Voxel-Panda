<<<<<<< Updated upstream
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCalculator : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
=======
﻿using System.Collections;
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

>>>>>>> Stashed changes
