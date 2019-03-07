<<<<<<< Updated upstream
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEvents : MonoBehaviour {

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

namespace VoxelPanda.Player.Events
{
	public class MoveEvents : MonoBehaviour
	{
		private List<IMoveListener> listeners = new List<IMoveListener>();

		public void Subscribe(IMoveListener listener)
		{
			if (!listeners.Contains(listener))
			{
				listeners.Add(listener);
			}
		}

		private void NotifyPositionChanged(Vector3 newPosition)
		{
			foreach (var listener in listeners)
			{
				listener.OnPositionChanged(newPosition);
			}
		}

		private void NotifyVelocityChanged(Vector3 newVelocity)
		{
			foreach(var listener in listeners)
			{
				listener.OnVelocityChanged(newVelocity);
			}
		}
	}
}

>>>>>>> Stashed changes
