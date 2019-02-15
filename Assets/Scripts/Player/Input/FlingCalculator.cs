using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelPanda.Player.Events;

namespace VoxelPanda.Player.Input
{

	public class FlingCalculator : InputCalculator
	{
		private List<IFlingListener> listeners = new List<IFlingListener>();

		public FlingCalculator(RawInput rawInput, ConstMoveData constMoveData, DynamicMoveData dynMoveData) : base(rawInput, constMoveData, dynMoveData)
		{
		}

		public void Subscribe(IFlingListener listener)
		{
			listeners.Add(listener);
		}

		public void RemoveListener(IFlingListener listener)
		{
			if(listeners.Contains(listener))
			{
				listeners.Remove(listener);
			}
		}

		private void FlingStarted(FlingData flingData)
		{
			for(int i = 0; i < listeners.Count; i++)
			{
				listeners[i].OnFlingStarted(flingData);
			}
		}
	}
}

