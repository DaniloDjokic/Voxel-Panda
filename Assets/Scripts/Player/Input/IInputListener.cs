using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace VoxelPanda.Player.Input
{
	public interface IInputListener
	{
		void OnTouchStarted(Vector3 position);
		void OnTouchDragged(Vector3 position);
		void OnTouchEnded(Vector3 position);
		void OnAccelerometerChanged(Vector3 accelerometer);
	}
}
