using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelPanda.Player.Input
{
	public class RawInput : MonoBehaviour
	{
		private List<IInputListener> listeners = new List<IInputListener>();
		private bool detectingInput;

		public void Subscribe(IInputListener listener)
		{
			listeners.Add(listener);
		}

		public void SetDetectingInput(bool active)
		{
			detectingInput = active;
		}
	}
}