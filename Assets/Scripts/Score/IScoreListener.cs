using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VoxelPanda.Score
{
	public interface IScoreListener
	{
		void OnScoreChanged(float newScore);
	}
}

