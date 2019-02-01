using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelPanda.Player.Events
{
	/// <summary>
	/// Wrapper for Fling Events
	/// </summary>
	public interface IFlingListener
	{
		void OnFlingStarted(FlingData flingData);
		void OnFlingRunning(FlingData flingData);
		void OnFlingEnded(FlingData flingData);
	}
}
