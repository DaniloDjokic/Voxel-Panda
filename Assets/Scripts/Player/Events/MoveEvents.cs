using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelPanda.Player.Events
{
	public class MoveEvents
	{
		private List<IMoveListener> listeners = new List<IMoveListener>();

        public MoveEvents(DynamicMoveData dMoveData)
        {
            dMoveData.moveEvents = this;
        }

		public void Subscribe(IMoveListener listener)
		{
			if (!listeners.Contains(listener))
			{
				listeners.Add(listener);
			}
		}

		public void NotifyPositionChanged(Vector3 newPosition)
		{
			foreach (var listener in listeners)
			{
				listener.OnPositionChanged(newPosition);
			}
		}

		public void NotifyVelocityChanged(Vector3 newVelocity)
		{
			foreach(var listener in listeners)
			{
				listener.OnVelocityChanged(newVelocity);
			}
		}
	}
}

