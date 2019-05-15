using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelPanda.Player.Events
{
	/// <summary>
	/// Wrapper for Curve Events
	/// </summary>
	public interface ICurveListener
	{
		void OnCurveChanged(CurveData curveData);
	}
}
