using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelPanda.Player.Events
{
	/// <summary>
	/// Wrapper for Move Events
	/// </summary>
	public interface IMoveListener
	{
		void OnPositionChanged(Vector3 position);
		void OnVelocityChanged(Vector3 velocity);
	}
}
