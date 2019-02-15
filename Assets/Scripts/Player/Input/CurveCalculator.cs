using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelPanda.Player.Events;

namespace VoxelPanda.Player.Input
{
	public class CurveCalculator : InputCalculator
	{
		private List<ICurveListener> listeners = new List<ICurveListener>();

		public CurveCalculator(RawInput rawInput, ConstMoveData constMoveData, DynamicMoveData dynMoveData) : base(rawInput, constMoveData, dynMoveData)
		{
		}

		public void Subscribe(ICurveListener listener)
		{
			listeners.Add(listener);
		}
	}
}
